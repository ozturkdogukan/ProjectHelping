using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("Skill")]
    public class Skill
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SkillName { get; set; }
    }
}
