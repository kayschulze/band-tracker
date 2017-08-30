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

        [TestMethod]
        public void Save_SavesVenueInformationToDatabase_VenueList()
        {
            //Arrange
            Venue expectedVenue = new Venue("Menashe Aaron's Table", "206-333-4444", "Ronald Roberts", 2);
            expectedVenue.Save();

            //Act
            List<Venue> resultVenueList = Venue.GetAll();
            List<Venue> expectedVenueList = new List<Venue> {expectedVenue};

            //Assert
            CollectionAssert.AreEqual(expectedVenueList, resultVenueList);
        }

        [TestMethod]
        public void Find_FindsVenueInDatabaseFromId_Venue()
        {
            //Arrange
            Venue testVenue = new Venue("Menashe Aaron's Table", "206-333-4444", "Ronald Roberts", 2);
            testVenue.Save();

            //Act
            Venue foundVenue = Venue.Find(testVenue.GetId());

            //Assert
            Assert.AreEqual(testVenue, foundVenue);
        }
    }
}
