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

        [TestMethod]
        public void UpdateVenueName_UpdatesNameOfVenue_Venue()
        {
            //Arrange
            string newVenueName = "Menashe Aaron's";
            Venue testVenue = new Venue("Menashe Aaron's Table", "206-333-4444", "Ronald Roberts", 2);
            testVenue.Save();

            //Act
            testVenue.UpdateVenueName(newVenueName);

            string result = Venue.Find(testVenue.GetId()).GetName();

            //Assert
            Assert.AreEqual(newVenueName, result);
        }

        [TestMethod]
        public void GetBands_ReturnsListOfBandsForAVenue_Bands()
        {
            //Arrange
            Venue testVenue = new Venue("Menashe Aaron's Table", "206-333-4444", "Ronald Roberts", 2);
            testVenue.Save();

            Band firstBand = new Band("Green Pointy Trees", "Nehemia", "503-555-7890", "Tayla", "206-555-6800", 2);
            firstBand.Save();
            Band secondBand = new Band("Cools", "Todd S.", "206-555-7890", "Jessica", "206-444-2255", 3);
            secondBand.Save();

            //Act
            List<Band> expectedBandList = new List<Band> {firstBand, secondBand};

            testVenue.AddBand(firstBand);
            testVenue.AddBand(secondBand);
            List<Band> resultBandList = testVenue.GetBands();

            //Assert
            CollectionAssert.AreEqual(expectedBandList, resultBandList);
        }

        [TestMethod]
        public void Delete_DeletesVenueFromDatabase_VenueList()
        {
            //Arrange
            Venue testVenue1 = new Venue("Menashe Aaron's Table", "206-333-4444", "Ronald Roberts", 2);
            testVenue1.Save();

            Venue testVenue2 = new Venue("Beth Shalom's", "206-555-6800", "Rose Borodin", 3);
            testVenue2.Save();

            //Act
            Venue.Delete(testVenue1.GetId());
            List<Venue> resultVenuesList = Venue.GetAll();
            List<Venue> actualVenuesList = new List<Venue> {testVenue2};

            //Assert
            CollectionAssert.AreEqual(actualVenuesList, resultVenuesList);
        }
    }
}
