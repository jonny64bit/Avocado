using System;

namespace Avocado.Database.Models
{
    public class MeterReading
    {
        public int Id { get; set; }
        public DateTimeOffset When { get; set; }
        public int Value { get; set; }

        /////////////
        ///FKs
        /////////////
        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}