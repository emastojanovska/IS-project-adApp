using Listing.Domain.DomainModels;
using System;
using System.Collections.Generic;

namespace Listing.Service.Interface
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        void CreateNewCategory(Category c);
        void UpdateExistingCategory(Category c);
        void DeleteCategory(Guid id);
        Category GetDetailsForCategory(Guid? id);

    }
}
