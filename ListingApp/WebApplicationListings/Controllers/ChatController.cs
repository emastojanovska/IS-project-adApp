using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;
using Listing.Domain.Identity;
using Listing.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationListings.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public readonly UserManager<UserDetails> _userManager;
        public readonly ApplicationDbContext _context;

        public ChatController(UserManager<UserDetails> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = User.Identity.Name;
            }
            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }

        // 1
        public async Task<IActionResult> Create(Message message)
        {
        
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                message.DateCreated = DateTime.Now;
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Error();
        }

        private IActionResult Error()
        {
            throw new NotImplementedException();
        }
    }
}
