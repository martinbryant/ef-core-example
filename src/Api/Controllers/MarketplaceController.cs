
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ef_core_example.Models;
using Microsoft.AspNetCore.Mvc;

namespace ef_core_example.Controllers
{
    public abstract class MarketplaceController : ControllerBase
    {
        /// <summary>
        /// The id of seller that has made the request
        /// </summary>
        public string SellerId => HttpContext.User.Identity.Name;

        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult ToActionResult<T>(Result<T, Error> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            if (result.Error.Code == Errors.General.Record_Not_Found)
                return NotFound(Envelope.Error(result.Error));

            return BadRequest(Envelope.Error(result.Error));
        }
    }

}