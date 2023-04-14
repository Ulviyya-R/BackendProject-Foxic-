using Foxic_Backend_Project_.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Foxic_Backend_Project_.Utilites
{
	public class ProductComparer: IEqualityComparer<Product>
	{
		public bool Equals(Product? x, Product? y)
		{
			if (Equals(x?.Id, y?.Id)) return true;
			return false;
		}

		public int GetHashCode([DisallowNull] Product obj)
		{
			throw new NotImplementedException();
		}
	}
}
