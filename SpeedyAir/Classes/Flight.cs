using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir
{
    public class Flight
    {
        #region properties
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public List<Order> Orders { get; set; }
        public int SendingDay { get; set; }
        #endregion

        public Flight() {
            Capacity = 20;
            Origin = "YUL";
            Orders = new List<Order>();
        }
    }
}
