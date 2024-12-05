namespace project_sem4_api.Payment
{
    public class PaymentRequest
    {
        public int TransactionId { get; set; }
        public string OrderInfo { get; set; }
        public decimal Amount { get; set; }
    }
}
