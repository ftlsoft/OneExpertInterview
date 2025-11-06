using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace OneExpertInterview
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var orderService = ServiceContainer.GetOrderService();

            Console.WriteLine("Order Processing System");

            var tasks = new Task[]
            {
                Task.Run(() => { orderService.ProcessOrderAsync(1); }),
                Task.Run(() => { orderService.ProcessOrderAsync(2); }),
                Task.Run(() => { orderService.ProcessOrderAsync(-1); }),
            };
            await Task.WhenAll(tasks);

            Console.WriteLine("All orders processed.");
        }
    }
}
