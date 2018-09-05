using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.QueryTypes;
using Polyglot.DataAccess.Seeds;

namespace Polyglot.DataAccess.SqlRepository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Glossary> Glossaries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectHistory> ProjectHistories { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ComplexString> ComplexStrings { get; set; }
        public DbSet<TranslatorLanguage> TranslatorLanguages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<GlossaryString> GlossaryStrings { get; set; }
        public DbSet<TeamTranslator> TeamTranslators { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationOption> NotificationOptions { get; set; }
        public DbSet<Option> Options { get; set; }

        public DbQuery<UserRights> UserRights { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasMany(up => up.Ratings)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfile>()
                .HasMany(up => up.Notifications)
                .WithOne(r => r.UserProfile)
                .HasForeignKey(r => r.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.CreatedBy)
                .WithMany()
                .HasForeignKey(r => r.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(r => r.SendFrom)
                .WithMany()
                .HasForeignKey(r => r.SendFromId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.MainLanguage)
                .WithMany(l => l.Projects)
                .HasForeignKey(p => p.MainLanguageId);

            modelBuilder.Entity<ProjectGlossary>()
                .HasKey(pg => new { pg.GlossaryId, pg.ProjectId });

            modelBuilder.Entity<ProjectGlossary>()
                .HasOne(pg => pg.Glossary)
                .WithMany(g => g.ProjectGlossaries)
                .HasForeignKey(pg => pg.GlossaryId);

            modelBuilder.Entity<ProjectGlossary>()
                .HasOne(pg => pg.Project)
                .WithMany(p => p.ProjectGlossaries)
                .HasForeignKey(pg => pg.ProjectId);

            modelBuilder.Entity<ProjectLanguage>()
                .HasKey(pl => new { pl.LanguageId, pl.ProjectId });

            modelBuilder.Entity<ProjectLanguage>()
                .HasOne(pl => pl.Language)
                .WithMany()
                .HasForeignKey(pl => pl.LanguageId);

            modelBuilder.Entity<ProjectLanguage>()
                .HasOne(pl => pl.Project)
                .WithMany(p => p.ProjectLanguageses)
                .HasForeignKey(pl => pl.ProjectId);

            modelBuilder.Entity<TeamTranslator>()
                .HasOne(tt => tt.Team)
                .WithMany(team => team.TeamTranslators)
                .HasForeignKey(tt => tt.TeamId);

            modelBuilder.Entity<TeamTranslator>()
                .HasOne(tt => tt.UserProfile)
                .WithMany(translator => translator.TeamTranslators)
                .HasForeignKey(tt => tt.TranslatorId);

            modelBuilder.Entity<TranslatorLanguage>()
                .HasKey(tl => new { tl.TranslatorId, tl.LanguageId });

            modelBuilder.Entity<TranslatorLanguage>()
                .HasOne(tl => tl.Language)
                .WithMany()
                .HasForeignKey(tl => tl.LanguageId);

            modelBuilder.Entity<TranslatorLanguage>()
                .HasOne(tl => tl.UserProfile)
                .WithMany()
                .HasForeignKey(tl => tl.TranslatorId);

            modelBuilder.Entity<TranslatorRight>()
                .HasKey(tr => new { tr.TeamTranslatorId, tr.RightId });

            modelBuilder.Entity<TranslatorRight>()
                .HasOne(tr => tr.Right)
                .WithMany(r => r.TranslatorRights)
                .HasForeignKey(tr => tr.RightId);

            modelBuilder.Entity<TranslatorRight>()
                .HasOne(tr => tr.TeamTranslator)
                .WithMany(teamTr => teamTr.TranslatorRights)
                .HasForeignKey(tr => tr.TeamTranslatorId);

            modelBuilder.Entity<Glossary>()
                .HasOne(p => p.OriginLanguage)
                .WithMany(l => l.Glossaries)
                .HasForeignKey(p => p.OriginLanguageId);

            modelBuilder.Query<UserRights>()
                .ToView("View_UserRights");

            modelBuilder.Entity<NotificationOption>()
                .HasKey(tr => new { tr.NotificationId, tr.OptionID });

            modelBuilder.Entity<NotificationOption>()
                .HasOne(tr => tr.Notification)
                .WithMany(r => r.NotificationOptions)
                .HasForeignKey(tr => tr.NotificationId);

            modelBuilder.Entity<NotificationOption>()
                .HasOne(tr => tr.Option)
                .WithMany(tr => tr.NotificationOptions)
                .HasForeignKey(tr => tr.OptionID);
            //new feature in EF Core 2.1
            //modelBuilder.Seed();



        }
    }
}
