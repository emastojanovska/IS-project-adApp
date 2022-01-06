using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;
using Listing.Domain.DTO;
using Listing.Repository;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationListings.Controllers
{
    public class ListingsController : Controller
    {
        private readonly IListingService _listingService;

        public ListingsController(IListingService listingService)
        {
            _listingService = listingService;
        }

        // GET: Listings
        public IActionResult Index()
        {
            var listings = _listingService.GetAllListings();
            return View(listings);
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Price,Discount,DateCreated,DateUpdated,CategoryId,Id")] ListingPost listing)
        {
            if (ModelState.IsValid)
            {
                _listingService.CreateNewListing(listing);
                return RedirectToAction(nameof(Index));
            }
         
            return View(listing);
        }

        public IActionResult AddListingToWishlist(Guid? id)
        {
            AddToWishlistDto model = _listingService.GetWishlistInfo(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddListingToWishlist([Bind("ListingId")] AddToWishlistDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _listingService.AddToWishlist(item, userId);
            return View(item);
        }

        // GET: Listings/Edit/5
        public IActionResult Edit(Guid? id)
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

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Description,Price,Discount,DateCreated,DateUpdated,CategoryId,Id")] ListingPost listing)
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
