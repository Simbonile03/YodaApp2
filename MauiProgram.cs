
using Microsoft.Extensions.Logging;
using YodaApp2.Configuration;
using YodaApp2.Services;
using YodaApp2.ViewModels;
using YodaApp2.Views;
namespace YodaApp2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //    builder.Services.AddTransient<ILoggingService, loggingService>();
            //    builder.Services.AddTransient<ISettingsService, SettingsService>();
            builder.RegisterServices()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<AIAssistant, OpenAIService>();
            mauiAppBuilder.Services.AddTransient<AiSettings, ConstantSettings>();

            // More services registered here.

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainView>();


            // More view-models registered here.

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainPage>();


            // More views registered here.

            return mauiAppBuilder;
        }
    }
}
