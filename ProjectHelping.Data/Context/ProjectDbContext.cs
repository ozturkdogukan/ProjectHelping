using Microsoft.EntityFrameworkCore;
using ProjectHelping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Data.Context
{
    public class ProjectDbContext : DbContext
    {

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {


        }
        public DbSet<Project> Project { get; set; }
        public DbSet<Advert> Advert { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<SubProject> SubProject { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Relation> Relation { get; set; }

    }

}
