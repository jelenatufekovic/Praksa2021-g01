using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.ViewModels.Person
{
    public class PlayerViewModel : PersonViewModelBase
    {
        public int PlayerValue { get; set; }
    }
}