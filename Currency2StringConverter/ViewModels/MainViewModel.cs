using System;
using System.Windows;
using System.Windows.Input;
using Currency2StringConverter.Commands;
using Currency2StringConverter.ConverterServiceReference;

namespace Currency2StringConverter.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string convertionResult;
        private string connectedStatus = Properties.Resources.DisconnectedText;
        private ConverterServiceClient service;
        private ICommand convertCommand;

        public MainViewModel()
        {
            convertionResult = Properties.Resources.DefaultConvertionResultText;
            convertCommand = new ButtonCommand(ConvertText, null);
            service = new ConverterServiceClient();
        }

        public string ConversionResult
        {
            get => convertionResult;
            set => RaiseAndSetIfChanged(ref convertionResult, value);
        }

        public string ConnectedStatus
        {
            get => connectedStatus;
            set => RaiseAndSetIfChanged(ref connectedStatus, value);
        }

        public ICommand ConvertCommand { get => convertCommand; }

        protected override void Dispose(bool disposing)
        {
            if (service != null
                && (service.State == System.ServiceModel.CommunicationState.Opened
                    || service.State == System.ServiceModel.CommunicationState.Opening))
                service.Close();
            base.Dispose(disposing);
        }

        private bool OpenConnection()
        {
            if (service.State != System.ServiceModel.CommunicationState.Opened
                && service.State != System.ServiceModel.CommunicationState.Opening)
            {
                try
                {
                    service.Open();
                    if (service.TestConnetion() == "OK")
                    {
                        ConnectedStatus = Properties.Resources.ConnectedText;
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    // Here should be logging
                    MessageBox.Show(ex.Message, "Currency2StringConverter", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return false;
            }
            return true;
        }

        private void ConvertText(object obj)
        {
            if (!OpenConnection()) return;
            ConversionResult = service.ConvertString(Convert.ToString(obj));
        }
    }
}
