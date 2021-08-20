using System;

namespace Api.Transferencia.Services
{
    /// <summary>
    /// Resultado do envio de item para fila
    /// </summary>
    public class ResultPublish
    {
        public bool SuccessfullyExecuted { get; set; }        
        public string Message { get; set; }        
    }
}
