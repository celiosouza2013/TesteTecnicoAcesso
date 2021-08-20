using Api.Transferencia.Models;
using Api.Transferencia.Repositories;
using Api.Transferencia.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Transferencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IPublishMessage _publishMessage;
        private readonly ITransferRepository _transferRepository;
       public TransferController(IPublishMessage publishMessage,
                                    ITransferRepository transferRepository)
        {
            _publishMessage = publishMessage;
            _transferRepository = transferRepository;
        }

        /// <summary>
        /// Método responsável por gravar informações de entrada e publicar os dados na fila
        /// </summary>
        /// <param name="transfer"></param>
        /// <returns></returns>       
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] TransferInputDto transfer)
        {
            var ret = await _transferRepository.AddAsync(transfer);
            var result = await _publishMessage.SendMessageAsync(transfer);
            if(result.SuccessfullyExecuted) return Ok(result);

            return BadRequest(result.Message);
        }
        [HttpGet]
        [Route("transfers")]
        public async Task<List<TransferInput>> GetTransfers()
        {
            return await _transferRepository.GetTransferInputsAsync();
        }
    }
}
