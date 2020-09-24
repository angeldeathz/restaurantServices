﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RestaurantServices.Restaurant.DAL.Shared
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetListAsync<T>(string query);
        Task<IEnumerable<T>> GetListAsync<T>(string query, Dictionary<string, object> parameters);
        Task<T> GetAsync<T>(string query);
        Task<T> GetAsync<T>(string query, Dictionary<string, object> parameters);
        Task<T> InsertAsync<T>(string query, Dictionary<string, object> parameters);
        Task<T> InsertAsync<T>(string query, Dictionary<string, object> parameters, CommandType commandType);
    }
}