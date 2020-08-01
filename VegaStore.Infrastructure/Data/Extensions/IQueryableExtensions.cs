using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using VegaStore.Core.DbQueryFeatures;
using VegaStore.Core.Entities;
using VegaStore.Core.RequestFeatures;

namespace VegaStore.Infrastructure.Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Vehicle> ApplyFilters(this IQueryable<Vehicle> query, VehicleQueryParameters vehicleQueryParameters)
        {
            var searchTerm = vehicleQueryParameters.SearchTerm;
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(v => v.Name.Contains(searchTerm.Trim().ToLower()));

            if (Enum.TryParse(vehicleQueryParameters.Color, out Colors color))
            {
                var totalEnumItems = Enum.GetNames(typeof(Colors)).Length;

                if (color > 0 && (int)color <= totalEnumItems)
                    query = query.Where(v => v.Color.Equals(color));
            }

            if (Enum.TryParse(vehicleQueryParameters.Condition, out Condition condition))
            {
                var totalEnumItems = Enum.GetNames(typeof(Condition)).Length;

                if (condition > 0 && (int)condition <= totalEnumItems)
                    query = query.Where(v => v.Condition.Equals(condition));
            }

            if (vehicleQueryParameters.MinPrice.HasValue && vehicleQueryParameters.MaxPrice.HasValue)
            {
                if (vehicleQueryParameters.MinPrice.Value > 0 && vehicleQueryParameters.MinPrice.Value < vehicleQueryParameters.MaxPrice.Value)
                {
                    query = query.Where(v => v.Price >= vehicleQueryParameters.MinPrice.Value && v.Price <= vehicleQueryParameters.MaxPrice.Value);
                }
            }

            return query;
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, VehicleQueryParameters vehicleQueryParameters)
        {

            return query
                .Skip((vehicleQueryParameters.PageNumber - 1) * vehicleQueryParameters.PageSize)
                .Take(vehicleQueryParameters.PageSize);
        }

        
    }
}
