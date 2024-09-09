using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ClinicaUniversitaria.DataAccess;
using ClinicaUniversitaria.DTOs;
using ClinicaUniversitaria.Utilidades;
using ClinicaUniversitaria.Modelo;
using System.Collections.ObjectModel;
using ClinicaUniversitaria.Views;
using ClinicaUniversitaria.ViewModels;


namespace ClinicaUniversitaria.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly MedicoDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<MedicosDTO> listaMedico = new ObservableCollection<MedicosDTO>();

        public MainViewModel(MedicoDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<MedicoMensajeria>(this, (r, m) =>
            {
                MedicoMensajeRecibido(m.Value);
            });
        }

        public async Task Obtener()
        {
            var lista = await _dbContext.Medicos.ToListAsync();
            if(lista.Any())
            {
                foreach(var item in lista)
                {
                    listaMedico.Add(new MedicosDTO
                    {
                        IdMedico = item.idMedico,
                        NombreCompleto = item.NombreCompleto,
                        Correo = item.Correo,
                        Profesion = item.Profesion,
                    });
                }
            }
        }

        private void MedicoMensajeRecibido(MedicoMensaje medicoMensaje)
        {
            var medicoDto = medicoMensaje.MedicosDTO;

            if (medicoMensaje.EsCrear)
            {
                listaMedico.Add(medicoDto);
            }
            else
            {
                var encontrado = listaMedico
                    .First(m => m.IdMedico == medicoDto.IdMedico);

                encontrado.NombreCompleto = medicoDto.NombreCompleto;
                encontrado.Correo = medicoDto.Correo;
                encontrado.Profesion = medicoDto.Profesion;
            }
        }

        //Crear el nuevo medico
        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(MedicoPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        //Editar un perfil de una medico ya existente
        [RelayCommand]
        private async Task Editar(MedicosDTO medicoDto)
        {
            var uri = $"{nameof(MedicoPage)}?id=0{medicoDto.idMedico}";
            await Shell.Current.GoToAsync(uri);
        }

        //Eliminar un perfil de un medico
        [RelayCommand]
        private async Task Eliminar(MedicosDTO medicoDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "¿Desea eliminar el siguiente perfil?", 
                "Si, estoy seguro", "No, volver");

            if(answer)
            {
                var encontrado = await _dbContext.Medicos
                    .FirstAsync(m=> m.idMedico == medicoDto.IdMedico);

                _dbContext.Medicos.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaMedico.Remove(medicoDto);
            }
        }
    }
}
