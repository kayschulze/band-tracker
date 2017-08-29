using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
    [TestClass]
    public class BandTests : IDisposable
    {
        public BandTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
        }

        public void Dispose()
        {
            Band.DeleteAll();
            Venue.DeleteAll();
        }

        [TestMethod]
        public void Equals_OverrideTrueForSameBandProperties_Band()
        {
            //Arrange, Act
            Band firstBand = new Band("Trees", "Nehemia", "503-555-7890", "Tayla", "206-555-6800", 1);
            Band secondBand = new Band("Trees", "Nehemia", "503-555-7890", "Tayla", "206-555-6800", 1);

            //Assert
            Assert.AreEqual(firstBand, secondBand);
        }
    }
}
