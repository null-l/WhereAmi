using System;
using System.Windows;

namespace WhereAmi
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private volatile IApplication _application;
        private volatile LocationApplicationViewModel _applicationViewModel;

        public App()
        {
            _applicationViewModel = new LocationApplicationViewModel();
            _applicationViewModel.ListenCommand.Subscribe( OnListenCommand );
            _applicationViewModel.CloseCommand.Subscribe( OnCloseCommand );
        }

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );
            MainWindow = new MainWindow();
            MainWindow.Show();
            if( !ValidateArgs( e.Args, out var speechRegion, out var speechApiKey, out var locationApiKey ) )
            {
                MainWindow.Close();
                return;
            }
            _application = ApplicationFactory.Create(
                speechApiKey,
                speechRegion,
                locationApiKey
            );
            MainWindow.DataContext = _applicationViewModel;
        }

        private static bool ValidateArgs( string[] args, out string speechRegion, out string speechApiKey, out string locationApiKey )
        {
            var isValid = args.Length == 3;
            if( isValid )
            {
                speechRegion = args[0];
                speechApiKey = args[1];
                locationApiKey = args[2];
                return true;
            }
            MessageBox.Show
            (
                @"Please restart a application with the following command line arguments:
<Speech-API-Region-Name> <Speech-API-Key> <Location-API-Key>"
            );
            speechRegion = speechApiKey = locationApiKey = null;
            return false;
        }

#region Overrides of Application
        private void OnCloseCommand()
        {
            MainWindow.Close();
            _application.Dispose();
        }

        private void OnListenCommand( bool state )
        {
            if( state )
            {
                _application.StartListening();
            }
            else
            {
                _application.StopListening();
            }
        }
#endregion
    }

}