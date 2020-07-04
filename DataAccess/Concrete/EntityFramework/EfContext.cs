using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using Utilities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {

        }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentRelated> DocumentRelateds { get; set; }
        public DbSet<DocumentAttachment> DocumentAttachments { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Correspondence> Correspondences { get; set; }
        public DbSet<To> Tos { get; set; }
        public DbSet<Inbox> Inbox { get; set; }
        public DbSet<Sent> Sent { get; set; }
        public DbSet<Trash> Trash { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<AboutMe> AboutMes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>()
                .HasData(
                new Unit
                {
                    Id = 1,
                    Name = "Merkez",
                    UpperUnitId = 1
                }
            );

            modelBuilder.Entity<Institution>()
                .HasData(
                new Institution
                {
                    Id = 1,
                    Name = "Boğaziçi Üniversitesi",
                    Domain = "@gmail.com",
                    SmtpHost = "smtp.gmail.com",
                    SmtpPort = 587,
                    Email = "tempmaildonotreply",
                    Password = "karahanll09",
                    Logo = "Logo.png"
                }
            );
            modelBuilder.Entity<Staff>()
                .HasData(
                new Staff
                {
                    Id = 1,
                    Name = "Super",
                    LastName = "Admin",
                    ProfileImage = "DefaultProfileImage.png",
                    Nickname = "super.admin",
                    Password = new Utility().ConvertToHashCode("superadmin"),
                    Title = "Admin",
                    DateOfBirth = DateTime.Now,
                    Gender = "Erkek",
                    UnitId = 1,
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
