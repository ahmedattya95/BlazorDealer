using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorDealer.Models.Shared
{
    public class HttpSingleResponse<T> 
    {
        public HttpSingleResponse()
        {
            ResponseDate = DateTime.UtcNow; 
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime ResponseDate { get; set; }
        public T Value { get; set; }

    }

    public class HttpCollectionResponse<T>
    {
        public HttpCollectionResponse()
        {
            ResponseDate = DateTime.UtcNow; 
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime ResponseDate { get; set; }
        public IEnumerable<T> Values { get; set; }
    }
}
