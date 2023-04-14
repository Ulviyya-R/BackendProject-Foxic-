using Foxic_Backend_Project_.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Foxic_Backend_Project_.Utilites
{
	public class ProductCollectionComparer : IEqualityComparer<Collection>
	{
		public bool Equals(Collection? x, Collection? y)
		{
			if (Equals(x?.Id, y.Id)) return true;
			return false;
		}

		public int GetHashCode([DisallowNull] Collection obj)
		{
			throw new NotImplementedException();
		}
	}
}

