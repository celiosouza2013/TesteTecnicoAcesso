using System.ComponentModel.DataAnnotations;

namespace Api.Transferencia.Models
{
    /// <summary>
    /// Dados de entrada na fila para tranferência
    /// </summary>
    public class TransferInputDto
    {
        [Required]
        public string AccountOrigin { get; set; }
        [Required]
        public string AccountDestination { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
