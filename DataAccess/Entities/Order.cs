using System;

namespace RedisTest.DataAccess.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerID { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
    }
}
