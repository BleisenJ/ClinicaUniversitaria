using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ClinicaUniversitaria.DataAccess;
using ClinicaUniversitaria.DTOs;
using ClinicaUniversitaria.Modelo;
using System;
using Microsoft.EntityFrameworkCore;
using ClinicaUniversitaria.Utilidades;

namespace ClinicaUniversitaria.ViewModels
{
    public partial class MedicoViewModel : ObservableObject, IQueryAttributable
    {
        private readonly MedicoDbContext _dbContext;

        public MedicoViewModel()
        {
            _dbContext = new MedicoDbContext(); // Ajusta según cómo configuras tu DbContext
        }

        [ObservableProperty]
        private MedicosDTO medicoDto = new MedicosDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int idMedico;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            idMedico = id;

            if (idMedico == 0)
            {
                TituloPagina = "Nuevo Médico";
            }
            else
            {
                TituloPagina = "Editar Médico";
                LoadingEsVisible = true;

                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.Medicos.FirstOrDefaultAsync(m => m.idMedico == idMedico);
                    if (encontrado != null)
                    {
                        MedicoDto.idMedico = encontrado.idMedico;
                        MedicoDto.NombreCompleto = encontrado.NombreCompleto;
                        MedicoDto.Correo = encontrado.Correo;
                        MedicoDto.Profesion = encontrado.Profesion;
                    }

                    MainThread.BeginInvokeOnMainThread(() => LoadingEsVisible = false);
                });
            }
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            MedicoMensaje mensaje = new MedicoMensaje();

            await Task.Run(async () =>
            {
                if (idMedico == 0)
                {
                    var tbMedico = new Medico
                    {
                        NombreCompleto = MedicoDto.NombreCompleto,
                        Correo = MedicoDto.Correo,
                        Profesion = MedicoDto.Profesion,
                    };

                    _dbContext.Medicos.Add(tbMedico);
                    await _dbContext.SaveChangesAsync();

                    MedicoDto.idMedico = tbMedico.idMedico;
                    mensaje = new MedicoMensaje
                    {
                        EsCrear = true,
                        MedicosDTO = MedicoDto
                    };
                }
                else
                {
                    var encontrado = await _dbContext.Medicos.FirstOrDefaultAsync(m => m.idMedico == idMedico);
                    if (encontrado != null)
                    {
                        encontrado.NombreCompleto = MedicoDto.NombreCompleto;
                        encontrado.Correo = MedicoDto.Correo;
                        encontrado.Profesion = MedicoDto.Profesion;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            });

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                WeakReferenceMessenger.Default.Send(new MedicoMensajeria(mensaje));
                await Shell.Current.Navigation.PopAsync();
            });
        }
    }
}