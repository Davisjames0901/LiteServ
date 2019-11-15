using System;

namespace LiteServ.Core
{
    public class Endpoint<T> : IEndpoint
    {
        public string Path { get; set; }
    }
}