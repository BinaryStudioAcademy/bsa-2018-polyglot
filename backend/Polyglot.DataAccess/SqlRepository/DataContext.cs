using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.Entities;
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
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectHistory> ProjectHistories { get; set; }
        public DbSet<ProjectLanguage> ProjectLanguages { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ComplexString> ComplexStrings { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<TranslatorLanguage> TranslatorLanguages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<ProjectTag>()
               .HasKey(pt => new { pt.TagId, pt.ProjectId });

            modelBuilder.Entity<ProjectTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProjectTags)
                .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<ProjectTag>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTags)
                .HasForeignKey(pt => pt.ProjectId);

            modelBuilder.Entity<TeamTranslator>()
                .HasOne(tt => tt.Team)
                .WithMany(team => team.TeamTranslators)
                .HasForeignKey(tt => tt.TeamId);

            modelBuilder.Entity<TeamTranslator>()
                .HasOne(tt => tt.Translator)
                .WithMany(translator => translator.TeamTranslators)
                .HasForeignKey(tt => tt.TranslatorId);

            modelBuilder.Entity<TranslatorLanguage>()
                .HasKey(tl => new { tl.TranslatorId, tl.LanguageId });

            modelBuilder.Entity<TranslatorLanguage>()
                .HasOne(tl => tl.Language)
                .WithMany()
                .HasForeignKey(tl => tl.LanguageId);

            modelBuilder.Entity<TranslatorLanguage>()
                .HasOne(tl => tl.Translator)
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
            
            //new feature in EF Core 2.1
            //modelBuilder.Seed();
            
            

        }
    }
}
