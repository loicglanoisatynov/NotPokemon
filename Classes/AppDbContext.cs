using Microsoft.EntityFrameworkCore;
using PokeWish.Classes;

namespace PokeWish.Classes
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Login>? Login { get; set; }
        public DbSet<Player>? Players { get; set; }
        public DbSet<Monster>? Monsters { get; set; }
        public DbSet<Spell>? Spells { get; set; }
        public DbSet<MonsterSpell>? MonsterSpells { get; set; }

        public DbSet<PlayerMonster>? PlayerMonsters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration pour MonsterSpell
            modelBuilder.Entity<MonsterSpell>()
                .HasKey(ms => new { ms.MonsterID, ms.SpellID });

            modelBuilder.Entity<MonsterSpell>()
                .HasOne(ms => ms.Monster)
                .WithMany(m => m.MonsterSpells)
                .HasForeignKey(ms => ms.MonsterID);

            modelBuilder.Entity<MonsterSpell>()
                .HasOne(ms => ms.Spell)
                .WithMany(s => s.MonsterSpells)
                .HasForeignKey(ms => ms.SpellID);

            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Monster>().ToTable("Monster");
            modelBuilder.Entity<Spell>().ToTable("Spell");
            //modelBuilder.Entity<Monster>().ToTable("PlayerMonster");
            modelBuilder.Entity<PlayerMonster>().ToTable("PlayerMonster");
            modelBuilder.Entity<PlayerMonster>().ToTable("PlayerMonster")
                .HasKey(pm => new { pm.PlayerID, pm.MonsterID });

            modelBuilder.Entity<PlayerMonster>().ToTable("PlayerMonster")
                .HasOne(pm => pm.Player)
                .WithMany(p => p.PlayerMonsters)
                .HasForeignKey(pm => pm.PlayerID);

            modelBuilder.Entity<PlayerMonster>().ToTable("PlayerMonster")
                .HasOne(pm => pm.Monster)
                .WithMany(m => m.PlayerMonsters)
                .HasForeignKey(pm => pm.MonsterID);

            // Configuration des relations MonsterSpell
            modelBuilder.Entity<MonsterSpell>()
                .HasKey(ms => new { ms.MonsterID, ms.SpellID });

            modelBuilder.Entity<MonsterSpell>()
                .HasOne(ms => ms.Monster)
                .WithMany(m => m.MonsterSpells)
                .HasForeignKey(ms => ms.MonsterID);

            modelBuilder.Entity<MonsterSpell>()
                .HasOne(ms => ms.Spell)
                .WithMany(s => s.MonsterSpells)
                .HasForeignKey(ms => ms.SpellID);

        }



    }
}
