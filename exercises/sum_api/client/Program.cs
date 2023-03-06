using Grpc.Core;
using Math;

namespace client
{
    class Program
    {
        const string target = "127.0.0.1:50052";

        static void Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });

            var client = new sumService.sumServiceClient(channel);

            var sum = new sum()
            {
                Num1 = 3,
                Num2 = 10
                
            };

            var request = new sumRequest() { Sum= sum };

            var response = client.math(request);

            Console.WriteLine(response.Result);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}