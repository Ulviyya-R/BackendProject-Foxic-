using Foxic_Backend_Project_.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Foxic_Backend_Project_.Utilites
{
	public class ProductCategoryComparer : IEqualityComparer<ProductCategory>
	{
		public bool Equals(ProductCategory? x, ProductCategory? y)
		{
			if (Equals(x?.CategoryId, y.CategoryId)) return true;
			return false;
		}

		public int GetHashCode([DisallowNull] ProductCategory obj)
		{
			throw new NotImplementedException();
		}
	}
}
