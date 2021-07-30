using System.Collections.Generic;

namespace Avocado.Database.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /////////////
        ///FKs
        /////////////
        public List<MeterReading> MeterReadings { get; set; }
    }
}