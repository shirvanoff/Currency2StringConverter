using System;
using System.Windows;
using Currency2StringConverter.ViewModels;

namespace Currency2StringConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModelBase viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();

            DataContext = viewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                viewModel?.Dispose();
            }
            catch (Exception ex)
            {
                // Here should be logging of the exception
            }
            base.OnClosed(e);
        }
    }
}
