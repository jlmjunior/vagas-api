using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Projeto.Dominio;
using Microsoft.AspNetCore.Identity;

namespace Projeto.Repositorio
{
    public class Contexto : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                              IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Vaga> Vagas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => 
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });

            builder.Entity<Organizacao>(org =>
            {
                org.ToTable("Organizacoes");
                org.HasKey(x => x.Id);

                org.HasMany<User>()
                    .WithOne()
                    .HasForeignKey(x => x.OrgId)
                    .IsRequired(false);
            });
        }
    }
}
