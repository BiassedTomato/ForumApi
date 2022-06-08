using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Shmelev_Backend_Task3
{
    /// <summary>
    /// wtf
    /// </summary>
    public class ForumContext : IdentityDbContext<ForumUser,ForumRole,string,IdentityUserClaim<string>,ForumUserRole,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public ForumContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ModeratedBoard> ModeratedBoards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Board>()
                .HasMany(x => x.Threads)
                .WithOne(x => x.Board)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Thread>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.Thread)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Attachments)
                .WithOne(x => x.Post)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ModeratedBoard>()
                .HasOne(x => x.Board)
                .WithOne(x => x.ModeratedBoard)
                .HasForeignKey<ModeratedBoard>(x=>x.BoardId);


            modelBuilder.Entity<ForumUser>()
                .HasMany(x => x.AuthoredThreads)
                .WithOne(x => x.Author);

            modelBuilder.Entity<ForumUser>()
                .HasMany(x => x.AuthoredPosts)
                .WithOne(x => x.Author);

            modelBuilder.Entity<ForumUser>()
                .HasMany(x => x.ModeratedBoards)
                .WithMany(x => x.Moderators);

            modelBuilder.Entity<ForumUser>()
                .HasMany(x => x.ModeratedBoards)
                .WithMany(x => x.Moderators);

            modelBuilder.Entity<ForumUser>()
            .HasMany(x => x.ForumUserRoles)
            .WithOne(x => x.User)
            .HasForeignKey(x=>x.UserId);

            modelBuilder.Entity<ForumRole>()
            .HasMany(x => x.ForumUserRoles)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId);


            string admID = null;
            string modID = null;

            foreach (var role in Shmelev_Backend_Task3.Roles.RolesList)
            {
                var gid = Guid.NewGuid().ToString();
                modelBuilder.Entity<ForumRole>()
                    .HasData(new ForumRole()
                    {
                        Id = gid,
                        Name = role,
                        NormalizedName = role.ToUpper()

                    });

                if (role == "Admin")
                    admID = gid;
                else if (role == "Moderator")
                    modID = gid;
            }

            // seed admin

            var adm = new ForumUser()
            {
                Id="1",
                UserName = "admin@admin.ru",
                NormalizedUserName = "ADMIN@ADMIN.RU",
                NormalizedEmail = "ADMIN@ADMIN.RU",
                Email = "admin@admin.ru",
                EmailConfirmed = true,

            };

            var mod = new ForumUser()
            {
                Id = "2",
                UserName = "mod@mod.ru",
                NormalizedUserName = "mod@mod.ru".ToUpper(),
                NormalizedEmail = "mod@mod.ru".ToUpper(),
                Email = "mod@mod.ru",
                EmailConfirmed = true,

            };

            // password hashing :)
            PasswordHasher<ForumUser> hasher = new PasswordHasher<ForumUser>();

            adm.PasswordHash = hasher.HashPassword(adm, "Admin123!");

            mod.PasswordHash = hasher.HashPassword(mod, "Admin123!");




            modelBuilder.Entity<ForumUser>()
                .HasData(adm);

            modelBuilder.Entity<ForumUser>()
                .HasData(mod);


            // assign the role

            modelBuilder.Entity<ForumUserRole>()
                .HasData(new ForumUserRole()
                {
                    RoleId = admID,
                    UserId = "1"// TODO: linking between roles for admin
                });

            modelBuilder.Entity<ForumUserRole>()
                .HasData(new ForumUserRole()
                {
                    RoleId = modID,
                    UserId = "2"// TODO: linking between roles for admin
                });
        }
    }
}
