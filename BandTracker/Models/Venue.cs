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

        public Venue(string name, string phonenumber, string venuecontact, int id = 0)
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

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO venues(name, phone_number, venue_contact) VALUES (@name, @phoneNumber, @venueContact);";

            MySqlParameter nameParameter = new MySqlParameter();
            nameParameter.ParameterName = "@name";
            nameParameter.Value = _name;
            cmd.Parameters.Add(nameParameter);

            MySqlParameter phoneNumberParameter = new MySqlParameter();
            phoneNumberParameter.ParameterName = "@phoneNumber";
            phoneNumberParameter.Value = _phonenumber;
            cmd.Parameters.Add(phoneNumberParameter);

            MySqlParameter venueContactParameter = new MySqlParameter();
            venueContactParameter.ParameterName = "@venueContact";
            venueContactParameter.Value = _venuecontact;
            cmd.Parameters.Add(venueContactParameter);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Venue> GetAll()
        {
            List<Venue> allVenues = new List<Venue> {};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "@SELECT * FROM venues;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int VenueId = rdr.GetInt32(0);
                string VenueName = rdr.GetString(1);
                string VenuePhoneNumber = rdr.GetString(2);
                string VenueContact = rdr.GetString(3);

                Venue newVenue = new Venue(VenueName, VenuePhoneNumber, VenueContact, VenueId);

                allVenues.Add(newVenue);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allVenues;
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
