namespace Api.Transferencia.Models
{
    /// <summary>
    /// Dados de Saída da fila para tranferência
    /// </summary>
    public class TransferOutput
    {
        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public int Value { get; set; }
    }
}
