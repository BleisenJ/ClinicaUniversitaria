using CommunityToolkit.Mvvm.ComponentModel;
namespace ClinicaUniversitaria.DTOs
{
    public partial class MedicosDTO : ObservableObject
    {
        [ObservableProperty]
        public int idMedico;
        [ObservableProperty]
        public string nombreCompleto;
        [ObservableProperty]
        public string profesion;
        [ObservableProperty]
        public int correo;
    }
}
