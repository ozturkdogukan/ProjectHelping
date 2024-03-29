﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("Employer")]
    public class Employer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Languages { get; set; }
        public string Password { get; set; }
        public string BMail { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Desc { get; set; }
        public string Expertise { get; set; }
        public string JobExperience { get; set; }
        public string CRDT { get; set; }
        public string CRTM { get; set; }
    }


}
