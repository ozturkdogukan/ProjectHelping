using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    public class Recourse
    {
        public string Id { get; set; }
        public string EmployerId { get; set; }
        public string DeveloperId { get; set; }
        public string SubProjectId { get; set; }
        public string AdvertId { get; set; }
        public string Desc { get; set; }
        public int Status { get; set; }
    }
}
