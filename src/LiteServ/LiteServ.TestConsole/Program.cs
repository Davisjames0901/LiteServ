using System;
using System.Linq;
using System.Threading;
using LiteServ.Client;
using LiteServ.Common;
using LiteServ.Common.Endpoints;
using LiteServ.Common.Types.ExecutionRequestset;
using LiteServ.Server;

namespace LiteServ.TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            var hub = new TestHub();
            var endpointbuilder = new EndPointBuilder();
            hub.BuildEndpoints(endpointbuilder);
            var testString = endpointbuilder.Endpoints["home"].Execute(new ExecutionRequest<string>
            {
                Request = "Sup fool"
            });          
            var testInt = endpointbuilder.Endpoints["home2"].Execute(new ExecutionRequest<string>
            {
                Request = "12"
            });
        }
    }

    class TestHub : LiteHub
    {
        public override void BuildEndpoints(EndPointBuilder builder)
        {
            builder.Add<string>("home")
                .On<string, string>(x =>
                {
                    Console.WriteLine(x);
                    return x;
                });

            builder.Add<int>("home2")
                .On<string, int>(x =>
                {
                    Console.WriteLine(x);
                    return Int32.Parse(x);
                });
        }
    }

    class TestClient
    {
        public void SendTest()
        {
            var client = new LiteServClient();
            client.Send("home", "Hi There");
        }
    }
    
}