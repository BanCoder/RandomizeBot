using Microsoft.Extensions.Configuration;

namespace RandomizeBot
{
	public class App
	{
		public static IConfiguration Configuration { get; private set; }
		public static AppSettings Settings { get; private set; }
		static App()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			Configuration = builder.Build();
			Settings = Configuration.GetSection("TgSettings").Get<AppSettings>();
		}
	}
}
