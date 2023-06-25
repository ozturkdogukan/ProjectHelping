using ProjectHelping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Business.Dto
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string RelationType { get; set; }
        public string MasterObject { get; set; }
        public string SlaveObject { get; set; }
        public string MasterId { get; set; }
        public string SlaveId { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

    }
}
