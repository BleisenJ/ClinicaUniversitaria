using ClinicaUniversitaria.Views;
using Microsoft.Maui.Controls;

namespace ClinicaUniversitaria
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnServicesButtonClicked(object sender, EventArgs e)
        {
            // Navega a la página de Servicios
            await Navigation.PushAsync(new ServicesPage());
        }

        private async void OnMedicoFormButtonClicked(object sender, EventArgs e)
        {
            // Navega al formulario de Médicos
            await Navigation.PushAsync(new MedicoPage());
        }
    }
}