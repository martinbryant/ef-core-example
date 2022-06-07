using System;
using Microsoft.AspNetCore.Http;

namespace ef_core_example.Logic
{
    public interface IPaginationService
    {
        bool HasPaging { get; }
    }

    public class PaginationService : IPaginationService
    {
        private readonly IHttpContextAccessor _httpContext;

        public PaginationService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        
        public bool HasPaging => throw new System.NotImplementedException();

        public int Take()
        {
            var query = _httpContext.HttpContext.Request.Query;

            if(query.TryGetValue("$take", out var takeValue))
            {
                if(int.TryParse(takeValue, out int take))
                {
                    return take;
                } 
            }

            throw new ArgumentException("whatever");
        }
    }
}