using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using pokemonAPI.Domain.Models;

namespace pokemonAPI.Persistence.MariaDB
{
    public partial class PokemonContext : DbContext
    {
        public PokemonContext()
        {
        }

        public PokemonContext(DbContextOptions<PokemonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ListPokemon> ListPokemon { get; set; }
        public virtual DbSet<Moves> Moves { get; set; }
        public virtual DbSet<Pokemon> Pokemon { get; set; }
        public virtual DbSet<PokemonMoves> PokemonMoves { get; set; }
        public virtual DbSet<TypeEffectiveness> TypeEffectiveness { get; set; }
        public virtual DbSet<Types> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListPokemon>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("list_pokemon");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Type1)
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Type2)
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e._)
                    .HasColumnName("#")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Moves>(entity =>
            {
                entity.HasKey(e => e.CdMove)
                    .HasName("PRIMARY");

                entity.ToTable("moves");

                entity.HasIndex(e => e.CdType)
                    .HasName("cd_type_fk");

                entity.Property(e => e.CdMove)
                    .HasColumnName("cd_move")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CdType)
                    .HasColumnName("cd_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NmCategory)
                    .IsRequired()
                    .HasColumnName("nm_category")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NmMove)
                    .IsRequired()
                    .HasColumnName("nm_move")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NrAcc)
                    .HasColumnName("nr_acc")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NrPp)
                    .HasColumnName("nr_pp")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NrPwr)
                    .HasColumnName("nr_pwr")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CdTypeNavigation)
                    .WithMany(p => p.Moves)
                    .HasForeignKey(d => d.CdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cd_type_fk");
            });

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.HasKey(e => e.CdPoke)
                    .HasName("PRIMARY");

                entity.ToTable("pokemon");

                entity.HasIndex(e => e.CdType1)
                    .HasName("type1_fk");

                entity.HasIndex(e => e.CdType2)
                    .HasName("type2_fk");

                entity.HasIndex(e => e.NmPoke)
                    .HasName("nm_poke");

                entity.Property(e => e.CdPoke)
                    .HasColumnName("cd_poke")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CdType1)
                    .HasColumnName("cd_type1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CdType2)
                    .HasColumnName("cd_type2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'19'");

                entity.Property(e => e.NmPoke)
                    .IsRequired()
                    .HasColumnName("nm_poke")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CdType1Navigation)
                    .WithMany(p => p.PokemonCdType1Navigation)
                    .HasForeignKey(d => d.CdType1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("type1_fk");

                entity.HasOne(d => d.CdType2Navigation)
                    .WithMany(p => p.PokemonCdType2Navigation)
                    .HasForeignKey(d => d.CdType2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("type2_fk");
            });

            modelBuilder.Entity<PokemonMoves>(entity =>
            {                
                entity.HasKey(e => new {e.CdMove, e.CdPoke});

                entity.ToTable("pokemon_moves");

                entity.HasIndex(e => e.CdMove)
                    .HasName("cd_move_fk");

                entity.HasIndex(e => e.CdPoke)
                    .HasName("cd_poke_fk");

                entity.Property(e => e.CdMove)
                    .HasColumnName("cd_move")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CdPoke)
                    .HasColumnName("cd_poke")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CdMoveNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CdMove)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cd_move_fk");

                entity.HasOne(d => d.CdPokeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CdPoke)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cd_poke_fk");
            });

            modelBuilder.Entity<TypeEffectiveness>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("type_effectiveness");

                entity.HasIndex(e => e.CdAtkType)
                    .HasName("atk_type_fk");

                entity.HasIndex(e => e.CdDefType)
                    .HasName("def_type_fk");

                entity.Property(e => e.CdAtkType)
                    .HasColumnName("cd_atk_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CdDefType)
                    .HasColumnName("cd_def_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NrEffectiveness)
                    .HasColumnName("nr_effectiveness")
                    .HasColumnType("decimal(2,1)")
                    .HasDefaultValueSql("'1.0'");

                entity.HasOne(d => d.CdAtkTypeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CdAtkType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("atk_type_fk");

                entity.HasOne(d => d.CdDefTypeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CdDefType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("def_type_fk");
            });

            modelBuilder.Entity<Types>(entity =>
            {
                entity.HasKey(e => e.CdType)
                    .HasName("PRIMARY");

                entity.ToTable("types");

                entity.Property(e => e.CdType)
                    .HasColumnName("cd_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NmType)
                    .IsRequired()
                    .HasColumnName("nm_type")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
