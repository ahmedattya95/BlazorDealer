using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly BlazorDealerDbContext _db;
        private readonly IConfiguration _configuration;
        List<string> allowedExtensions = new List<string>
                    {
                        ".png", ".jpg", ".bmp"
                    };
        public CarsController(IConfiguration configuration, BlazorDealerDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }

        [HttpGet]
        public IActionResult Get(string query, bool getAll)
        {
            if (query == null)
                query = "";

            var cars = _db.Cars.Include(c => c.Brand).Include(c => c.CarType).Where(c => c.Model.Contains(query) || c.Brand.Name.Contains(query))
                .Select(c => new Car
                {
                    ID = c.ID,
                    CarType = new CarType
                    {
                        Name = c.CarType.Name
                    },
                    Price = c.Price,
                    Year = c.Year,
                    PhotoPath = $"{_configuration["WebsiteUrl"]}{c.PhotoPath}",
                    Brand = new Brand
                    {
                        Name = c.Brand.Name,
                        Country = c.Brand.Country
                    },
                })
                .ToArray();

            return Ok(new HttpCollectionResponse<Car>
            {
                IsSuccess = true,
                Message = "Cars rerieved successfully!",
                Values = cars
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var car = await _db.Cars.Include(c => c.Brand).Include(c => c.CarType).SingleOrDefaultAsync(c => c.ID == id);
            if (car == null)
                return NotFound();


            // Get the images 
            var images = _db.CarImages.Where(i => i.CarId == id).Select(i => new CarImage
            {
                ImagePath = $"{_configuration["WebsiteUrl"]}{i.ImagePath}"
            }).ToList();

            var bookings = _db.Bookings.Where(i => i.CarId == id).Select(i => new Booking
            {
                CreatedDate = i.CreatedDate,
                Description = i.Description,
                Time = i.Time,
                ID = i.ID
            });

            return Ok(new HttpSingleResponse<Car>
            {
                IsSuccess = true,
                Message = "Car has been retrieved successfully!",
                Value = new Car
                {
                    PhotoPath = $"{_configuration["WebsiteUrl"]}{car.PhotoPath}",
                    Brand = new Brand
                    {
                        ID = car.BrandId,
                        Name = car.Brand.Name,
                        Country = car.Brand.Country,
                        IconPath = $"{_configuration["WebsiteUrl"]}{car.Brand.IconPath}"
                    },
                    CarType = new CarType
                    {
                        ID = car.CarTypeId,
                        Name = car.CarType.Name,
                    },
                    CarImages = images.ToList(),
                    Description = car.Description,
                    ID = car.ID,
                    Model = car.Model,
                    Price = car.Price,
                    Year = car.Year,
                    Bookings = bookings.ToList()
                }
            });
        }

        // Add a new Car 
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CarViewModel model, [FromForm]IFormFile carImage)
        {
            if (ModelState.IsValid)
            {
                // Validate the price 
                if (model.Price < 0)
                    return BadRequest("Price must be bigger than 0");

                // Get the brand and type 
                var type = await _db.CarTypes.FindAsync(model.TypeId);
                if (type == null)
                    return BadRequest("Invalid type ID");

                var brand = await _db.Brands.FindAsync(model.BrandId);
                if (brand == null)
                    return BadRequest("Invalid brand ID");

                string carImagePath = "images/default.jpg";

                if (carImage != null)
                {
                    string extension = Path.GetExtension(carImage.FileName);


                    // Check the extension 
                    if (!allowedExtensions.Contains(extension))
                        return BadRequest("Please upload a valid image file");

                    string newFileName = $"images/{Guid.NewGuid().ToString()}{extension}";

                    using (var fileStream = new FileStream($"{Directory.GetCurrentDirectory()}/wwwroot/{newFileName}", FileMode.Create, FileAccess.Write))
                    {
                        await carImage.CopyToAsync(fileStream);
                    }

                    carImagePath = newFileName;
                }

                var car = await _db.Cars.AddAsync(new Car
                {
                    ID = Guid.NewGuid().ToString(),
                    BrandId = model.BrandId,
                    CarTypeId = model.TypeId,
                    Description = model.Description?.Trim(),
                    CreatedDate = DateTime.UtcNow,
                    PhotoPath = carImagePath,
                    Price = model.Price,
                    Model = model.CarModel,
                    Year = model.Year,
                });
                await _db.SaveChangesAsync();

                return Ok(new HttpSingleResponse<Car>
                {
                    IsSuccess = true,
                    Message = "Car added successfully!",
                    Value = new Car
                    {
                        Model = car.Entity.Model,
                        Year = car.Entity.Year,
                        BrandId = car.Entity.BrandId,
                        CarTypeId = car.Entity.CarTypeId,
                        Brand = new Brand
                        {
                            Name = brand.Name,
                        },
                        ID = car.Entity.ID,
                        Description = car.Entity.Description,
                        Price = car.Entity.Price,
                        PhotoPath = $"{_configuration["WebsiteUrl"]}{car.Entity.PhotoPath}",
                    }
                });
            }

            return BadRequest("Some properties are not valid");
        }

        // Upload car images 
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id, [FromForm]IEnumerable<IFormFile> carImages)
        {
            if (id == null)
                return NotFound();

            var car = await _db.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            var files = new List<string>();

            foreach (var file in carImages)
            {
                string extension = Path.GetExtension(file.FileName);
                if (!allowedExtensions.Contains(extension))
                    return BadRequest($"{file.FileName} is not a valid image file");

                files.Add($"images/{Guid.NewGuid().ToString()}{extension}");
            }

            int i = 0;
            foreach (var file in carImages)
            {
                using (var fileStream = new FileStream($"{Directory.GetCurrentDirectory()}/wwwroot/{files[i]}", FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }

                await _db.CarImages.AddAsync(new CarImage
                {
                    CarId = id,
                    UploadDate = DateTime.Now,
                    ImagePath = files[i++],
                    ID = Guid.NewGuid().ToString(),
                });
            }

            await _db.SaveChangesAsync(); 

            return Ok(new HttpCollectionResponse<string>
            {
                IsSuccess = true,
                Message = $"{i} file(s) has been uploaded successfully!",
                Values = files.Select(f => $"{_configuration["WebsiteUrl"]}{f}")
            });
        }


    }
}