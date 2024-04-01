using AlunosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }
        public virtual DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Maria da Penha",
                    Email = "mariapenha@yahoo.com",
                    Idade = 23,
                    Senha = 1234,
                    Foto = "https://s2.glbimg.com/KOETFfAwGiz8WSEoNSK9UKhNL6Q=/e.glbimg.com/og/ed/f/original/2019/10/20/2802213pzb1877.jpg"
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Manuel Bueno",
                    Email = "manuelbueno@yahoo.com",
                    Idade = 22,
                    Senha = 1235,
                    Foto = "https://yt3.googleusercontent.com/fqVEkuSr9CdD3yi8gDkZYebDE_d5UIuvtb-bvuja_xPQSPu6GSS7ha4c2rcJPNQIvXe6FUqOJvU=s900-c-k-c0x00ffffff-no-rj"
                }
            );
        }
    }
}
