using System;

namespace RedisTest.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Adres { get; set; }
        public string Adres2 { get; set; }
        public string Adres3 { get; set; }
    }
}
