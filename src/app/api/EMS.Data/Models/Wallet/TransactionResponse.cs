namespace EMS.Data.Models.Wallet
{
    public class TransactionResponse
    {
        public bool IsSuccessful { get; set; } = true;
        public string EmployeeId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}