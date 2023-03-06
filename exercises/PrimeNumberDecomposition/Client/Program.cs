using Grpc.Core;
using NumberDecomposition;
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

            var client = new NumberDecompositionServices.NumberDecompositionServicesClient(channel);


            var numToDecompose = new NumToDecompose()
            {
                Num = 1200123
            };

            var request = new NumToDecomposeRequest() {  NumToDecompose= numToDecompose };

            var response = client.numDecompose(request);

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}