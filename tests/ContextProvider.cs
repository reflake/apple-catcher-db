using System;
using Database;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
	public record ContextProvider<TEntry>(IServiceScope Scope, AppDbContext<TEntry> Context) : IDisposable
		where TEntry : class
	{
		public void Dispose()
		{
			Scope.Dispose();
			Context.Dispose();
		}
	}
}