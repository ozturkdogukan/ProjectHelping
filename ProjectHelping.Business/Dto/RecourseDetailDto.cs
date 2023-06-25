using ProjectHelping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Business.Dto
{
    public class RecourseDetailDto
    {
        public string Id { get; set; }
        public Employer Employer { get; set; }
        public Developer Developer { get; set; }
        public SubProject SubProject { get; set; }
        public Advert Advert { get; set; }
        public string Desc { get; set; }
        public int Status { get; set; }
    }
}
