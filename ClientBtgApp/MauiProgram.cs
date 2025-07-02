using ClientBtgApp.Base.DbContext;
using ClientBtgApp.Base.Repository;
using ClientBtgApp.Base.Repository.Interfaces;
using ClientBtgApp.ViewModels;
using ClientBtgApp.Views;
using Microsoft.Extensions.Logging;

namespace ClientBtgApp
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

            builder.Services.AddSingleton<DbContext>();
            builder.Services.AddSingleton<IClientRepository, ClientRepository>();

            builder.Services.AddBindingContext<ClientListViewModel, ClientListPage>();
            builder.Services.AddBindingContext<ClientEditAddViewModel, ClientEditAddPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void AddBindingContext<TViewModel, TView>(this IServiceCollection services)
            where TView : ContentPage, new()
            where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient<TView>(s => new TView() { BindingContext = s.GetRequiredService<TViewModel>() });
        }
    }
}
