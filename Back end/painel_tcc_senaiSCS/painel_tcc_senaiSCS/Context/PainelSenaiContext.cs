using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using painel_tcc_senaiSCS.Domains;

#nullable disable

namespace painel_tcc_senaiSCS.Context
{
    public partial class PainelSenaiContext : DbContext
    {
        public PainelSenaiContext()
        {
        }

        public PainelSenaiContext(DbContextOptions<PainelSenaiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CadastrarCampanha> CadastrarCampanhas { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                // Pc do Senai
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PainelSenai"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<CadastrarCampanha>(entity =>
            {
                entity.HasKey(e => e.IdCampanha)
                    .HasName("PK__Cadastra__64DAD299C3E8D436");

                entity.ToTable("CadastrarCampanha");

                entity.Property(e => e.IdCampanha).HasColumnName("idCampanha");

                entity.Property(e => e.Arquivo)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("arquivo");

                entity.Property(e => e.DataFim)
                    .HasColumnType("datetime")
                    .HasColumnName("dataFim");

                entity.Property(e => e.DataInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("dataInicio");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.NomeCampanha)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nomeCampanha");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.CadastrarCampanhas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Cadastrar__idUsu__3B75D760");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TipoUsua__03006BFF513EB198");

                entity.ToTable("TipoUsuario");

                entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");

                entity.Property(e => e.NomeTipoUsuario)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nomeTipoUsuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__645723A69E79EDA4");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");

                entity.Property(e => e.NomeUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nomeUsuario");

                entity.Property(e => e.Senha)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("senha");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Usuario__idTipoU__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
