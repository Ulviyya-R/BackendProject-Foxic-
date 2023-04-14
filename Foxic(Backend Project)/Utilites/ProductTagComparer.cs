using Foxic_Backend_Project_.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Foxic_Backend_Project_.Utilites
{
	public class ProductTagComparer: IEqualityComparer<ProductTag>
	{
		public bool Equals(ProductTag? x, ProductTag? y)
		{
			if (Equals(x?.TagId, y.TagId)) return true;
			return false;
		}

		public int GetHashCode([DisallowNull] ProductTag obj)
		{
			throw new NotImplementedException();
		}
	}
}
