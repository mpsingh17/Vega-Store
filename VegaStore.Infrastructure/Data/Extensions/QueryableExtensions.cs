using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using VegaStore.Core.Entities;
using VegaStore.Core.RequestFeatures;

namespace VegaStore.Infrastructure.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Vehicle> ApplyFilters(this IQueryable<Vehicle> query, VehicleParameters vehicleParameters)
        {
            var searchTerm = vehicleParameters.Value;
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(v => v.Name.Contains(searchTerm.Trim().ToLower()));

            if (Enum.TryParse(vehicleParameters.Color, out Colors color))
            {
                var totalEnumItems = Enum.GetNames(typeof(Colors)).Length;

                if (color > 0 && (int)color <= totalEnumItems)
                    query = query.Where(v => v.Color.Equals(color));
            }

            if (Enum.TryParse(vehicleParameters.Condition, out Condition condition))
            {
                var totalEnumItems = Enum.GetNames(typeof(Condition)).Length;

                if (condition > 0 && (int)condition <= totalEnumItems)
                    query = query.Where(v => v.Condition.Equals(condition));
            }

            if (vehicleParameters.MinPrice.HasValue && vehicleParameters.MaxPrice.HasValue)
            {
                if (vehicleParameters.MinPrice.Value > 0 && vehicleParameters.MinPrice.Value < vehicleParameters.MaxPrice.Value)
                {
                    query = query.Where(v => v.Price >= vehicleParameters.MinPrice.Value && v.Price <= vehicleParameters.MaxPrice.Value);
                }
            }

            return query;
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int skip, int take)
        {
            return query.Skip(skip).Take(take);
        }

        
    }
}
