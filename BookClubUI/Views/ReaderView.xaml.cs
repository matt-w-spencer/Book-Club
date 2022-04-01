using BookClub.UI.ViewModel;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookClub.UI.Views
{
    /// <summary>
    /// Interaction logic for ReaderView.xaml
    /// </summary>
    public partial class ReaderView : UserControl
    {

        private ReaderViewModel _viewModel;
        public ReaderView()
        {
            InitializeComponent();
            _viewModel = new ReaderViewModel();
            DataContext = _viewModel;
            Loaded += ReaderView_Loaded;
        }

        private void ReaderView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.InitializeContext();
            _viewModel.LoadReaders();
            
            if(_viewModel.Readers?.Count > 0)
            {
                lvReaders.SelectedItem = _viewModel.Readers[0];
            }
            _viewModel.LoadBooks();

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((DelegateCommand)_viewModel.SaveReaderCommand).RaiseCanExecuteChanged();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
