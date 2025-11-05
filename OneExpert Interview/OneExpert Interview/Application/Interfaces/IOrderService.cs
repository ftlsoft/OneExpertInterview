using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Application.Interfaces
{
    public interface IOrderService
    {
        void ProcessOrder(int orderId);
    }
}
