using System.Diagnostics;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace WhereAmi
{

    public sealed class WhereAmISpeechAction : ISpeechAction
    {
        private readonly ILocationInformationService _locationInformationService;

        public WhereAmISpeechAction( ILocationInformationService locationInformationService )
        {
            _locationInformationService = locationInformationService;
        }

        public async Task ExecuteAsync( CancellationToken cancellationToken = default )
        {
            var countryName = await _locationInformationService.GetLocationCountryNameAsync( cancellationToken )
                .ConfigureAwait( false );
            using (var automation = new UIA3Automation())
            using (var notepad = StartNotepad( countryName ))
            {
                var notepadWindow = notepad.GetMainWindow( automation );
                try
                {
                    ProcessModalFileDialog( notepadWindow );
                    ProcessKeyboardInput();
                    ProcessFileSave( notepadWindow );
                }
                catch
                {
                    // ToDo: Exception message handling
                }
                finally
                {
                    notepadWindow.Close();
                }
            }
        }

        private void ProcessModalFileDialog( Window mainWindow )
            => mainWindow.ModalWindows
                .FirstOrDefault()?
                .FindFirstByXPath( "//Button[@Name='Yes']" )?
                .AsButton()?
                .Click();

        private void ProcessKeyboardInput()
        {
            Keyboard.TypeSimultaneously( VirtualKeyShort.CONTROL, VirtualKeyShort.END ); // Move to the bottom of file
            Keyboard.Press( VirtualKeyShort.F5 );                                        // Insert current date and time
            Keyboard.Press( VirtualKeyShort.RETURN );                                    // Append new line
        }

        private void ProcessFileSave( Window mainWindow )
        {
            ProcessModalFileDialog( mainWindow );
            mainWindow.FindFirstByXPath( "/MenuBar/MenuItem[@Name='File']" ).AsMenuItem().Click();
            mainWindow.FindFirstByXPath( "/Menu[@Name='File']/MenuItem[@Name='Save']" ).AsMenuItem().Click();
        }

        private Application StartNotepad( string countryName )
        {
            var executablePath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Windows ), "notepad.exe" );
            var locationFilePath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ), $"{countryName}.txt" );
            var startInfo = new ProcessStartInfo( executablePath, $"\"{locationFilePath}\"" )
            {
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Maximized
            };
            return Application.Launch( startInfo );
        }
    }

}