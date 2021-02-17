using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Person
{
    public class PlayerRest : PersonRestBase
    {
        public int PlayerValue { get; set; }

    }
}