using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMobileApp.Models
{
    public class Command
    {
        public double Aileron { set; get; }

        public double Rudder { set; get; }

        public double Elevator { set; get; }

        public double Throttle { set; get; }
    }
}
