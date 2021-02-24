using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.ViewModels.Person
{
    public class PersonViewModelBase : AdminViewModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}