using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
    [TestClass]
    public class VenueTests : IDisposable
    {
        public VenueTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
        }

        public void Dispose()
        {
            Band.DeleteAll();
            Venue.DeleteAll();
        }

        [TestMethod]
        public void Equals_OverrideTrueForSameVenueProperties_Venue()
        {
            //Arrange, Act
            Venue firstVenue = new Venue("Menashe Aaron's Table", "206-333-4444", "Ronald Roberts", 2);
            Venue secondVenue = new Venue("Menashe Aaron's Table", "206-333-4444", "Ronald Roberts", 2);

            //Assert
            Assert.AreEqual(firstVenue, secondVenue);
        }
    }
}
