using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeploPro.Models.HomeViewModels;

namespace TeploPro.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<InputDataModel> InputData { get; set; }
        public DbSet<СoefficientsReference> Сoefficients { get; set; }
        public DbSet<ReferenceModel> References { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ReferenceModel reference = ReferenceModel.GetDefaultCoefficients();

            modelBuilder.Entity<СoefficientsReference>().HasData(reference.CokeCunsumptionCoefficents);
            modelBuilder.Entity<СoefficientsReference>().HasData(reference.FurnanceCapacityCoefficents);

            reference.CokeCunsumptionCoefficents = null;
            reference.FurnanceCapacityCoefficents = null;


            modelBuilder.Entity<ReferenceModel>().HasData(reference);

            //ReferenceModel reference = new ReferenceModel
            //{
            //    Id = 1,
            //    RefId1 = 1,
            //    RefId2 = 1,
            //    CokeCunsumptionCoefficents = new СoefficientsReference { Id = 1 },
            //    FurnanceCapacityCoefficents = new СoefficientsReference { Id = 2 }
            //};


            //modelBuilder.Entity<ReferenceModel>().HasData(ReferenceModel.GetDefaultCoefficients());

            base.OnModelCreating(modelBuilder);
        }
    }
}
