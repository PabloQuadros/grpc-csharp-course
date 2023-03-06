using Grpc.Core;
using NumberDecomposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static NumberDecomposition.NumberDecompositionServices;

namespace Server
{
    public class NumDecomposeImpl : NumberDecompositionServicesBase
    {
        public override async Task numDecompose(NumToDecomposeRequest request, IServerStreamWriter<NumToDecomposeResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("The server received the request: ");
            Console.WriteLine(request.NumToDecompose.ToString());
            int k = 2;
            int N = request.NumToDecompose.Num;

            while (N > 1)
            {
                if (N%k ==0)
                {
                    await responseStream.WriteAsync(new NumToDecomposeResponse() { Result = k });
                    N = N / k;
                }
                else
                {
                    k = k + 1;
                }
            }

        }
    }
}
