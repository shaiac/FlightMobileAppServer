using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMobileApp.Models
{
    public interface ICommandsManager
    {
        public void ConnectToSimulator(string serverIp, int port, int innerPort);
        public void start();
        public void getImage();
        public void SetCommandValues(Command command);
    }
}
