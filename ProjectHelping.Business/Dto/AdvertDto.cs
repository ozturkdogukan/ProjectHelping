using ProjectHelping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Business.Dto
{
    public class AdvertDto
    {
        public string Id { get; set; }
        public SubProject SubProject { get; set; }
        public int Price { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
        public Employer Employer { get; set; }
        public string CRDT { get; set; }
        public string CRTM { get; set; }
    }
}
