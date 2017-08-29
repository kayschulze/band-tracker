using System.Collecitons.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
    public class Band
    {
        private string _id;
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

        public override bool Equals(System.Object otherBand)
        {
            if (!(otherBand) is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool 
            }
        }

    }
}
