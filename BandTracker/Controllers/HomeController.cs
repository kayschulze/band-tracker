using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;

namespace BandTracker.Controllers
{
    public class HomeController : Controller
    {
        //Homepage
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        //Venue Homepage (List Venues, Go to Add Venue)
        [HttpGet("/venues")]
        public ActionResult Venues()
        {
            List<Venue> allVenues = Venue.GetAll();
            return View(allVenues);
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
            string venueName = Request.Form["venue-name"];
            string venueContact = Request.Form["venue-contact"];
            string venuePhoneNumber = Request.Form["venue-phone-number"];
            Venue newVenue = new Venue(venueName, venueContact, venuePhoneNumber);
            newVenue.Save();

            return View(newVenue);
        }

        //List Individual Venue (List Information, Link to Band, Edit Venue, Delete Venue)
        [HttpGet("/venues/{id}")]
        public ActionResult VenueDetails(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object> ();
            Venue SelectedVenue = Venue.Find(id);
            List<Band> VenueBands = SelectedVenue.GetBands();
            List<Band> AllBands = Band.GetAll();
            model.Add("venue", SelectedVenue);
            model.Add("venueBands", VenueBands);
            model.Add("allBands", AllBands);

            return View(model);
        }

        //Edit Venue
        [HttpPost("/venues/{id}/edit")]
        public ActionResult EditVenue(int id)
        {
            Venue thisVenue = Venue.Find(id);
            thisVenue.UpdateVenueName(Request.Form["new-venue-name"]);
            return View(thisVenue);
        }

        //Link Band to Venue
        [HttpPost("/venues/{id}/add_band")]
        public ActionResult AddBandToVenue()
        {
            Dictionary<string, object> model = new Dictionary<string, object>() {};

            Venue newVenue = Venue.Find(Int32.Parse(Request.Form["venue-id"]));
            Band newBand = Band.Find(Int32.Parse(Request.Form["band-id"]));
            newVenue.AddBand(newBand);

            model.Add("venue", newVenue);
            model.Add("band", newBand);

            return View(model);
        }

        //Venue Deleted
        [HttpGet("/venues/{id}/delete")]
        public ActionResult VenueDeleted(int id)
        {
            Venue.Delete(id);
            return View();
        }

        //Band Homepage (List Bands, Go to Add Band)
        [HttpGet("bands")]
        public ActionResult Bands()
        {
            List<Band> allBands = Band.GetAll();
            return View(allBands);
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
            string bandName = Request.Form["band-name"];
            string bandManager = Request.Form["band-manager"];
            string bandManagerPhone = Request.Form["band-manager-phone"];
            string bandLeader = Request.Form["band-leader"];
            string bandLeaderPhone = Request.Form["band-leader-phone"];

            Band newBand = new Band(bandName, bandManager, bandManagerPhone, bandLeader, bandLeaderPhone);
            newBand.Save();

            return View(newBand);
        }

        //List Individual Band (List Information, Link to Venue, Edit Band, Delete Band)
        [HttpGet("/bands/{id}")]
        public ActionResult BandDetails(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object> ();
            Band SelectedBand = Band.Find(id);
            List<Venue> BandVenues = SelectedBand.GetVenues();
            List<Venue> AllVenues = Venue.GetAll();
            model.Add("band", SelectedBand);
            model.Add("bandVenues", BandVenues);
            model.Add("allVenues", AllVenues);

            return View(model);
        }

        //Edit Band
        [HttpPost("/bands/{id}/edit")]
        public ActionResult EditBand(int id)
        {
            Band thisBand = Band.Find(id);
            thisBand.UpdateBandName(Request.Form["new-band-name"]);
            return View(thisBand);
        }

        //Link Venue to Band
        [HttpPost("/bands/{id}/add_venue")]
        public ActionResult AddVenueToBand()
        {
            Dictionary<string, object> model = new Dictionary<string, object>() {};
            Venue newVenue = Venue.Find(Int32.Parse(Request.Form["venue-id"]));
            Band newBand = Band.Find(Int32.Parse(Request.Form["band-id"]));
            newBand.AddVenue(newVenue);

            model.Add("venue", newVenue);
            model.Add("band", newBand);

            return View();
        }

        //Band Deleted
        [HttpGet("/bands/{id}/delete")]
        public ActionResult BandDeleted(int id)
        {
            Band.Delete(id);
            return View();
        }

    }
}
