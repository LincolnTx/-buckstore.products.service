using System;
using System.Threading.Tasks;

namespace buckstore.products.service.domain.SeedWork
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}