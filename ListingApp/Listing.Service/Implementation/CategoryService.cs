using Listing.Domain.DomainModels;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void CreateNewCategory(Category c)
        {
            this._categoryRepository.Insert(c);
        }
        public Category GetDetailsForCategory(Guid? id)
        {
            return this._categoryRepository.Get(id);
        }

        public void DeleteCategory(Guid id)
        {
            var category = this.GetDetailsForCategory(id);
            this._categoryRepository.Delete(category);
        }

        public List<Category> GetAllCategories()
        {
            return this._categoryRepository.GetAll().ToList();
        }

        public void UpdateExistingCategory(Category l)
        {
            this._categoryRepository.Update(l);
        }
    }
}
