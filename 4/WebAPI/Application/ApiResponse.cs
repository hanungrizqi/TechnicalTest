using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
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
