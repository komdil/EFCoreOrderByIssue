using Microsoft.EntityFrameworkCore;

namespace EFOrderByIssue
{
    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost;Initial Catalog=MyDemo;integrated security=True;TrustServerCertificate=True;");
            optionsBuilder.AddInterceptors(new MyInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Student>();
            entity.HasKey(x => x.Guid);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Student
    {
        public Guid Guid { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
