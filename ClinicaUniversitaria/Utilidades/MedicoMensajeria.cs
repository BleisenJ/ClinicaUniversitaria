using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ClinicaUniversitaria.Utilidades
{
    public class MedicoMensajeria : ValueChangedMessage <MedicoMensaje>
    {
        public MedicoMensajeria(MedicoMensaje value) : base(value)
        {
            
        }
    }
}
