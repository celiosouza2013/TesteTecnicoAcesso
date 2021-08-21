using System.ComponentModel.DataAnnotations;

namespace Api.Transferencia.Models
{
    /// <summary>
    /// Dados de entrada para persistência, envio a fila e realização de transferência
    /// </summary>
    public class TransferInputDto
    {
        /// <summary>
        /// Conta de Origem
        /// </summary>
        [Required]
        public string AccountOrigin { get; set; }
        /// <summary>
        /// Conta de destino
        /// </summary>
        [Required]
        public string AccountDestination { get; set; }
        /// <summary>
        /// Valor a ser tranferido
        /// </summary>
        [Required]
        public int Value { get; set; }
    }
}
