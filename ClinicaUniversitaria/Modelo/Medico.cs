using System.ComponentModel.DataAnnotations;
namespace ClinicaUniversitaria.Modelo
{
    public class Medico
    {
        [Key]
        public int idMedico { get; set; }
        public string NombreCompleto { get; set; }
        public string Profesion { get; set; }
        public int Correo { get; set; }
        
    }
}
