using System;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;

namespace LiteServ.Common.Types.LiteActions
{
    public class LiteFunc<T,TT> : ILiteActionBase
    {
        private readonly Func<T, TT> _func;
        
        public LiteFunc(Func<T, TT> func)
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

            var result = Execute(request.Request);
            return new ExecutionResult<TT>(result);;
        }
        
        private TT Execute(T obj)
        {
            var resp = _func.Invoke(obj);
            if (resp == null)
            {
                return default;
            }

            return resp;
        }
    }
}