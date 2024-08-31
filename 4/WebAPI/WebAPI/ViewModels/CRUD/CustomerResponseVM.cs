namespace WebAPI.ViewModels.CRUD
{
    //public class CustomerResponseVM
    //{
    //    public int CustomerId { get; set; }  // Ini sebaiknya diabaikan saat POST
    //    public string? CustomerCode { get; set; }
    //    public string? CustomerName { get; set; }
    //    public string CustomerAddress { get; set; } = string.Empty;  // Default value sesuai dengan database
    //    public int CreatedBy { get; set; }  // Pastikan ini adalah int
    //    public DateTime CreatedAt { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime? ModifiedAt { get; set; }
    //}

    public class Customer
    {
        public int CustomerId { get; set; }  // Primary Key
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerAddress { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }


    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public T? Data { get; set; }

        public ApiResponse(string message, string transactionId, T? data = default)
        {
            Message = message;
            TransactionId = transactionId;
            Data = data;
        }
    }

}
