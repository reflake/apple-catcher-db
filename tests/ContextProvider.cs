using System;
using Database;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
	public record ContextProvider(IServiceScope Scope, AppDbContext Context) : IDisposable
	{
		public void Dispose()
		{
			Scope.Dispose();
			Context.Dispose();
		}
	}
}