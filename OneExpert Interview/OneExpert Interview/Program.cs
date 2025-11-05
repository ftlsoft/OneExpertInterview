namespace OneExpertInterview
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: Initialize DI container, services, and repository
            // TODO: Demonstrate multi-threaded order processing
            Console.WriteLine("Order Processing System");

            // Example: Simulate multiple threads processing orders
            Task[] tasks = new Task[3];
            tasks[0] = Task.Run(() => { /* Call ProcessOrder(1) */ });
            tasks[1] = Task.Run(() => { /* Call ProcessOrder(2) */ });
            tasks[2] = Task.Run(() => { /* Call ProcessOrder(-1) */ });
            Task.WaitAll(tasks);

            Console.WriteLine("Processing complete.");
        }
    }
}
