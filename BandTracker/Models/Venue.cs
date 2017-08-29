using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
    public class Venue
    {
        private int _id;
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

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetPhoneNumber()
        {
            return _phonenumber;
        }

        public string GetVenueContact()
        {
            return _venuecontact;
        }

        public override bool Equals(System.Object otherVenue)
        {
            if (!(otherVenue is Venue))
            {
                return false;
            }
            else
            {
                Venue newVenue = (Venue) otherVenue;
                bool idEquality = this.GetId() == newVenue.GetId();
                bool nameEquality = this.GetName() == newVenue.GetName();
                bool phoneNumberEquality = this.GetPhoneNumber() == newVenue.GetPhoneNumber();
                bool venueContactEquality = this.GetVenueContact() == newVenue.GetVenueContact();

                return (idEquality && nameEquality && phoneNumberEquality && venueContactEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM venues;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
