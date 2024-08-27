using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir
{
    public class Order
    {
        #region properties       

        public string Name { get; set; }
        public string Destination { get; set; }
        public int Weight { get; set; } = 1;
        #endregion
    }
}
