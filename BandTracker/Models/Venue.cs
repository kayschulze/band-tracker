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
            cmd.CommandText = @"SELECT * FROM venues;";

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

        public void AddBand(Band newBand)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @venueId);";

            MySqlParameter band_id = new MySqlParameter();
            band_id.ParameterName = "@bandId";
            band_id.Value = newBand.GetId();
            cmd.Parameters.Add(band_id);

            MySqlParameter venue_id = new MySqlParameter();
            venue_id.ParameterName = "@venueId";
            venue_id.Value = _id;
            cmd.Parameters.Add(venue_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Band> GetBands()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT bands.* FROM venues
             JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @venueId;";

            MySqlParameter venueIdParameter = new MySqlParameter();
            venueIdParameter.ParameterName = "@venueId";
            venueIdParameter.Value = _id;
            cmd.Parameters.Add(venueIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Band> bands = new List<Band> {};

            while(rdr.Read())
            {
                int thisBandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);
                string thisBandManager = rdr.GetString(2);
                string bandManagerPhoneNumber = rdr.GetString(3);
                string thisBandLeader = rdr.GetString(4);
                string bandLeaderPhoneNumber = rdr.GetString(5);

                Band newBand = new Band(bandName, thisBandManager, bandManagerPhoneNumber, thisBandLeader, bandLeaderPhoneNumber, thisBandId);

                bands.Add(newBand);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return bands;
        }

        public static Venue Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM venues WHERE id = (@venueId);";

            MySqlParameter venueIdParameter = new MySqlParameter();
            venueIdParameter.ParameterName = "@venueId";
            venueIdParameter.Value = id;
            cmd.Parameters.Add(venueIdParameter);

            int venueId = 0;
            string venueName = "";
            string venueContact = "";
            string venuePhoneNumber = "";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                venueId = rdr.GetInt32(0);
                venueName = rdr.GetString(1);
                venueContact = rdr.GetString(2);
                venuePhoneNumber = rdr.GetString(3);
            }

            Venue newVenue = new Venue(venueName, venueContact, venuePhoneNumber, venueId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newVenue;
        }

        public void UpdateVenueName(string newVenueName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE venues SET name = @newVenueName WHERE id = @findId;";

            MySqlParameter findId = new MySqlParameter();
            findId.ParameterName = "@findId";
            findId.Value = _id;
            cmd.Parameters.Add(findId);

            MySqlParameter venueNameParameter = new MySqlParameter();
            venueNameParameter.ParameterName = "@newVenueName";
            venueNameParameter.Value = newVenueName;
            cmd.Parameters.Add(venueNameParameter);

            cmd.ExecuteNonQuery();
            _name = newVenueName;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM venues WHERE id = @venueId; DELETE FROM bands_venues WHERE venue_id = @venueId;", conn);

            MySqlParameter venueIdParameter = new MySqlParameter();
            venueIdParameter.ParameterName = "@venueId";
            venueIdParameter.Value = id;
            cmd.Parameters.Add(venueIdParameter);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
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
