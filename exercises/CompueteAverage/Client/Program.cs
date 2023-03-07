using Grpc.Core;
using ComputeAverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        const string target = "127.0.0.1:50052";

        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });

            var client = new ComputeAverageServices.ComputeAverageServicesClient(channel);

            Random random = new Random();
            var stream = client.calcAverage();

            for(int i = 0 ; i < 10; i++)
            {
                int n = random.Next(1, 101);
                Num nume = new Num();
                nume.Num_ = n;
                var request = new AverageRequest();
                request.Num = nume;
                await stream.RequestStream.WriteAsync(request);
            }
            await stream.RequestStream.CompleteAsync();
            var response = await stream.ResponseAsync;
            Console.WriteLine(response.Result);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}