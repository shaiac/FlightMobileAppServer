using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightMobileApp.Models
{
    public class CommandsManager : ICommandsManager
    {
        IClient simulator;
        IClient innerSimulator;
        Command commandValues;
        bool[] isChanged = { false, false, false, false };
        Dictionary<string, string> setStrings;
        Dictionary<string, string> getStrings;

        public CommandsManager()
        {
            this.commandValues = new Command();
            this.simulator = new MyClient();
            this.innerSimulator = new MyClient();
            createGetStrings();
            createSetStrings();
            this.ConnectToSimulator("127.0.0.1", 5403, 0);
        }
        public void ConnectToSimulator(string serverIp, int port, int innerPort)
        {
            simulator.Connect(serverIp, port);
            //innerSimulator.Connect(serverIp, innerPort);
            simulator.Write("data\n");
            this.start();
        }

        public void start()
        {
            new Thread(delegate () {
                string setCommand;
                while (true)
                {
                    if (isChanged[0])
                    {
                        setStrings.TryGetValue("throttle", out setCommand);
                        setCommand += this.commandValues.Throttle.ToString() + "\r\n";
                        simulator.Write(setCommand);
                        readResponseAndCheck(this.commandValues.Throttle, "throttle");
                        isChanged[0] = false;
                    }
                    if (isChanged[1])
                    {
                        setStrings.TryGetValue("aileron", out setCommand);
                        setCommand += this.commandValues.Aileron.ToString() + "\r\n";
                        simulator.Write(setCommand);
                        readResponseAndCheck(this.commandValues.Aileron, "aileron");
                        isChanged[1] = false;
                    }
                    if (isChanged[2])
                    {
                        setStrings.TryGetValue("elevator", out setCommand);
                        setCommand += this.commandValues.Elevator.ToString() + "\r\n";
                        simulator.Write(setCommand);
                        readResponseAndCheck(this.commandValues.Elevator, "elevator");
                        isChanged[2] = false;
                    }
                    if (isChanged[3])
                    {
                        setStrings.TryGetValue("rudder", out setCommand);
                        setCommand += this.commandValues.Rudder.ToString() + "\r\n";
                        simulator.Write(setCommand);
                        readResponseAndCheck(this.commandValues.Rudder, "rudder");
                        isChanged[3] = false;
                    }
                }
            }).Start();
        }

        private void readResponseAndCheck(double value, string key)
        {
            string message, getCommand;
            getStrings.TryGetValue(key, out getCommand);
            simulator.Write(getCommand);
            message = simulator.Read();
            double rec = Double.Parse(message);
            if (value != Double.Parse(message))
            {
                //write to the client error message
            }
        }

        public void getImage()
        {
            this.innerSimulator.Write("get HOST:PORT/screenshot");
        }

        private void createSetStrings()
        {
            this.setStrings = new Dictionary<string, string>();
            this.setStrings.Add("aileron", "set /controls/flight/aileron ");
            this.setStrings.Add("rudder", "set /controls/flight/rudder ");
            this.setStrings.Add("elevator", "set /controls/flight/elevator ");
            this.setStrings.Add("throttle", "set /controls/engines/current-engine/throttle ");
        }

        private void createGetStrings()
        {
            this.getStrings = new Dictionary<string, string>();
            this.getStrings.Add("aileron", "get /controls/flight/aileron\r\n");
            this.getStrings.Add("rudder", "get /controls/flight/rudder\r\n");
            this.getStrings.Add("elevator", "get /controls/flight/elevator\r\n");
            this.getStrings.Add("throttle", "get /controls/engines/current-engine/throttle\r\n");
        }

        public void SetCommandValues(Command command)
        {
            if (this.commandValues.Throttle != command.Throttle)
            {
                this.commandValues.Throttle = command.Throttle;
                isChanged[0] = true;
            }
            if (this.commandValues.Aileron != command.Aileron)
            {
                this.commandValues.Aileron = command.Aileron;
                isChanged[1] = true;
            }
            if (this.commandValues.Elevator != command.Elevator)
            {
                this.commandValues.Elevator = command.Elevator;
                isChanged[2] = true;
            }
            if (this.commandValues.Rudder != command.Rudder)
            {
                this.commandValues.Rudder = command.Rudder;
                isChanged[3] = true;
            }
        }
    }
}
