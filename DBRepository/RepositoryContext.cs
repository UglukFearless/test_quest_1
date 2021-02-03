/****************************************************************************
*  Файл: RepositoryContext.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса RepositoryContext
****************************************************************************/

using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Security;

namespace DBRepository
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class RepositoryContext : DbContext
    {
        // Конструктор - вызываем констуктор родительского класса
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Назначение ключей уникальности для моделей

            modelBuilder.Entity<SecurityUser>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Code)
                .IsUnique();

            modelBuilder.Entity<ClaimsDefenition>()
                .HasIndex(c => new { c.RoleId, c.Value })
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(r => r.Token)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(r => r.Session)
                .IsUnique();

            modelBuilder.Entity<AppUser>()
                .HasIndex(r => r.Nickname)
                .IsUnique();

            modelBuilder.Entity<AppUser>()
                .HasIndex(r => r.SecurityUserId)
                .IsUnique();

            #endregion Назначение ключей уникальности для моделей
        }

        #region Определение DbSets

        public DbSet<SecurityUser> SecurityUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ClaimsDefenition> ClaimsDefenitions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        #endregion Определение DbSets

    }
}
