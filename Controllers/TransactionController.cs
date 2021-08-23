using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ef_core_example.Models;
using System.Linq;
using System;

namespace ef_core_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(
            AppDbContext context,
            ILogger<TransactionController> logger
)

        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var transaction = _context.Transactions
                .Include(trans => trans.Buyer)
                .Include(trans => trans.Listing)
                .ThenInclude(listing => listing.Seller)
                .Include(trans => trans.Listing)
                .ThenInclude(listing => listing.Manufacturer)
                .FirstOrDefaultAsync(trans => trans.Id == id);

            var result = await transaction;

            return Ok(result);
        }

        [HttpGet("search")]    
        public async Task<IActionResult> GetDetailed([FromBody]TransactionQuery query)
        {
            var transactions = await _context.Transactions.Where(trans => trans.OrderedDate >= query.FromDate && trans.OrderedDate <= query.ToDate)
                                                    .Include(trans => trans.Buyer)
                                                    .Include(trans => trans.Listing)
                                                    .ThenInclude(listing => listing.Seller)
                                                    .Include(trans => trans.Listing)
                                                    .ThenInclude(listing => listing.Manufacturer)
                                                    .ToListAsync();
                    
            return Ok(transactions);
        }

        // [HttpGet("buyer/{id}")]
        // public async Task<IActionResult> GetBuyers(int id)
        // {
        //     var transactions = await _context.Transactions.Where(trans => trans.Buyer.Profile.Id == id)
        //                                                     .Include(trans => trans.Buyer)
        //                                                     .Include(trans => trans.Listing)
        //                                                     .ThenInclude(listing => listing.Seller)
        //                                                     .Include(trans => trans.Listing)
        //                                                     .ThenInclude(listing => listing.Manufacturer)
        //                                                     .ToListAsync(); 

        //     return Ok(transactions);
        // }
    }

    public class TransactionQuery
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
