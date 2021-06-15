using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using buckstore.products.service.api.v1.Filters;
using buckstore.products.service.domain.Exceptions;

namespace buckstore.products.service.api.v1.Controllers
{
	[Route("commodities/[controller]")]
	[ServiceFilter(typeof(GlobalExceptionFilterAttribute))]
	public class BaseController : Controller
	{
		private readonly ExceptionNotificationHandler _notifications;

		protected IEnumerable<ExceptionNotification> Notifications => _notifications.GetNotifications();

		protected BaseController(INotificationHandler<ExceptionNotification> notifications)
		{
			_notifications = (ExceptionNotificationHandler) notifications;
		}

		protected bool IsValidOperation()
		{
			return (!_notifications.HasNotifications());
		}

		protected new IActionResult Response(int statusCode, object result = null)
		{
			if (IsValidOperation())
			{
				return StatusCode(statusCode, new
				{
					success = true,
					data = result
				});
			}

			return BadRequest(new
			{
				success = false,
				errors = _notifications.GetNotifications()
			});
		}
		
		protected string GetTokenClaim(string claim)
		{
			var header = Request.Headers["Authorization"].ToString();
			var token = header.Replace("Bearer ", string.Empty);

			var readToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

			return readToken.Payload[claim].ToString() ?? string.Empty;
		}
	}
}