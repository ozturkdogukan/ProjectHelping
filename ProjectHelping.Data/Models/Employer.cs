using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    public class Employer : User
    {
        public string Mail { get; set; }
        public string Phone { get; set; }
        public List<int> DevIds { get; set; }

    }


}
