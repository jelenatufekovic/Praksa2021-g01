using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Person
{
    public class RefereeRest : PersonRestBase
    {
        public int Rating { get; set; }
    }
}