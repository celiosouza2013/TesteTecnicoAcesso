using Api.Transferencia.Data;
using Api.Transferencia.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Transferencia.Repositories
{
    /// <summary>
    /// Classe responsável por persistir informações de transferência
    /// </summary>
    public class TransferRepository : ITransferRepository
    {
        private readonly DataContext _context;
        public TransferRepository(DataContext context) => _context = context;
        
        /// <summary>
        /// Insere dados da Tranferência para historioco de transações
        /// </summary>
        /// <param name="transferInputDto"></param>
        /// <returns></returns>
        public async Task<TransferInput> AddAsync(TransferInputDto transferInputDto)
        {
            try
            {
                TransferInput transferInput = new TransferInput
                {
                    Id = Guid.NewGuid(),
                    AccountDestination = transferInputDto.AccountDestination,
                    AccountOrigin = transferInputDto.AccountOrigin,
                    Value = transferInputDto.Value
                };

                var logged = await _context.TransferInputs.AddAsync(transferInput);
                _context.SaveChanges();
                return transferInput;    
            }
            catch { throw; }
        }
       
        /// <summary>
        /// Lista os dados de transferencias salvos no histórico
        /// </summary>
        /// <returns></returns>
        public async Task<List<TransferInput>> GetTransferInputsAsync()
        {
            try
            {
                return await _context.TransferInputs.ToListAsync();
            }
            catch { throw; }
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public TransferInput GetTransferInputByAccountOrigin(string accountOrigin)
        {
            throw new NotImplementedException();
        }

       
    }
}
