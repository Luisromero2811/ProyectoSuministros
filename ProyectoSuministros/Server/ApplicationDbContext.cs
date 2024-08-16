using System;
using Microsoft.EntityFrameworkCore;
using ProyectoSuministros.Shared.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

            //Estado a facturas
            modelBuilder.Entity<Factura>()
                .HasOne(x => x.Estado)
                .WithMany()
                .HasForeignKey(x => x.Codest);

            //Facturas a destino
            modelBuilder.Entity<Factura>()
                .HasOne(x => x.Destinos)
                .WithMany()
                .HasPrincipalKey(x => x.IDGob)
                .HasForeignKey(x => x.IDDestino);

            //Factura a producto
            modelBuilder.Entity<Factura>()
                .HasOne(x => x.Producto)
                .WithMany()
                .HasForeignKey(x => x.IDProd);

            //(Grupo a cliente)
            modelBuilder.Entity<Grupo>()
                .HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.CodCte);

            //Razon social a cliente
            modelBuilder.Entity<RazonSocial>()
                .HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.IDCte);

            //Destinos a Razon social
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.RazonSocial)
                .WithMany()
                .HasForeignKey(x => x.CodRs);

            //Destinos a Cluster
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Cluster)
                .WithMany()
                .HasForeignKey(x => x.IDCluster);

            //Destinos a Ejecutivo
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Ejecutivo)
                .WithMany()
                .HasForeignKey(x => x.IDEjecutivo);

            //Destinos a franquicias
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Franquicia)
                .WithMany()
                .HasForeignKey(x => x.IDFranquicia);
            //Destinos a Reparto
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Reparto)
                .WithMany()
                .HasForeignKey(x => x.IDReparto);
            //Destinos a TAD
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.TAD)
                .WithMany()
                .HasForeignKey(x => x.IDTad);
            //Destinos a Zona
            modelBuilder.Entity<Destinos>()
                .HasOne(x => x.Zona)
                .WithMany()
                .HasForeignKey(x => x.IDZona);

        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cluster> Cluster { get; set; }
        public DbSet<Destinos> Destinos { get; set; }
        public DbSet<Ejecutivo> Ejecutivo { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Franquicia> Franquicia { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<RazonSocial> Razonsocial { get; set; }
        public DbSet<Reparto> Reparto { get; set; }
        public DbSet<TAD> TAD { get; set; }
        public DbSet<Zona> Zona { get; set; }
    }
}

