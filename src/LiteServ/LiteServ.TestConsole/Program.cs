using System;
using LiteServ.Client;
using LiteServ.Common;
using LiteServ.Common.Types.ExecutionRequestset;
using LiteServ.Common.Types.Hubs;
using LiteServ.Server;

namespace LiteServ.TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            var server = new LiteServServer();
            server.Start();
            var testString = server.Process("test/home", new ExecutionRequest<string>
            {
                Request = "Sup fool"
            });          
            var testInt = server.Process("test/home2", new ExecutionRequest<string>
            {
                Request = "12"
            });
        }
    }

    class TestHub : LiteHub
    {
        public override string Route => "test";

        public override void BuildEndpoints(EndpointBuilder builder)
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