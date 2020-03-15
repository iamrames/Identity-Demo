using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Errors", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Errors");
            response.Headers.Add("Allow-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime theDateTime) 
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if ( theDateTime.AddYears(age) > DateTime.Today)
                age--;
                
            return age;
        }

        public static void AddPagination(this HttpResponse response, int currentPage,
            int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}