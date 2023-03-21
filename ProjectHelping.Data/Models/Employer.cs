using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("Employer")]
    public class Employer : User
    {
        public string BMail { get; set; }
        public string Phone { get; set; }

    }


}
