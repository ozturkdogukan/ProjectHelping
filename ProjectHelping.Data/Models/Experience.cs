using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("Experience")]
    public class Experience
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CompanyName { get; set; }
        public string Time { get; set; }
        public string Desc { get; set; }
        public string Position { get; set; }
    }
}
