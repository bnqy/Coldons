using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using System.Collections.Concurrent; // ConcurrentDictionary
using Coldons.Lib;

namespace Northwind.WebApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
	private static ConcurrentDictionary<string, Customer>? customersCache;

	private NorthwindContext db;

	public CustomerRepository(NorthwindContext injectedContext)
	{
		db = injectedContext;

		if(customersCache is null)
		{
			customersCache = new ConcurrentDictionary<string, Customer>(db.Customers
				.ToDictionary(c => c.CustomerId));
		}
	}

	public async Task<Customer?> CreateAsync(Customer c)
	{
		c.CustomerId = c.CustomerId.ToUpper();

		//addinf database
		EntityEntry<Customer> added = await db.Customers.AddAsync(c);
		int affected = await db.SaveChangesAsync();

		if (affected == 1)
		{
			if (customersCache is null) 
				return c;
			return customersCache.AddOrUpdate(c.CustomerId, c, UpdateCache);
		}
		else
		{
			return null;
		}
	}


}
