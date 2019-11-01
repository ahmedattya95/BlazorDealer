using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlazorDealer.Models.Shared
{
    public class AppFile
    {

        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
    }
}
