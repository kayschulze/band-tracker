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
    }
}
