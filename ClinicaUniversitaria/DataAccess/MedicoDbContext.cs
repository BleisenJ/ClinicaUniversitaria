using ClinicaUniversitaria.Modelo;
using ClinicaUniversitaria.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace ClinicaUniversitaria.DataAccess
{
    public class MedicoDbContext : DbContext
    {
        public DbSet<Medico>Medicos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("Medicos.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(col => col.idMedico);
                //Aqui abajo, lo que pasa es que cuando se agregue un medico nuevo,
                //se va a generar un id para cada uno y no se repita en la base de datos :3
                entity.Property(col => col.idMedico).IsRequired().ValueGeneratedOnAdd();
            });
        }

    }
}
