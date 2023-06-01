using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("SubProject")]
    public class SubProject
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProjectId { get; set; }
        public string Desc { get; set; }
        public string CRDT { get; set; }
        public string CRTM { get; set; }
    }
}
