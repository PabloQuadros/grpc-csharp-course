using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Max;

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

            var client = new FindMaxService.FindMaxServiceClient(channel);
            var stream = client.findMaximum();

            var responseReaderTask = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine(stream.ResponseStream.Current.Max);
                }
            });

            int[] number = { 1, 5, 3, 6, 2, 20 };

            foreach(var numberItem in number) 
            {
                await stream.RequestStream.WriteAsync(new FindMaxRequest() { Num = numberItem });
            }

            await stream.RequestStream.CompleteAsync();
            await responseReaderTask;


            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}