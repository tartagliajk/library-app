namespace AdminHandlesBook
{
    public class Book
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public Book(string bookId, string bookAuthor, string bookName, int bookYear) {
            Id = bookId;
            Author = bookAuthor;
            Name = bookName;
            Year = bookYear;
        }
    }
}