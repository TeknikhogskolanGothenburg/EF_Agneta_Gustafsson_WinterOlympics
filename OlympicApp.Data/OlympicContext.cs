using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OlympicApp.Domain;
using Microsoft.Extensions.Logging.Console;
using System;

namespace OlympicApp.Data
{
    public class OlympicContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Referee> Referees { get; set; }
       // public DbSet<Medal> Medals { get; set; }  // Borttagen i migration 5. Se nedan kommentarer.
        public DbSet<Contestant> Contestants { get; set; }
        public DbSet<Match> Matches { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().ToTable("Country"); // Jag väljer singular för databas namnen.
            modelBuilder.Entity<Sport>().ToTable("Sport");
            modelBuilder.Entity<Contest>().ToTable("Contest");
            modelBuilder.Entity<Referee>().ToTable("Referee");
          //  modelBuilder.Entity<Medal>().ToTable("Medal");
            modelBuilder.Entity<Contestant>().ToTable("Contestant");
            modelBuilder.Entity<Match>().ToTable("ContestContestant");

            modelBuilder.Entity<Sport>()
                .HasMany<Contest>(c => c.Contests)
                .WithOne(s => s.Sport)              
               // .HasForeignKey(f => f.SportId)  //Sport tabellen har inget SportId. Tar bort denna i migration 7
                .OnDelete(DeleteBehavior.Cascade);// if a sport is deleted all Contests should be deleted

            modelBuilder.Entity<Contest>()
             .HasOne<Sport>(c => c.Sport)
             .WithMany(m => m.Contests)
             .HasForeignKey(f => f.SportId)
             .OnDelete(DeleteBehavior.Cascade);// if a Contest is deleted , Sport should remain. Tried with "set Null" first but sql had opinions...
                                                // aha, only children is involved. Sport is the Mother. Changing to Cascade.
            //modelBuilder.Entity<Medal>()
            //    .HasOne<Country>(c => c.Country)  //Jag gjorde ett designfel som jag upptäckte när jag ville lägga in Countries.
            //    .WithOne(m => m.Medal)            //Då MedalId var FK, och jag inte hade, eller kunde skapa medaljer utan att ha landet,
            //    .HasForeignKey<Country>(key => key.MedalId)// så låste de varandra. Valde att ta bort Tabellen och lägga medaljerna i Countrytabeln
            //    .OnDelete(DeleteBehavior.Cascade);// Jag kunde ha reverserat allt och gjort om, men valde att visa att fel kan göras och att man kan
                                                    //rätta via migreringar. P.S. Efter din senaste föreläsning om att backa en migrering mitt i, hade inte
                                                //heller funkat då den innehöll annat, som jag ville ha kvar.            

            modelBuilder.Entity<Referee>()
             .HasOne<Country>(c => c.Country)
             .WithMany(m => m.Referees)
             .HasForeignKey(f => f.CountryId)
             .IsRequired();                        

            modelBuilder.Entity<Contestant>()
                .HasOne<Country>(c => c.Country)
                .WithMany(c => c.Contestants)
                .HasForeignKey(f => f.CountryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contestant>()
                .HasOne<Sport>(c => c.Sport)
                .WithMany(c => c.Contestants)
                .HasForeignKey(f => f.SportId)
                .IsRequired();
            modelBuilder.Entity<Match>().HasKey(m => new { m.ContestId, m.ContestantId }); // var med i migrering 4, men valde att redigera i nr 7

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Contest)
                .WithMany(m => m.Matches)
                .HasForeignKey(m => m.ContestId)
                .OnDelete(DeleteBehavior.Cascade); // tried to read the difference between Cascade and Restrict and base this choise on this conclusion:
                                            // if a Contest is deleted, the Match shall delete. But if a Contestant is deleted the Match shall go on.
            modelBuilder.Entity<Match>()
             .HasOne(m => m.Contestant)
             .WithMany(m => m.Matches)
             .HasForeignKey(m => m.ContestantId)
             .OnDelete(DeleteBehavior.Restrict); //Fan! Tänkte fel i ovanstående med (att det skulle vara Restrict). Ändrar till Cascade i migration 8. Nähä, hjälpte inte. cirkelreferens? tar bort migration 8 Felmeddelande:
                                                //Introducing FOREIGN KEY constraint 'FK_ContestContestant_Contestant_ContestantId' on table 'ContestContestant' may cause 
                                                 //cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
                                                 // Could not create constraint or index. See previous errors.
            modelBuilder.Entity<Match>().Property(m => m.Arena).IsRequired(); //migration 6. I want Arena to be requried. Missed that in original migration.
            base.OnModelCreating(modelBuilder);


            //Brister i databasen::
            // * Upptäckte att inget hindrade att koppla en curlingmatch med en ishockeyspelare.
            // * Referee, insåg när jag testade att det borde funnits en FK till Sport, för att hindra koppling av en obehörig domare. 
           // Försökte migrera in dessa ändringar, genom att lägga till SportID i Match resp Referee, men fick hela tiden felmeddelande, ungefär som
           // meddelandet jag klippte in på rad 82. Förstod inte vad den klagade på och varför det skulle vara ett hinder.
          //
        }

        public static readonly LoggerFactory OlympicLoggerFactory =
           new LoggerFactory(new[]
           {
                new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information, true)
           });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.EnableSensitiveDataLogging()
                //.UseLoggerFactory(OlympicLoggerFactory)
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = OlympicDatabase; Trusted_Connection = True; ");
        }

    }

}
