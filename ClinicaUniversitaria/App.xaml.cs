using Microsoft.Maui.Controls;
using ClinicaUniversitaria.Views;

namespace ClinicaUniversitaria
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establece la página principal como una NavigationPage
            MainPage = new NavigationPage(new MainPage());
        }
    }
}