using OneExpertInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Application.Interfaces
{
    public interface IOrderValidator
    {
        public bool IsValid(int orderId);

        public bool IsValid(Order order);
    }
}
