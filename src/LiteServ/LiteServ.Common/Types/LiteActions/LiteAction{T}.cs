using System;
using LiteServ.Common.Serialization;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionRequestset;
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

            Execute(request.Content);
            return new ExecutionResult.ExecutionResult();;
        }

        public SerializationContext GetSerializationContext(ISerializer serializer)
        {
            return new SerializationContext
            {
                CreateRequest = x => CreateRequest(x, serializer),
                CreateResponse = x => null
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
            catch (Exception e)
            {
                return new RequestSerializationResult
                {
                    Request = null,
                    Status = SerializationStatus.Error
                };
            }
        }

        private void Execute(T obj)
        {
            _func.Invoke(obj);
        }
    }
}