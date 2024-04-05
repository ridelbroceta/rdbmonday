using App.ApplicationLayer.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.ApplicationLayer.Contexts
{
    public partial class RidelTestContext : DbContext
    {
        public RidelTestContext()
        {

        }
        public RidelTestContext(DbContextOptions<RidelTestContext> options)
            : base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("\"Server=pbcsql14dev;Database=RidelTest;User Id=URidelTest;Password=URidelTest;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;\"");
        //}

        public virtual DbSet<Table1> Table1s { get; set; }
        public virtual DbSet<Table2> Table2s { get; set; }

        public virtual DbSet<Table3> Table3s { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("dbo");
            //base.OnModelCreating(modelBuilder);
        }
    }
}
