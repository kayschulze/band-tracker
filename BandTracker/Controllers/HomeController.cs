using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;

namespace BandTracker.Controllers
{
    public class HomeController : Controllers
    {
        //Homepage
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        //Venue Homepage (List Venues, Go to Add Venue)
        [HttpGet("venues")]
        public ActionResult Venues()
        {

        }

        //Venue Form - Enters New Venue Information.
        [HttpGet("/venues/new")]
        public ActionResult VenueForm()
        {
            return View();
        }

        //Venue Add - Displays New Venue Creation.
        [HttpPost("/venues/add")]
        public ActionResult AddVenue()
        {

        }

        //List Individual Venue (List Information, Link to Band, Edit Venue, Delete Venue)
        [HttpGet("/venues/{id}")]
        public ActionResult VenueDetails(int id)
        {

        }

        //Edit Venue
        [HttpGet("/venues/{id}/edit")]
        public ActionResult EditVenue(int id)
        {

        }

        //Link Band to Venue
        [HttpGet("/venues/{id}/add_band")]
        public ActionResult AddBandToVenue()
        {

        }

        //Venue Deleted
        [HttpGet("/venues/{id}/delete")]
        public ActionResult VenueDeleted(int id)
        {

        }

        //Band Homepage (List Bands, Go to Add Band)
        [HttpGet("bands")]
        public ActionResult Bands()
        {

        }

        //Band Form - Enters New Band Information.
        [HttpGet("/bands/new")]
        public ActionResult BandForm()
        {
            return View();
        }

        //Band Add - Displays New Band Creation.
        [HttpPost("/bands/add")]
        public ActionResult AddBand()
        {

        }

        //List Individual Band (List Information, Link to Venue, Edit Band, Delete Band)
        [HttpGet("/bands/{id}")]
        public ActionResult BandDetails(int id)
        {

        }

        //Edit Band
        [HttpGet("/bands/{id}/edit")]
        public ActionResult EditBand(int id)
        {

        }

        //Link Venue to Band
        [HttpGet("/venues/{id}/add_venue")]
        public ActionResult AddVenueToBand()
        {

        }

        //Band Deleted
        [HttpGet("/bands/{id}/delete")]
        public ActionResult BandDeletedBand(int id)
        {

        }

    }
}
