using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDesc { get; set; }
        public int EmployerId { get; set; }
        public List<int> SubProjectIds { get; set; }
    }
}
