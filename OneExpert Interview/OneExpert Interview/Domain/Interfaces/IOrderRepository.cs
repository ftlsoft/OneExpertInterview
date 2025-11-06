using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Domain.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        string GetOrder(int orderId);
    }
}
