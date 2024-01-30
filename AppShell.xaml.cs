namespace Dolgozat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ListEditor", typeof(ListEditor));
        }
    }
}