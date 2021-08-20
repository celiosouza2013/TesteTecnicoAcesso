using Api.Transferencia.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Transferencia.Repositories
{
    public interface ITransferRepository
    {
        Task<TransferInput> AddAsync(TransferInputDto transferInput);
        void Delete(string id);
        Task <List<TransferInput>> GetTransferInputsAsync();
        TransferInput GetTransferInputByAccountOrigin(string accountOrigin);
    }
}