using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir
{
    public static class Utils
    {
        public static List <Order> GetOrders(string orderPath)
        {
            List <Order> orders = new List <Order>();

            var json = File.ReadAllText(orderPath);

            var obj = JsonConvert.DeserializeObject<JObject>(json);  

            int index = 0;
            foreach (var order in obj)
            {
                orders.Add(new Order
                {
                    Name = order.Key,
                    Destination = (string)order.Value.SelectToken("destination"),
                    Weight = 1
                });

                index++;
            }

            return orders;
        }
    }
}
