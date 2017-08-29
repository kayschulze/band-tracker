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

        public Band(int id, string name, string bandmanager, string managerphone, string bandleader, string bandleaderphone)
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
