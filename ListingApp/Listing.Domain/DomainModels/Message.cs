using Listing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Listing.Domain.DomainModels
{
    public class Message : BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }

        public virtual UserDetails User { get; set; }
    }
}
