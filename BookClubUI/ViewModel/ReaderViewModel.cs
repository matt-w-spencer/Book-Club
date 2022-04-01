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
    public class ReaderViewModel : INotifyPropertyChanged
    {
        private BookClubContext _Context;
        private Reader _selectedReader;
        private Reading _selectedReading;
        

        public event PropertyChangedEventHandler PropertyChanged;

        public ReaderViewModel()
        {
            Readers = new ObservableCollection<Reader>();
            Readings = new ObservableCollection<Reading>();
            Books = new ObservableCollection<Book>();
            AddReaderCommand = new DelegateCommand(OnAddReaderExecute);
            SaveReaderCommand = new DelegateCommand(OnSaveReaderExecute, OnSaveReaderCanExecute);
            DeleteReaderCommand = new DelegateCommand(OnDeleteReaderExecute, OnDeleteReaderCanExecute);
            DeleteReadingCommand = new DelegateCommand(OnDeleteReadingExecute, OnDeleteReadingCanExecute);
            AddReadingCommand = new DelegateCommand(OnAddReadingExecute);
        }

        public ICommand AddReaderCommand { get; }

        public ICommand SaveReaderCommand { get; }

        public ICommand DeleteReaderCommand { get; }

        public ICommand DeleteReadingCommand { get; }

        public ICommand AddReadingCommand { get; }

        public ObservableCollection<Reader> Readers { get; set; }

        public ObservableCollection<Reading> Readings { get; set; }

        public ObservableCollection<Book> Books { get; set; }

        public Reader SelectedReader
        {
            get
            {
                return _selectedReader;
            }
            set
            {
                _selectedReader = value;
                Readings.Clear();


                if (_selectedReader != null)
                {
                    foreach (Reading reading in _Context.Readings.Where(r => r.ReaderId == SelectedReader.Id))
                    {
                        Readings.Add(reading);
                    }
                }


                OnPropertyChanged();
                OnPropertyChanged("IsReaderSelected");
                OnPropertyChanged("Readings");
                ((DelegateCommand)DeleteReaderCommand).RaiseCanExecuteChanged();
            }
        }

        public Book SelectedBook { get; set; }

        public bool IsReaderSelected => SelectedReader != null;

        public Reading SelectedReading
        {
            get
            {
                return _selectedReading;
            }
            set
            {
                _selectedReading = value;
                ((DelegateCommand)DeleteReadingCommand).RaiseCanExecuteChanged();
            }
        }



        public void InitializeContext()
        {
            _Context = new BookClubContext();
        }

        public void LoadReaders()
        {
            Readers.Clear();
            foreach (Reader r in _Context.Readers.OrderBy(r => r.Name))
            {
                Readers.Add(r);
            }
        }

        public void LoadBooks()
        {
            Books.Clear();
            foreach (Book b in _Context.Books.OrderBy(b => b.Title))
            {
                Books.Add(b);
            }
        }

        private void OnAddReaderExecute()
        {
            Reader reader = new Reader();
            Readers.Add(reader);
            SelectedReader = reader;
        }

        private bool OnSaveReaderCanExecute()
        {
            if (!string.IsNullOrEmpty(SelectedReader?.Name?.Trim()))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        private void OnSaveReaderExecute()
        {
            bool newReader = false;
            Reader selected = SelectedReader;



            if (SelectedReader?.Id == 0 && !string.IsNullOrEmpty(SelectedReader?.Name?.Trim()))
            {
                newReader = true;
                _Context.Attach(SelectedReader);
            }

            _Context.SaveChanges();

            if (newReader)
            {
                LoadReaders();
                SelectedReader = selected;
            }

        }

        private bool OnDeleteReaderCanExecute()
        {
            if (SelectedReader?.Readings.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDeleteReaderExecute()
        {


            if (SelectedReader != null && !SelectedReader.Readings.Any())
            {
                if (SelectedReader.Id != 0)
                {
                    _Context.Readers.Remove(SelectedReader);
                    _Context.SaveChanges();
                    LoadReaders();
                }
                Readers.Remove(SelectedReader);

                if (Readers.Count > 0)
                {
                    SelectedReader = Readers[0];
                }
            }
        }
        private bool OnDeleteReadingCanExecute()
        {
            if (SelectedReading != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        private void OnDeleteReadingExecute()
        {
            if (SelectedReader != null && SelectedReading != null)
            {
                SelectedReader.Readings.Remove(SelectedReading);
                Readings.Remove(SelectedReading);
            }

        }

        private void OnAddReadingExecute()
        {
            if (SelectedReader != null && SelectedBook != null)
            {
                Reading reading = new Reading();
                reading.BookId = SelectedBook.Id;
                reading.ReaderId = SelectedReader.Id;
                SelectedReader.Readings.Add(reading);
                Readings.Add(reading);
                _Context.Attach(reading);
            }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
