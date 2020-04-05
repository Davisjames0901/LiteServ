using System;
using LiteServ.Client;
using LiteServ.Common;
using LiteServ.Common.Attributes;
using LiteServ.Common.Types;
using LiteServ.Common.Types.Hubs;
using LiteServ.Server;

namespace LiteServ.TestConsole
{
    internal static class Program
    {
        internal static void Main()
        {
            var server = new LiteServServer();
            server.Start();
            var testString = server.Process(new RequestPacket
            {
                Content = "\"Sup Fool\"",
                Path = "test/home",
                ClientId = Guid.NewGuid()
            });
            var testInt = server.Process(new RequestPacket
            {
                Content = "12",
                Path = "test/home2",
                ClientId = Guid.NewGuid()
            });
        }
    }

    [Route("test")]
    internal class TestHub : LiteHub
    {
        public string Route => "test";

        // public override void BuildEndpoints(EndpointBuilder builder)
        // {
        //     builder.Add<string>("home")
        //         .On<int, string>(x =>
        //         {
        //             Console.WriteLine(x);
        //             return x.ToString();
        //         });
        //
        //     builder.Add<int>("home2")
        //         .On<string, int>(x =>
        //         {
        //             Console.WriteLine(x);
        //             return int.Parse(x);
        //         });
        // }
        [Action("home2")]
        public string HomeTest(string param)
        {
            Console.WriteLine(param);
            return param;
        }

        [Action("home2")]
        public int Home2Test(string param)
        {
            Console.WriteLine(param);
            return int.Parse(param);
        }
    }

    internal class TestClient
    {
        public void SendTest()
        {
            var client = new LiteServClient();
            client.Send("home", "Hi There");
        }
    }
    
}