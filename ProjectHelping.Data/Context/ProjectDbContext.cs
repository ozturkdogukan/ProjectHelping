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
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=projectassistance;Trusted_Connection=True;TrustServerCertificate=True");
            }
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
