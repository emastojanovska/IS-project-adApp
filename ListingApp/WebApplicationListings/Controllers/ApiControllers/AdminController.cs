using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationListings.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IListingService _listingService;

        public AdminController(ICategoryService _categoryService, IListingService listingService)
        {
            this._categoryService = _categoryService;
            this._listingService = listingService;
        }

        [HttpGet("[action]")]
        public List<Category> GetCategories()
        {
            return this._categoryService.GetAllCategories();
        }

        [HttpPost("[action]")]
        public Category GetCategory(BaseEntity model)
        {
            return this._categoryService.GetDetailsForCategory(model.Id);
        }

        [HttpPost("[action]")]
        public bool CreateCategory(Category category)
        {
            try
            {
                _categoryService.CreateNewCategory(category);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            
        }

        [HttpPost("[action]")]
        public bool EditCategory(Category category)
        {
            try
            {
                _categoryService.UpdateExistingCategory(category);
                return true;
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            
        }

       
        [HttpPost("[action]")]
        public bool DeleteCategory(BaseEntity model)
        {
            try
            {
                _categoryService.DeleteCategory(model.Id);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

        }


        private bool CategoryExists(Guid id)
        {
            return _categoryService.GetDetailsForCategory(id) != null;
        }




    }
}
