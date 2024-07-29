using System;
using Microsoft.EntityFrameworkCore;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Server
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Relaciones que existiran entre tablas

            //Facturas a destino
            modelBuilder.Entity<Factura>()
                .HasOne(x => x.Destinos)
                .WithMany()
                .HasForeignKey(x => x.IDDestino);

            //Factura a producto
            modelBuilder.Entity<Factura>()
                .HasOne(x => x.Producto)
                .WithMany()
                .HasForeignKey(x => x.IDProd);

            //(Grupo a cliente)
            modelBuilder.Entity<Cliente>()
                .HasOne(x => x.Grupo)
                .WithMany()
                .HasForeignKey(x => x.Codgru);

            //Razon social a cliente
            modelBuilder.Entity<RazonSocial>()
                .HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.IDCte);

            //Destinos a Razon social
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.RazonSocial)
                .WithMany()
                .HasForeignKey(x => x.ID_Rs);

            //Destinos a Cluster
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Cluster)
                .WithMany()
                .HasForeignKey(x => x.ID_Cluster);

            //Destinos a Ejecutivo
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Ejecutivo)
                .WithMany()
                .HasForeignKey(x => x.ID_Ejecutivo);

            //Destinos a franquicias
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Franquicia)
                .WithMany()
                .HasForeignKey(x => x.ID_Franquicia);
            //Destinos a Reparto
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Reparto)
                .WithMany()
                .HasForeignKey(x => x.ID_Reparto);
            //Destinos a TAD
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.TAD)
                .WithMany()
                .HasForeignKey(x => x.ID_Tad);
            //Destinos a Zona
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Zona)
                .WithMany()
                .HasForeignKey(x => x.ID_Zona);

        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cluster> Cluster { get; set; }
        public DbSet<Destinos> Destinos { get; set; }
        public DbSet<Ejecutivo> Ejecutivo { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Franquicia> Franquicia { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<RazonSocial> RazonSocials { get; set; }
        public DbSet<Reparto> Reparto { get; set; }
        public DbSet<TAD> TAD { get; set; }
        public DbSet<Zona> Zona { get; set; }
    }
}

