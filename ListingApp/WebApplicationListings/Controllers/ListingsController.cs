﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;
using Listing.Domain.DTO;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationListings.Controllers
{
    public class ListingsController : Controller
    {
        private readonly IListingService _listingService;
        private readonly ICategoryService _categoryService;
        private readonly ILocationService _locationService;
        private readonly IWishlistService _wishlistService;


        public ListingsController(IListingService listingService, ICategoryService categoryService, ILocationService locationService, IWishlistService wishlistService)
        {
            _listingService = listingService;
            _categoryService = categoryService;
            _locationService = locationService;
            _wishlistService = wishlistService;
        }



        // GET: Listings
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            categories.Add(new Category("All"));
            ViewBag.Categories = new SelectList(categories, "Name", "Name");

            var locations = _locationService.GetAllLocations();
            locations.Add(new Location(" ", "All"));
            ViewBag.Locations = new SelectList(locations, "City", "City");

            var listings = _listingService.GetAllActiveListings();
            ListingsWithFilter listingsWithFilter = new ListingsWithFilter(listings, "All", "All");
            return View(listingsWithFilter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("SelectedCategory, SelectedLocation")] ListingsWithFilter listing)
        {
            var categories = _categoryService.GetAllCategories();
            categories.Add(new Category("All"));
            ViewBag.Categories = new SelectList(categories, "Name", "Name");

            var locations = _locationService.GetAllLocations();
            locations.Add(new Location(" ", "All"));
            ViewBag.Locations = new SelectList(locations, "City", "City");
            var listings = _listingService.GetAllByLocationAndCategory(listing.SelectedLocation, listing.SelectedCategory);
            ListingsWithFilter listingsWithFilter = new ListingsWithFilter(listings, listing.SelectedCategory, listing.SelectedLocation);
            return View(listingsWithFilter);
        }
        // GET: Listings
        public IActionResult MyPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listings = _listingService.GetAllListingsForUser(userId);
            return View(listings);
        }
        // GET: Listings/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = _listingService.GetDetailsForListing(id);

            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // GET: Listings/Create
        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var locations = _locationService.GetAllLocations();
            ViewBag.Locations = new SelectList(locations, "Id", "City");

            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Price,Discount,CategoryId,Id,LocationId")] ListingPost listing)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                listing.UserId = userId;
                _listingService.CreateNewListing(listing);
                return RedirectToAction(nameof(Index));
            }
         
            return View(listing);
        }

        public IActionResult AddListingToWishlist(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ListingPost listing = _listingService.GetDetailsForListing(id);
            AddToWishlistDto item = new AddToWishlistDto
            {
                SelectedListing = listing,
                ListingId = id
            };
            if(_wishlistService.checkIfExist(userId, id))
            {
                return RedirectToAction("Index", "Listings");
            }
            _listingService.AddToWishlist(item, userId);
            

            return RedirectToAction("Index", "Wishlists");
        }

        // GET: Listings/Edit/5
        public IActionResult Edit(Guid? id)
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var locations = _locationService.GetAllLocations();
            ViewBag.Locations = new SelectList(locations, "Id", "City");

            if (id == null)
            {
                return NotFound();
            }

            var listing = _listingService.GetDetailsForListing(id);

            if (listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Description,Price,Discount,CategoryId,Id,LocationId, DateCreated")] ListingPost listing)
        {
            if (id != listing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _listingService.UpdeteExistingListing(listing);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingExists(listing.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(listing);
        }

        // GET: Listings/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = _listingService.GetDetailsForListing(id);

            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _listingService.DeleteListing(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ListingExists(Guid id)
        {
            return _listingService.GetDetailsForListing(id) != null;
        }
    }
}
