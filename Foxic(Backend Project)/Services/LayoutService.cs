using Foxic_Backend_Project_.DAL;

namespace Foxic_Backend_Project_.Services
{
	public class LayoutService
	{
		private readonly FoxicDbContext _context;
		private readonly IHttpContextAccessor _accessor;

		public LayoutService(FoxicDbContext context, IHttpContextAccessor accessor)
		{
			_context = context;
			_accessor = accessor;
		}
		public Dictionary<string, string> GetSettings()
		{
			Dictionary<string, string> settings = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

			return settings;
		}

	}
}
