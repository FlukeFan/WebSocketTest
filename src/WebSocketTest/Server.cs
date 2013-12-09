using System;
using System.Net;
using Alchemy;
using Alchemy.Classes;

namespace WebSocketTest
{
    public static class Server
    {
        private static WebSocketServer _webSocketServer;

        public static void Start()
        {
            _webSocketServer = new WebSocketServer(8090, IPAddress.Any);
            _webSocketServer.TimeOut = TimeSpan.FromMinutes(3);

            _webSocketServer.OnConnect = OnConnect;
            _webSocketServer.OnDisconnect = OnDisconnect;
            _webSocketServer.OnReceive = OnReceive;

            _webSocketServer.Start();
        }

        private static void Write(string context, UserContext user)
        {
            Console.WriteLine(string.Format("{0} {1} {2}", context, user.ClientAddress, user.DataFrame));
        }

        private static void OnConnect(UserContext user)
        {
            Write("OnConnect", user);
        }

        private static void OnDisconnect(UserContext user)
        {
            Write("OnDisconnect", user);
        }

        private static void OnReceive(UserContext user)
        {
            Write("OnReceive", user);

            var text = user.DataFrame.ToString();

            if (text == "OPENED")
            {
                Console.WriteLine("Sending ...");
                user.Send("RECEIVED");
            }
        }
    }
}
