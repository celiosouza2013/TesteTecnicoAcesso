using Api.Transferencia.Models;
using System.Threading.Tasks;

namespace Api.Transferencia.Services
{
    public interface IPublishMessage
    {
        Task<ResultPublish> SendMessageAsync(TransferInputDto message);
    }
}