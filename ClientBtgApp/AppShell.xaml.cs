using ClientBtgApp.Views;

namespace ClientBtgApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ClientListPage), typeof(ClientListPage));
            Routing.RegisterRoute(nameof(ClientEditAddPage), typeof(ClientEditAddPage));
        }
    }
}
