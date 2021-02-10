using System;
using MediatR;

namespace buckstore.products.service.domain.Events
{
	public class Event : INotification
	{
		public DateTime Timestamp { get; set; }

		protected Event()
		{
			Timestamp = DateTime.Now;
		}
	}
}