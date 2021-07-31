﻿using System.Collections.Generic;
using Pact.Web.Vue.Grid.Interfaces;

namespace Avocado.Database.Models
{
    public class Account : IDatabaseObject
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