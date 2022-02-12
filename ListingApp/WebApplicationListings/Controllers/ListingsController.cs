using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;
using Listing.Domain.DTO;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IImageService _imageService;
        private readonly IEmailService _emailService;

        public ListingsController(IListingService listingService, ICategoryService categoryService, 
            ILocationService locationService, IWishlistService wishlistService, IImageService imageService,
            IEmailService emailService)
        {
            _listingService = listingService;
            _categoryService = categoryService;
            _locationService = locationService;
            _wishlistService = wishlistService;
            _imageService = imageService;
            _emailService = emailService;
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

            var listings = _listingService.GetAllApprovedListings();

            ListingsWithFilter listingsWithFilter = new ListingsWithFilter(listings, "All", "All", 1000);
            return View(listingsWithFilter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("SelectedCategory, SelectedLocation, Price")] ListingsWithFilter listing)
        {
            var categories = _categoryService.GetAllCategories();
            categories.Add(new Category("All"));
            ViewBag.Categories = new SelectList(categories, "Name", "Name");

            var locations = _locationService.GetAllLocations();
            locations.Add(new Location(" ", "All"));
            ViewBag.Locations = new SelectList(locations, "City", "City");

            var listings = _listingService.GetAllByLocationAndCategoryAndPrice(listing.SelectedLocation, listing.SelectedCategory, listing.Price);

            ListingsWithFilter listingsWithFilter = new ListingsWithFilter(listings, listing.SelectedCategory, listing.SelectedLocation, listing.Price);
            return View(listingsWithFilter);
        }
        [Authorize]
        // GET: Listings
        public IActionResult MyPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listings = _listingService.GetAllListingsForUser(userId);
            return View(listings);
        }
        public IActionResult SearchResult(String search)
        {
            ViewBag.searchValue = search;
            var listings = _listingService.GetAllByTitleOrDescription(search);
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
            AddCommentToListingPost model = new AddCommentToListingPost
            {
                SelectedListing = listing,
                ListingId = listing.Id
            };

            if (listing == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [Authorize]
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
        public async Task<IActionResult> Create([Bind("Title,Description,Price,Discount,CategoryId,Id,LocationId")] ListingPost listing, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _listingService.CreateNewListing(listing, userId, images);

                await _emailService.SendEmailAsync(listing,"newListing", "");
                return RedirectToAction(nameof(Index));
            }

            return View(listing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCommentToListing(Guid id, [Bind("Comment")] AddCommentToListingPost model)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ListingPost listing = _listingService.GetDetailsForListing(id);
            _listingService.AddCommentToListing(listing, userId, model.Comment);

            return RedirectToAction("Details", "Listings", new { id });
        }

        [Authorize]
        public IActionResult AddListingToWishlist(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ListingPost listing = _listingService.GetDetailsForListing(id);
            AddToWishlistDto item = new AddToWishlistDto
            {
                SelectedListing = listing,
                ListingId = id
            };
            if (_wishlistService.checkIfExist(userId, id))
            {
                return RedirectToAction("Index", "Listings");
            }
            _listingService.AddToWishlist(item, userId);


            return RedirectToAction("Index", "Wishlists");
        }

        [Authorize]
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
        public IActionResult Edit(Guid id, [Bind("Title,Description,Price,Discount,CategoryId,Id,LocationId, DateCreated, UserId, Status")] ListingPost listing, List<IFormFile> images)
        {
            if (id != listing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _listingService.UpdeteExistingListing(listing, images);
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

        [Authorize]
        // GET: Listings/DeleteImage/5
        public IActionResult DeleteImage(Guid listingId, Guid imageId)
        {
            var listing = _listingService.GetDetailsForListing(listingId);
            ListingImage listingImage = _imageService.GetListingImage(imageId);
            listing.ListingImages.Remove(listingImage);
            _listingService.UpdeteExistingListing(listing);

            return RedirectToAction("Edit", "Listings", new { id = listingId });
        }

        // GET: Listings/Delete/5
        [Authorize]
        public IActionResult Delete(Guid id)
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

            _listingService.DeleteListing(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ListingExists(Guid id)
        {
            return _listingService.GetDetailsForListing(id) != null;
        }
    }
}
