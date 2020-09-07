using System;
using System.Collections.Generic;
using System.Linq;
using LiteServ.Common;
using LiteServ.Common.Endpoints;
using LiteServ.Common.Serialization;
using LiteServ.Common.Types;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;
using LiteServ.Common.Types.Hubs;
using LiteServ.Common.Types.LiteActions;
using LiteServ.Common.Types.Routing;

namespace LiteServ.Server
{
    public class LiteServServer
    {
        private readonly RoutingContext _routes;

        public LiteServServer(RoutingContext routes)
        {
            _routes = routes;
        }

        public ResponsePacket Process(RequestPacket packet)
        {
            var executionContext = _routes.GetExecutionContext(packet.Path, packet.Content, new JsonSerializer());
            var request = executionContext.SerializationContext.CreateRequest(packet.Content);
            
            if (request.Status != SerializationStatus.Ok)
            {
                return BuildSerializationErrorPacket(request.Status, packet.ClientId);
            }
            
            var result = Execute(executionContext.Action, request.Request);
            var response = executionContext.SerializationContext.CreateResponse(result);
            
            if (response.Status != SerializationStatus.Ok)
            {
                return BuildSerializationErrorPacket(response.Status, packet.ClientId);
            }
            
            return BuildPacket(Status.Ok, response.Response, packet.ClientId);
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
                _ => throw new Exception($"Missing arm of switch {status}")
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