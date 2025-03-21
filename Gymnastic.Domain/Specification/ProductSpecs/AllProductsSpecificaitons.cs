﻿using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gymnastic.Domain.Specification.ProductSpecs
{
    public class AllProductsSpecificaitons : BaseSpecification<Product, int>
    {
        public AllProductsSpecificaitons(
            string? searchTerm = null,
            int? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? pageNumber = null,
            int? pageSize = null,
            string? orderBy = null,
            bool isDecending = false)
        {
            Expression<Func<Product, bool>> criteria = p => true;

            // Filtering by category
            if (categoryId is not null)
            {
                criteria = criteria.AndAlso(p => p.CategoryId == categoryId);
            }

            // Filtering by price range
            if (minPrice is not null)
            {
                criteria = criteria.AndAlso(p => p.Price >= minPrice);
            }
            if (maxPrice is not null)
            {
                criteria = criteria.AndAlso(p => p.Price <= maxPrice);
            }

            // Search term filtering (assumes searching in Name and Description)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                criteria = criteria.AndAlso(p => p.Name.Contains(searchTerm));
            }

            Where(criteria);

            // Sorting
            if (!string.IsNullOrEmpty(orderBy))
            {
                var property = typeof(Product).GetProperties()
                    .FirstOrDefault(p => string.Equals(p.Name, orderBy, StringComparison.OrdinalIgnoreCase));
                if (isDecending && property is not null)
                {
                    AddOrderByDescending(p => EF.Property<Product>(p, property.Name));
                }
                else if (property is not null)
                {
                    {
                        AddOrderBy(p => EF.Property<Product>(p, property.Name));
                    }
                }

                // Pagination
                if (pageNumber is not null && pageSize is not null)
                {
                    ApplyPaging(((int)pageNumber - 1) * (int)pageSize, (int)pageSize);
                }

                // Always include related data
                AddInclude(p => p.Category);
                AddInclude(p => p.Images);
                EnableSplitQuery();
            }
        }
    }
}
