using ContactsManage.Map;
using ContactsManage.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace ContactsManage.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UserModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=contatosdb;uid=root;pwd=", ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        public class BancoContextFactory : IDesignTimeDbContextFactory<BancoContext>
        {
            public BancoContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BancoContext>();
                optionsBuilder.UseMySql("server=localhost;database=contatosdb;uid=root;pwd=", ServerVersion.Parse("8.0.31-mysql"));

                return new BancoContext(optionsBuilder.Options);
            }
        }

    }
}