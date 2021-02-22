using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Stadium
{
    public class DeleteStadiumRest
    {
        public Guid Id { get; set; }
        public Guid ByUser { get; set; }
    }
}