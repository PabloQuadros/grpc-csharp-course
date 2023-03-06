using Grpc.Core;
using Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Math.sumService;

namespace server
{
    public class SumServiceImpl : sumServiceBase
    {
        public override Task<sumResponse> math(sumRequest request, ServerCallContext context)
        {
            Int32 result = request.Sum.Num1 + request.Sum.Num2;
            return Task.FromResult(new sumResponse() { Result = result });
        }
    }
}
