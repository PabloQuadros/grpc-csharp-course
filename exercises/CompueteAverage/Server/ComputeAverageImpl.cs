using ComputeAverage;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComputeAverage.ComputeAverageServices;

namespace Server
{
    public class ComputeAverageImpl : ComputeAverageServicesBase
    {
        public override async Task<AverageResponse> calcAverage(IAsyncStreamReader<AverageRequest> requestStream, ServerCallContext context)
        {
            int sum = 0;
            int cont = 0;
           

            while (await requestStream.MoveNext())
            {
                sum += requestStream.Current.Num.Num_;
                cont++;
            }

            return new AverageResponse() { Result= sum/cont };
        }
    }
}
