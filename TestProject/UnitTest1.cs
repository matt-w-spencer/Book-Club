using BookClub.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class ReaderUnitTest
    {
        [TestMethod]
    public void AddBookToReadings()
        {

            //Arrange
            Reader reader = new Reader();
            reader.Name = "John Doe";

            Book book = new Book();
            book.Title = "Book 1";

            //Act
            Reading reading = new Reading();
            reading.BookId = book.Id;
            reading.ReaderId = reader.Id;

            reader.Readings.Add(reading);

            //Assert
            Assert.AreEqual(reader.Readings.Count, 1);
        }
    }
}
