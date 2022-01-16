using Listing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Domain.DomainModels
{
    public class Image : BaseEntity
    {
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public string MimeType { get; set; }
        public string ImageDataBase64 { get; set; }
        public string ImageSrc { get; set; }
      
      
        public Image()
        {
         
        }
        public Image(byte[] data, string name, string type, string imageDataBase64, string imageSrc)
        {
            this.ImageData = data;
            this.ImageName = name;
            this.MimeType = type;
            this.ImageDataBase64 = imageDataBase64;
            this.ImageSrc = imageSrc;
        }
    }
}
