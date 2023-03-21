using System;
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
        public int Id { get; set; }
        public int SubProjectId { get; set; }
        public int Quata { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public int EmployerId { get; set; }
        public string CRDT { get; set; }
        public string CRTM { get; set; }
    }
}
