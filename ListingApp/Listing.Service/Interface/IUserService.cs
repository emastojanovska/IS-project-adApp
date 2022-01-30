using Listing.Domain.DomainModels;
using Listing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Service.Interface
{
    public interface IUserService
    {
        UserDetails Get(string id);
        void UpdateExistingUserDetails(UserDetails userDetails, string id, UserImage image);
        Boolean AddImageToUser(UserDetails userDetails, UserImage userImage);

    }
}
