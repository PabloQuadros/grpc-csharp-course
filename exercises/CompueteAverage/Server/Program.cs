using Grpc.Core;
using ComputeAverage;
using static Grpc.Core.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace server
{
    class Program
    {
        const int Port = 50052;

        static async Task Main(string[] args)
        {
            Grpc.Core.Server server = null;

            try
            {
                server = new Grpc.Core.Server()
                {
                    Services = { ComputeAverageServices.BindService(new ComputeAverageImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };

                server.Start();
                Console.WriteLine("The server is listening on the port : " + Port);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("The server failed to start : " + e.Message);
                throw;
            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}