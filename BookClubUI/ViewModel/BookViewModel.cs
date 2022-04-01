using BookClub.Data;
using BookClub.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookClub.UI.ViewModel
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private BookClubContext _Context;
        private Book _selectedBook;


        public event PropertyChangedEventHandler PropertyChanged;

        public BookViewModel()
        {
            Readers = new ObservableCollection<Reader>();
            Readings = new ObservableCollection<Reading>();
            Books = new ObservableCollection<Book>();
            AddBookCommand = new DelegateCommand(OnAddBookExecute);
            SaveBookCommand = new DelegateCommand(OnSaveBookExecute, OnSaveBookCanExecute);
            DeleteBookCommand = new DelegateCommand(OnDeleteBookExecute, OnDeleteBookCanExecute);
            //DeleteBookCommand = new DelegateCommand(OnDeleteReadingExecute, OnDeleteReadingCanExecute);
            //AddBookCommand = new DelegateCommand(OnAddReadingExecute);
        }

        public ICommand AddBookCommand { get; }

        public ICommand SaveBookCommand { get; }

        public ICommand DeleteBookCommand { get; }

        //public ICommand DeleteReadingCommand { get; }

        //public ICommand AddReadingCommand { get; }

        public ObservableCollection<Reader> Readers { get; set; }

        public ObservableCollection<Reading> Readings { get; set; }

        public ObservableCollection<Book> Books { get; set; }

        public Book SelectedBook
        {
            get
            {
                return _selectedBook;
            }
            set
            {
                _selectedBook = value;
                Readings.Clear();


                if (_selectedBook != null)
                {
                    foreach (Reading reading in _Context.Readings.Where(r => r.ReaderId == SelectedBook.Id))
                    {
                        Readings.Add(reading);
                    }
                }


                OnPropertyChanged();
                OnPropertyChanged("IsBookSelected");
                OnPropertyChanged("Book");
                ((DelegateCommand)DeleteBookCommand).RaiseCanExecuteChanged();
            }
        }

        public Author SelectedAuthor { get; set; }

        public bool IsBookSelected => SelectedBook != null;

        //public Book SelectedBook
        //{
        //    get
        //    {
        //        return _selectedBook;
        //    }
        //    set
        //    {
        //        _selectedBook = value;
        //        ((DelegateCommand)DeleteBookCommand).RaiseCanExecuteChanged();
        //    }
        //}



        public void InitializeContext()
        {
            _Context = new BookClubContext();
        }

        public void LoadBook()
        {
            Books.Clear();
            foreach (Book b in _Context.Books.OrderBy(b => b.Title))
            {
                Books.Add(b);
            }
        }

        //public void LoadBooks()
        //{
        //    Books.Clear();
        //    foreach (Book b in _Context.Books.OrderBy(b => b.Title))
        //    {
        //        Books.Add(b);
        //    }
        //}

        private void OnAddBookExecute()
        {
            Book book = new Book();
            Books.Add(book);
            SelectedBook = book;
        }

        private bool OnSaveBookCanExecute()
        {
            if (!string.IsNullOrEmpty(SelectedBook?.Title?.Trim()))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        private void OnSaveBookExecute()
        {
            bool newBook = false;
            Book selected = SelectedBook;



            if (SelectedBook?.Id == 0 && !string.IsNullOrEmpty(SelectedBook?.Title?.Trim()))
            {
                newBook = true;
                _Context.Attach(SelectedBook);
            }

            _Context.SaveChanges();

            if (newBook)
            {
                LoadBook();
                SelectedBook = selected;
            }

        }

        private bool OnDeleteBookCanExecute()
        {
            if (SelectedBook?.Authors.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDeleteBookExecute()
        {


            if (SelectedBook != null && !SelectedBook.Authors.Any())
            {
                if (SelectedBook.Id != 0)
                {
                    _Context.Books.Remove(SelectedBook);
                    _Context.SaveChanges();
                    LoadBook();
                }
                Books.Remove(SelectedBook);

                if (Books.Count > 0)
                {
                    SelectedBook = Books[0];
                }
            }
        }
        //private bool OnDeleteBookCanExecute()
        //{
        //    if (SelectedReading != null)
        //    {
        //        return true;

        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //private void OnDeleteReadingExecute()
        //{
        //    if (SelectedReader != null && SelectedReading != null)
        //    {
        //        SelectedReader.Readings.Remove(SelectedReading);
        //        Readings.Remove(SelectedReading);
        //    }

        //}

        //private void OnAddReadingExecute()
        //{
        //    if (SelectedReader != null && SelectedBook != null)
        //    {
        //        Reading reading = new Reading();
        //        reading.BookId = SelectedBook.Id;
        //        reading.ReaderId = SelectedReader.Id;
        //        SelectedReader.Readings.Add(reading);
        //        Readings.Add(reading);
        //        _Context.Attach(reading);
        //    }
        //}


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

