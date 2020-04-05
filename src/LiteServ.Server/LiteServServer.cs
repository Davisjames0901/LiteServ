using System;
using System.Collections.Generic;
using System.Linq;
using LiteServ.Common.Serialization;
using LiteServ.Common.Types;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;
using LiteServ.Common.Types.Hubs;
using LiteServ.Common.Types.LiteActions;

namespace LiteServ.Server
{
    public class LiteServServer
    {
        private RoutingContext _routes;
        public void Start()
        {
            var hubs = GetHubs();
            var routeBuilders = hubs
                .Select(x=>Activator.CreateInstance(x)as LiteHub)
                .Select(x=> x.Build());
            _routes= new RoutingContext(routeBuilders);

        }

        public ResponsePacket Process(RequestPacket packet)
        {
            var executionContext = _routes.GetExecutionContext(packet.Path, new JsonSerializer());
            var request = executionContext.SerializationContext.CreateRequest(packet.Content);
            
            if (request.Status != SerializationStatus.Ok)
            {
                return BuildSerializationErrorPacket(request.Status, packet.ClientId);
            }
            
            var result = Execute(executionContext.Action, request.Request);
            var response = executionContext.SerializationContext.CreateResponse(result);
            
            return response.Status != SerializationStatus.Ok ? BuildSerializationErrorPacket(response.Status, packet.ClientId) : BuildPacket(Status.Ok, response.Response, packet.ClientId);
        }

        //Todo: we should be using DI for this but I am lazy and will do it later when it matters more :D
        private IEnumerable<Type> GetHubs()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(LiteHub).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
        }

        private IExecutionResultBase Execute(ILiteActionBase action, IExecutionRequestBase request)
        {
            IExecutionResultBase result;
            try
            {
                result = action.Execute(request);
            }
            catch (Exception e)
            {
                result = new ExecutionResult
                {
                    Error = new Error
                    {
                        Message = e.Message
                    },
                    Status = Status.InternalError
                };
            }

            return result;
        }

        private ResponsePacket BuildSerializationErrorPacket(SerializationStatus status, Guid clientId)
        {
            return status switch
            {
                SerializationStatus.Error => BuildPacket(Status.InternalError, "There was an issue deserializing", clientId),
                SerializationStatus.NotApplicable => BuildPacket(Status.InvalidType, "No handle for that type", clientId),
                _ => null
            };
        }

        private ResponsePacket BuildPacket(Status status, string content, Guid clientId)
        {
            return new ResponsePacket
            {
                Checksum = content.Sum(x=> (byte)x),
                ClientId = clientId,
                Content = content,
                Status = status
            };
        }
        
        
    }
}