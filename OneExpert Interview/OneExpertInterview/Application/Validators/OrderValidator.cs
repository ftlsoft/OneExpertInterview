using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Application.Validators
{
    public class OrderValidator : IOrderValidator
    {
        public bool IsValid(int orderId)
        {
            return orderId > 0;
        }

        public bool IsValid(Order order)
        {
            if (order == null) return false;
            if (order.Id <= 0) return false;
            if (string.IsNullOrWhiteSpace(order.Description)) return false;

            return true;
        }
    }
}
