using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartAC1.Core.Models;
using SmartAC1.Data.Mappings;

namespace SmartAC1.Data
{
    public class  SmartAc1DbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<SensorData> SensorData { get; set; }

        public SmartAc1DbContext(DbContextOptions<SmartAc1DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapDevice();
            modelBuilder.MapSensorData();
            base.OnModelCreating(modelBuilder);
        }
    }
}
