namespace ClinicaUniversitaria
{
    public partial class ServicesPage : ContentPage
    {
        public ServicesPage()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Lógica para regresar a la página anterior
            await Navigation.PopAsync();
        }
    }
}
