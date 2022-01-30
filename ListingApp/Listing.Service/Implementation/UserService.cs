using Listing.Domain.DomainModels;
using Listing.Domain.Identity;
using Listing.Repository.Implementation;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public UserDetails Get(string id)
        {
            return userRepository.Get(id);
        }

        public void UpdateExistingUserDetails(UserDetails userDetails, string id, UserImage image)
        {
            UserDetails user = this.Get(id);
            user.FirstName = userDetails.FirstName;
            user.LastName = userDetails.LastName;
            user.Contact = userDetails.Contact;
            user.Address = userDetails.Address;
            user.Address = userDetails.Address;
            user.Address = userDetails.Address;
            if(image!=null)
            user.Image = image;

            this.userRepository.Update(user);
        }
        public Boolean AddImageToUser(UserDetails userDetails, UserImage userImage)
        {
            userDetails.Image = userImage;

            this.userRepository.Update(userDetails);

            return true;
        }
    }
}
