using BookClub.UI.ViewModel;
using Prism.Commands;
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
    public partial class BookView : UserControl
    {

        private BookViewModel _viewModel;
        public BookView()
        {
            InitializeComponent();
            _viewModel = new BookViewModel();
            DataContext = _viewModel;
            Loaded += BookView_Loaded;
        }

        private void BookView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.InitializeContext();
            _viewModel.LoadBook();

            if (_viewModel.Books?.Count > 0)
            {
                lvBooks.SelectedItem = _viewModel.Readers[0];
            }
            _viewModel.LoadBook();

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((DelegateCommand)_viewModel.SaveBookCommand).RaiseCanExecuteChanged();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
