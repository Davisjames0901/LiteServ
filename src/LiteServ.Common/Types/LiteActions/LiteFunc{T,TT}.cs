using System;
using LiteServ.Common.Serialization;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionRequestset;
using LiteServ.Common.Types.ExecutionResult;

namespace LiteServ.Common.Types.LiteActions
{
    public class LiteFunc<T, TT> : ILiteActionBase
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

            var result = Execute(request.Content);
            return new ExecutionResult<TT>(result);
            ;
        }

        public SerializationContext GetSerializationContext(ISerializer serializer)
        {
            return new SerializationContext
            {
                CreateRequest = x => CreateRequest(x, serializer),
                CreateResponse = x => CreateResponse(x, serializer)
            };
        }

        private RequestSerializationResult CreateRequest(string message, ISerializer serializer)
        {
            try
            {
                var request = serializer.Deserialize<T>(message);
                return new RequestSerializationResult
                {
                    Request = new ExecutionRequest<T>
                    {
                        Content = request
                    },
                    Status = SerializationStatus.Ok
                };
            }
            catch (Exception)
            {
                return new RequestSerializationResult
                {
                    Request = null,
                    Status = SerializationStatus.Error
                };
            }
        }

        private ResponseSerializationResult CreateResponse(IExecutionResultBase resultBase, ISerializer serializer)
        {
            var result = resultBase as ExecutionResult<TT>;
            var request = serializer.Serialize(result.Content);
            return new ResponseSerializationResult
            {
                Response = request,
                Status = SerializationStatus.Ok
            };
        }

        private TT Execute(T obj)
        {
            var resp = _func.Invoke(obj);
            return resp ?? default;
        }
    }
}