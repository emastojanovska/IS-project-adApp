﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;
using Listing.Domain.Identity;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationListings.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<UserDetails> userManager;
        private readonly SignInManager<UserDetails> signInManager;
        private readonly IUserService userService;
        private readonly IImageService imageService;

        public AccountController(UserManager<UserDetails> userManager, SignInManager<UserDetails> signInManager,
            IUserService _userService, IImageService _imageService)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            userService = _userService;
            imageService = _imageService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDetails = userService.Get(userId);

            return View(userDetails);
        }

        [Authorize]
        // GET: Listings/Edit/5
        public IActionResult Edit(string id)
        {
        
            if (id == null)
            {
                return NotFound();
            }

   
            var userDetails = userService.Get(id);

            if (userDetails == null)
            {
                return NotFound();
            }
            return View(userDetails);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("FirstName,LastName,Contact,Address, Id")] UserDetails userDetails, IFormFile image)
        {
            if (id != userDetails.Id)
            {
                return NotFound();
            }

            var data = GetByteArrayFromImage(image);
            var imageDataBase64 = Convert.ToBase64String(data, 0, data.Length);
            var imageSrc = "data:image/png;base64," + imageDataBase64;

            UserImage userImage = new UserImage(data, Path.GetFileName(image.FileName), image.ContentType, imageDataBase64, imageSrc);

            if (ModelState.IsValid)
            {
                try
                {
                    userService.UpdateExistingUserDetails(userDetails, id, userImage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailsExist(userDetails.Id))
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

            return View(userDetails);
        }
        private bool UserDetailsExist(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDetails = userService.Get(userId);
            return userDetails!= null;
        }

        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }
        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var data = GetByteArrayFromImage(image);
                var imageDataBase64 = Convert.ToBase64String(data, 0, data.Length);
                var imageSrc = "data:image/png;base64," + imageDataBase64;

                UserImage userImage = new UserImage(data, Path.GetFileName(image.FileName), image.ContentType, imageDataBase64, imageSrc);
            

                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new UserDetails
                    {
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Address = request.Address,
                        Contact = request.Contact,
                        UserWishlist = new Wishlist()
                    };
                    var result = await userManager.CreateAsync(user, request.Password);

                    if (result.Succeeded)
                    {
                        user.Image = userImage;
                        result = await userManager.UpdateAsync(user);
                        if(result.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            if (result.Errors.Count() > 0)
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("message", error.Description);
                                }
                            }
                            return View(request);
                        }
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
