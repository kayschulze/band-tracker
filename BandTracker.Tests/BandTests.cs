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

        [TestMethod]
        public void Save_SavesBandInformationToDatabase_BandList()
        {
            //Arrange
            Band expectedBand = new Band("Trees", "Nehemia", "503-555-7890", "Tayla", "206-555-6800", 1);
            expectedBand.Save();

            //Act
            List<Band> resultBandList = Band.GetAll();
            List<Band> expectedBandList = new List<Band> {expectedBand};

            //Assert
            CollectionAssert.AreEqual(expectedBandList, resultBandList);
        }

        [TestMethod]
        public void Find_FindsBandInDatabaseFromId_Band()
        {
            //Arrange
            Band testBand = new Band("Trees", "Nehemia", "503-555-7890", "Tayla", "206-555-6800", 1);
            testBand.Save();

            //Act
            Band foundBand = Band.Find(testBand.GetId());

            //Assert
            Assert.AreEqual(testBand, foundBand);
        }

        [TestMethod]
        public void UpdateBandName_UpdatesNameOfBand_Band()
        {
            //Arrange
            string newBandName = "Trees";
            Band testBand = new Band("Green Pointy Trees", "Nehemia", "503-555-7890", "Tayla", "206-555-6800", 1);
            testBand.Save();

            //Act
            testBand.UpdateBandName(newBandName);

            string result = Band.Find(testBand.GetId()).GetName();

            //Assert
            Assert.AreEqual(newBandName, result);
        }

        [TestMethod]
        public void GetVenues_ReturnsListOfVenuesForABand_Venues()
        {
            //Arrange
            Band testBand = new Band("Green Pointy Trees", "Nehemia", "503-555-7890", "Tayla", "206-555-6800", 1);
            testBand.Save();

            Venue firstVenue = new Venue("Menashe Aaron's Table", "206-333-4444", "Ronald Roberts", 1);
            firstVenue.Save();
            Venue secondVenue = new Venue("Beth Shalom", "206-333-4444", "Rose Borodin", 2);
            secondVenue.Save();

            //Act
            List<Venue> expectedVenueList = new List<Venue> {firstVenue, secondVenue};

            testBand.AddVenue(firstVenue);
            testBand.AddVenue(secondVenue);
            List<Venue> resultVenueList = testBand.GetVenues();

            //Assert
            CollectionAssert.AreEqual(expectedVenueList, resultVenueList);
        }

        [TestMethod]
        public void Delete_DeletesBandFromDatabase_BandList()
        {
            //Arrange
            Band testBand1 = new Band("Green Pointy Trees", "Nehemia", "503-555-7890", "Tayla", "206-555-6800", 1);
            testBand1.Save();

            Band testBand2 = new Band("Mincha Choir", "206-555-6800", "Rose Borodin", "Carl Los", "206-888-7777", 3);
            testBand2.Save();

            //Act
            Band.Delete(testBand1.GetId());
            List<Band> resultBandsList = Band.GetAll();
            List<Band> actualBandsList = new List<Band> {testBand2};

            //Assert
            CollectionAssert.AreEqual(actualBandsList, resultBandsList);
        }
    }
}
