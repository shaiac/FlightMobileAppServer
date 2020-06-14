using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Threading;

namespace FlightMobileApp.Models
{
    public class MyClient : IClient
    {
        TcpClient client;
        Stream stm;

        public MyClient()
        {
        }

        //Connecting to the server, TCP connection setting the read time out to 10 sec.
        public void Connect(string ip, int port)
        {
            client = new TcpClient();
            try
            {
                Console.WriteLine("Connecting...");
                this.client.Connect(ip, port);
                Console.WriteLine("Connected");
                this.stm = client.GetStream();
            }
            catch
            {
                throw new Exception();
            }
        }

        //Writing to the server.
        public void Write(string command)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(command);
            try
            {
                stm.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                Console.WriteLine("Cannot Write to the server, the server is off");
            }
        }

        public string Read()
        {
            string str = "";
            byte[] bytes = new byte[100];
            try
            {
                stm.Read(bytes, 0, 100);
                str = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            }
            catch
            {
                Console.WriteLine("Cannot Read from the server, the server is off");
            }
            return str;
        }

        //Closing the connection
        public void Disconnect()
        {
            client.Close();
        }
    }
}
