using OneExpertInterview.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Domain.Interfaces
{
    public interface IOrderRepository
    {
        string GetOrder(int orderId);
    }
}
