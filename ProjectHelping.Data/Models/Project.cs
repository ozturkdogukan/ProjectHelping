﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("Project")]
    public class Project
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDesc { get; set; }
        public string EmployerId { get; set; }
        public string CRDT { get; set; }
        public string CRTM { get; set; }
    }
}
