using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Models
{
    [Table("Relation")]
    public class Relation
    {
        public int Id { get; set; }
        public string RelationType { get; set; }
        public string MasterObject { get; set; }
        public string SlaveObject { get; set; }
        public int MasterId { get; set; }
        public int SlaveId{ get; set; }
    }
}
