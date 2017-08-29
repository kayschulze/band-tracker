using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
    public class Venue
    {
        private string _id;
        private string _name;
        private string _phonenumber;
        private string _venuecontact;

        public Venue(int id, string name, string phonenumber, string venuecontact)
        {
            _id = id;
            _name = name;
            _phonenumber = phonenumber;
            _venuecontact = venuecontact;
        }
    }
}
