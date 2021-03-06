using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
    public class Band
    {
        private int _id;
        private string _name;
        private string _bandmanager;
        private string _managerphone;
        private string _bandleader;
        private string _bandleaderphone;

        public Band(string name, string bandmanager, string managerphone, string bandleader, string bandleaderphone, int id = 0)
        {
            _id = id;
            _name = name;
            _bandmanager = bandmanager;
            _managerphone = managerphone;
            _bandleader = bandleader;
            _bandleaderphone = bandleaderphone;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetBandManager()
        {
            return _bandmanager;
        }

        public string GetBandManagerPhone()
        {
            return _managerphone;
        }

        public string GetBandLeader()
        {
            return _bandleader;
        }

        public string GetBandLeaderPhone()
        {
            return _bandleaderphone;
        }

        public override bool Equals(System.Object otherBand)
        {
            if (!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool idEquality = this.GetId() == newBand.GetId();
                bool nameEquality = this.GetName() == newBand.GetName();
                bool bandManagerEquality = this.GetBandManager() == newBand.GetBandManager();
                bool bandManagerPhoneEquality = this.GetBandManagerPhone() == newBand.GetBandManagerPhone();
                bool bandLeaderEquality = this.GetBandLeader() == newBand.GetBandLeader();
                bool bandLeaderPhoneEquality = this.GetBandLeaderPhone() == newBand.GetBandLeaderPhone();

                return (idEquality && nameEquality && bandManagerEquality && bandManagerPhoneEquality && bandLeaderEquality && bandLeaderPhoneEquality);
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
            cmd.CommandText = @"INSERT INTO bands(name, band_manager, manager_phone, band_leader, band_leader_phone) VALUES (@name, @bandManager, @managerPhone, @bandLeader, @bandLeaderPhone);";

            MySqlParameter nameParameter = new MySqlParameter();
            nameParameter.ParameterName = "@name";
            nameParameter.Value = _name;
            cmd.Parameters.Add(nameParameter);

            MySqlParameter bandManagerParameter = new MySqlParameter();
            bandManagerParameter.ParameterName = "@bandManager";
            bandManagerParameter.Value = _bandmanager;
            cmd.Parameters.Add(bandManagerParameter);

            MySqlParameter bandManagerPhoneParameter = new MySqlParameter();
            bandManagerPhoneParameter.ParameterName = "@managerPhone";
            bandManagerPhoneParameter.Value = _managerphone;
            cmd.Parameters.Add(bandManagerPhoneParameter);

            MySqlParameter bandLeaderParameter = new MySqlParameter();
            bandLeaderParameter.ParameterName = "@bandLeader";
            bandLeaderParameter.Value = _bandleader;
            cmd.Parameters.Add(bandLeaderParameter);

            MySqlParameter bandLeaderPhoneParameter = new MySqlParameter();
            bandLeaderPhoneParameter.ParameterName = "@bandLeaderPhone";
            bandLeaderPhoneParameter.Value = _bandleaderphone;
            cmd.Parameters.Add(bandLeaderPhoneParameter);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Band> GetAll()
        {
            List<Band> allBands = new List<Band> {};

            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM bands;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int BandId = rdr.GetInt32(0);
                string BandName = rdr.GetString(1);
                string BandManager = rdr.GetString(2);
                string BandManagerPhone = rdr.GetString(3);
                string BandLeader = rdr.GetString(4);
                string BandLeaderPhone = rdr.GetString(5);

                Band newBand = new Band(BandName, BandManager, BandManagerPhone, BandLeader, BandLeaderPhone, BandId);

                allBands.Add(newBand);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allBands;
        }

        public void AddVenue(Venue newVenue)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @venueId);";

            MySqlParameter band_id = new MySqlParameter();
            band_id.ParameterName = "@bandId";
            band_id.Value = _id;
            cmd.Parameters.Add(band_id);

            MySqlParameter venue_id = new MySqlParameter();
            venue_id.ParameterName = "@venueId";
            venue_id.Value = newVenue.GetId();
            cmd.Parameters.Add(venue_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Venue> GetVenues()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT venues.* FROM bands
             JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @bandId;";

            MySqlParameter bandIdParameter = new MySqlParameter();
            bandIdParameter.ParameterName = "@bandId";
            bandIdParameter.Value = _id;
            cmd.Parameters.Add(bandIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Venue> venues = new List<Venue> {};

            while(rdr.Read())
            {
                int thisVenueId = rdr.GetInt32(0);
                string venueName = rdr.GetString(1);
                string thisVenueContact= rdr.GetString(2);
                string venuePhoneNumber = rdr.GetString(3);

                Venue newVenue = new Venue(venueName, thisVenueContact, venuePhoneNumber, thisVenueId);

                venues.Add(newVenue);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return venues;
        }

        public static Band Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM bands WHERE id = (@bandId);";

            MySqlParameter bandIdParameter = new MySqlParameter();
            bandIdParameter.ParameterName = "@bandId";
            bandIdParameter.Value = id;
            cmd.Parameters.Add(bandIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int bandId = 0;
            string bandName = "";
            string bandManager = "";
            string bandManagerPhone = "";
            string bandLeader = "";
            string bandLeaderPhone = "";

            while(rdr.Read())
            {
                bandId = rdr.GetInt32(0);
                bandName = rdr.GetString(1);
                bandManager = rdr.GetString(2);
                bandManagerPhone = rdr.GetString(3);
                bandLeader = rdr.GetString(4);
                bandLeaderPhone = rdr.GetString(5);
            }

            Band newBand = new Band(bandName, bandManager, bandManagerPhone, bandLeader, bandLeaderPhone, bandId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newBand;
        }

        public void UpdateBandName(string newBandName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE bands SET name = @newBandName WHERE id = @findId;";

            MySqlParameter findId = new MySqlParameter();
            findId.ParameterName = "@findId";
            findId.Value = _id;
            cmd.Parameters.Add(findId);

            MySqlParameter bandNameParameter = new MySqlParameter();
            bandNameParameter.ParameterName = "@newBandName";
            bandNameParameter.Value = newBandName;
            cmd.Parameters.Add(bandNameParameter);

            cmd.ExecuteNonQuery();
            _name = newBandName;

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

            MySqlCommand cmd = new MySqlCommand("DELETE FROM bands WHERE id = @bandId; DELETE FROM bands_venues WHERE band_id = @bandId;", conn);

            MySqlParameter bandIdParameter = new MySqlParameter();
            bandIdParameter.ParameterName = "@bandId";
            bandIdParameter.Value = id;
            cmd.Parameters.Add(bandIdParameter);

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
            cmd.CommandText = @"DELETE FROM bands;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
