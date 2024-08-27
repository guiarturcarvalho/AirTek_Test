using System.IO.IsolatedStorage;
using System.Linq.Expressions;

namespace SpeedyAir;


public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine($"Beginning at {DateTime.Now.ToString("yyyyMMddHHmmss")}");

        string ordersPath = "C:\\coding-assigment-orders.json";

        List<Flight> scheduledFlights = new List<Flight>();
        List<Flight> extraFlights = new List<Flight>();

        List<Order> orders = Utils.GetOrders(ordersPath);

        #region User Story 1        
        int desiredDays = 2;

        List<Order> ordersToronto = orders.Where(x => x.Destination == "YYZ").ToList();
        List<Order> ordersCalgary = orders.Where(x => x.Destination == "YYC").ToList();
        List<Order> ordersVancouver = orders.Where(x => x.Destination == "YVR").ToList();
        List<Order> extraOrders = orders.Where(x => x.Destination == "YYE").ToList();

        //Simple sending order
        for (int i = 0; i < desiredDays; i++)
        {
            
            scheduledFlights.Add(new Flight
            {
                Destination = "YYZ",
                SendingDay = i + 1,
                Name = $"{(i * 3) + 1}",
                Orders = ordersToronto.Skip(i*20).Take(20).ToList()
            }); 

            scheduledFlights.Add(new Flight
            {
                Destination = "YYC",
                SendingDay = i + 1,
                Name = $"{(i * 3) + 2}",
                Orders = ordersCalgary.Skip(i * 20).Take(20).ToList()
            });

            scheduledFlights.Add(new Flight
            {
                Destination = "YVR",
                SendingDay = i + 1,
                Name = $"{(i * 3) + 3}",
                Orders = ordersVancouver.Skip(i * 20).Take(20).ToList()
            });
        }

        //extra orders
        bool thereAreExtraOrders = true;
        int w = 0;
        while (thereAreExtraOrders)
        {
            extraFlights.Add(new Flight
            {
                Destination = "YYE",                
                Orders = extraOrders.Skip(w * 20).Take(20).ToList()
            });
            w++;

            if (extraFlights.Last().Orders.Count < 20) thereAreExtraOrders = false;
        }

        //replace scheduledFlights with bigger extra orders
        foreach (Flight extra in extraFlights)
        {
            Flight flightToReplace = scheduledFlights.Where(x => x.Orders.Count < extra.Orders.Count).First();

            if (flightToReplace != null)
            {
                var index = scheduledFlights.IndexOf(flightToReplace);
                scheduledFlights[index].Destination = extra.Destination;
                scheduledFlights[index].Orders = extra.Orders;
                extra.Orders = new List<Order>();
            }
            
        }

        //result

        foreach (Flight f in scheduledFlights)
        {
            Console.WriteLine($"Flight: {f.Name}, Departure: {f.Origin}, Arrival: {f.Destination}, Day: {f.SendingDay}");
        }
        #endregion

        #region User story 2


        //resutl        

        foreach (Order order in orders)
        {
            Order foundOrder = null;
            Flight orderFlight = null;

            foreach (Flight flight in scheduledFlights)
            {
                foundOrder = flight.Orders.FirstOrDefault(x => x.Name == order.Name);

                if (foundOrder != null)
                {
                    orderFlight = flight;
                    break;
                }
            }

            if (foundOrder != null)
            {
                Console.WriteLine($"order: {foundOrder.Name}, flightNumber: {orderFlight.Name}, departure: {orderFlight.Origin}, arrival: {orderFlight.Destination}, day: {orderFlight.SendingDay}");
            }
            else
            {
                Console.WriteLine($"Order: {order.Name}, Flight: not scheduled");
            }
        }

        #endregion

        Console.WriteLine($"Ending at {DateTime.Now.ToString("yyyyMMddHHmmss")}");



    }
}