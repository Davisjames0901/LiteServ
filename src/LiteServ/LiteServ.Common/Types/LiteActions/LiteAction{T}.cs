using System;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;

namespace LiteServ.Common.Types.LiteActions
{
    public class LiteAction<T> : ILiteActionBase
    {
        private readonly Action<T> _func;
        
        public LiteAction(Action<T> func)
        {
            _func = func;
        }

        public IExecutionResultBase Execute(IExecutionRequestBase requestBase)
        {
            var request = requestBase as IExecutionRequest<T>;
            if (request == null)
            {
                throw new Exception("Invalid request");
            }

            Execute(request.Request);
            return new ExecutionResult.ExecutionResult();;
        }
        
        private void Execute(T obj)
        {
            _func.Invoke(obj);
        }
    }
}