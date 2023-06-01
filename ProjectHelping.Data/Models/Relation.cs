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
        public string Id { get; set; }
        public string RelationType { get; set; }
        public string MasterObject { get; set; }
        public string SlaveObject { get; set; }
        public string MasterId { get; set; }
        public string SlaveId { get; set; }
    }
}
