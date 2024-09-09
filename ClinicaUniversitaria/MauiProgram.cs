using Microsoft.Extensions.Logging;
//para que conecte a la base de datos
using ClinicaUniversitaria.DataAccess;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ClinicaUniversitaria.ViewModels;
using ClinicaUniversitaria.Views;

namespace ClinicaUniversitaria
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //se hara la conexion con la base de datos
            var dbContext = new MedicoDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<MedicoDbContext>();

            builder.Services.AddTransient<MedicoPage>();
            builder.Services.AddTransient<MedicoViewModel>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
