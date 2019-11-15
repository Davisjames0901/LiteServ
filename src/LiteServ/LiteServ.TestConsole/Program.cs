using System;
using System.Threading;
using LiteServ.Client;
using LiteServ.Core;

namespace LiteServ.TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            var server = new LiteServServer(typeof(TestHub));
            new Thread(server.Start).Start();
            
            new TestClient().SendTest();
            Console.Read();
        }
    }

    class TestHub : LiteHub
    {
        public override void BuildEndpoints(EndPointBuilder builder)
        {
            builder.Add<string>("home")
                .Ok(Console.WriteLine)
                .Error(err => Console.WriteLine(err.Message));
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