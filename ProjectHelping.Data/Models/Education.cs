using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("Education")]
    public class Education
    {
        [Key]
        public string Id
        { get; set; }

        public string UserId { get; set; }
        public string SchoolName { get; set; }
        public string Degree { get; set; }
        public string Time { get; set; }
        public string Desc { get; set; }
    }
}
