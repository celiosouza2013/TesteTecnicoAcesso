using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Transferencia.Models
{
    public class TransferInput
    {
        public Guid Id { get; set; }
        [Required]
        public string AccountOrigin { get; set; }
        [Required]
        public string AccountDestination { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
