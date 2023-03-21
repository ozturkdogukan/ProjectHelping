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
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public string Desc { get; set; }
    }
}
