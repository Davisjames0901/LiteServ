using System;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;
using LiteServ.Common.Types.LiteActions;

namespace LiteServ.Common.Endpoints
{
   
    public class Endpoint : IEndpoint
    {
        public ILiteActionBase Action { get; set; }
        
        public void Execute<T>(T obj)
        {
            (Action as Action<T>)?.Invoke(obj);
        }

        public IExecutionResultBase Execute(IExecutionRequestBase requestBase)
        {
            //Hmmmmm
            //var request = requestBase as IExecutionRequest<>
            return default;
        }

        public Type GetReturnType()
        {
            return null;
        }
    }
}