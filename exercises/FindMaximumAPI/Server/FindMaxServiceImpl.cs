using Grpc.Core;
using Max;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Max.FindMaxService;

namespace Server
{
    public class FindMaxServiceImpl : FindMaxServiceBase
    {
        public override async Task findMaximum(IAsyncStreamReader<FindMaxRequest> requestStream, IServerStreamWriter<FindMaxResponse> responseStream, ServerCallContext context)
        {
            int? max = null;
            
            while (await requestStream.MoveNext())
            {
                if (max  == null || max < requestStream.Current.Num )
                {
                    max = requestStream.Current.Num;
                    await responseStream.WriteAsync(new FindMaxResponse() { Max = max.Value });
                }
            }
        }
    }
}
