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
        public ProjectDbContext() : base()
        {

        }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=db;port=3306;user=root;password=S3cur3P@ssW0rd!;database=projectassistance");
            }
        }

        public DbSet<Project> Project { get; set; }
        public DbSet<Advert> Advert { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<SubProject> SubProject { get; set; }
        public DbSet<Relation> Relation { get; set; }
        public DbSet<Recourse> Recourse { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Experience> Experience { get; set; }

    }

}
