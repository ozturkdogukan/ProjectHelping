using ProjectHelping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Business.Dto
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDesc { get; set; }
        public Employer Employer { get; set; }
        public string CRDT { get; set; }
        public string CRTM { get; set; }
    }
}
