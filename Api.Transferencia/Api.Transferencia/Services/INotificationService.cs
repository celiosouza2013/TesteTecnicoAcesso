using Api.Transferencia.Models;

namespace Api.Transferencia.Services
{
    public interface INotificationService
    {
        void NotifyTransference(TransferOutput dataTransfer);
    }
}
