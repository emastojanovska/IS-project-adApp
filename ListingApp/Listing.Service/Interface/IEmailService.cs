using Listing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listing.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(ListingPost listing, string action, string userEmail);
    }
}
