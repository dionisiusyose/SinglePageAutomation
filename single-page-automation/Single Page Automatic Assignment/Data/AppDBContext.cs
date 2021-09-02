using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Single_Page_Automatic_Assignment.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vCampaignHeader>().HasNoKey().ToView("vCampaignHeader");
        }

        public DbSet<Mst_BU> Mst_BU { get; set; }
        public DbSet<Mst_Brand> Mst_Brand { get; set; }
        public DbSet<vCampaignHeader> vCampaignHeader { get; set; }
        public DbSet<Campaign_FileUpload> Campaign_FileUpload { get; set; }
    }
}
