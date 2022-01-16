using Listing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Domain.DomainModels
{
    public class UserImage: Image
    {
        public string UserId { get; set; }
        public virtual UserDetails Image { get; set; }

        public UserImage(byte[] data, string name, string type, string imageDataBase64, string imageSrc) : base(data, name, type, imageDataBase64, imageSrc)
        {

        }
        public UserImage()
        {

        }
    }
}
