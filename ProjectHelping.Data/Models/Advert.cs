﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("Advert")]
    public class Advert
    {
        [Key]
        public string Id { get; set; }
        public string SubProjectId { get; set; }
        public int Quata { get; set; }
        public int Price { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
        public string EmployerId { get; set; }
        public string CRDT { get; set; }
        public string CRTM { get; set; }
    }
}
