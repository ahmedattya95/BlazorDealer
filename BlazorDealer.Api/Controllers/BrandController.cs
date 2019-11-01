using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BlazorDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandsController : ControllerBase
    {

        private readonly BlazorDealerDbContext _db;
        private readonly IConfiguration _configuration; 
        public BrandsController(BlazorDealerDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            var brands = _db.Brands.Select(b => new Brand
            {
                ID = b.ID,
                Country = b.Country,
                Name = b.Name,
                IconPath = $"{_configuration["WebsiteUrl"]}{b.IconPath}",
            });

            return Ok(new HttpCollectionResponse<Brand>
            {
                IsSuccess = true,
                Message = "Brands returned successfully!",
                Values = brands
            });
        }

        [HttpPost]
        [RequestSizeLimit(40000000)]
        public async Task<IActionResult> Post([FromForm]BrandViewModel model, [FromForm]IFormFile brandIcon)
        {
            if (ModelState.IsValid)
            {
                // Check the file 
                if (brandIcon == null)
                {
                    return BadRequest("Brand icon file is required");
                }

                var fileName = brandIcon.FileName;
                var extension = Path.GetExtension(fileName);
                var allowedExtensions = new List<string>
                {
                    ".png", ".jpg", ".bmp"
                };

                // Check the extension 
                if (!allowedExtensions.Contains(extension))
                    return BadRequest("Please upload a valid image file");

                string newFileName = $"images/{Guid.NewGuid().ToString()}{extension}";

                using (var fileStream = new FileStream($"{Directory.GetCurrentDirectory()}/wwwroot/{newFileName}", FileMode.Create, FileAccess.Write))
                {
                    await brandIcon.CopyToAsync(fileStream); 
                }

                var brand = new Brand
                {
                    ID = Guid.NewGuid().ToString(),
                    Country = model.Country,
                    CreatedDate = DateTime.UtcNow,
                    Name = model.Name,
                    IconPath = newFileName
                };

                await _db.Brands.AddAsync(brand);
                await _db.SaveChangesAsync();

                brand.IconPath = $"{_configuration["WebsiteUrl"]}{brand.IconPath}";
                return Ok(new HttpSingleResponse<Brand>
                {
                    IsSuccess = true, 
                    Message = "Brand added successfully!", 
                    Value = brand
                });
                
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var brand = await _db.Brands.FindAsync(id);
            if (brand == null)
                return NotFound();

            // Remove the file 
            string filePath = $"{Directory.GetCurrentDirectory()}/wwwroot/{brand.IconPath}";
            System.IO.File.Delete(filePath);

            _db.Brands.Remove(brand);
            await _db.SaveChangesAsync();

            return Ok(new HttpSingleResponse<Brand>
            {
                IsSuccess = true, 
                Message = "Brand deleted successfully!", 
                Value = new Brand
                {
                    Country = brand.Country, 
                    IconPath = $"{_configuration["WebsiteUrl"]}/{brand.IconPath}", 
                    Name = brand.Name, 
                    ID = brand.ID
                }
            });

        }
    }
}