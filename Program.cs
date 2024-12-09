//using EF_DataAccess.Data;
//using EF_Models.Models;
//using Microsoft.EntityFrameworkCore;

////using (ApplicationDbContext context = new()) 
////{
////    context.Database.EnsureCreated();
////    if(context.Database.GetPendingMigrations().Count() > 0)
////    {
////        context.Database.Migrate();
////    }
////}


////AddBook();
////GetAllBooks();
////GetBook();
////GetBookByPid(2);
////GetUsingFind(); // just userd when the primary key is given

////GetBookSkip();
//UpdateBook();
//GetAllBooks();
//DeleteBook();
//GetAllBooks();

//void DeleteBook()
//{
//    using var context = new ApplicationDbContext();
//    var books = context.Books.Find(4);
//    context.Books.Remove(books);
//    context.SaveChanges();
//}

//void UpdateBook()
//{
//    using var context = new ApplicationDbContext();
//    var books = context.Books.Where(u => u.BookID == 1).ToList();
//    foreach (var book in books)
//    {
//        book.Price = 29.99;  // Set the new price
//    }
//    context.SaveChanges();
//}

//void GetBookSkip()
//{
//    using var context = new ApplicationDbContext();
//    int skip = 0;
//    int take = 1;
//    var books = context.Books.Skip(skip).Take(take);
//    foreach (var item in books)
//    {
//        Console.WriteLine(item.Title + " " + item.ISBN + " " + item.Publisher_Id);
//    }
//}
//void GetUsingFind()
//{
//    using var context = new ApplicationDbContext();
//    //var books = context.Books.Find(2);
//    var books = context.Books.SingleOrDefault(u => u.Publisher_Id == 3); // this just gives when there is single entry for that value
//    Console.WriteLine(books.Title + " " + books.ISBN + " " + books.Publisher_Id);
//}

//void GetBookByPid(int id)
//{
//    using var context = new ApplicationDbContext();

//    //Book books = context.Books.First();
//    var books = context.Books.Where(b => b.Publisher_Id == id && b.ISBN.Equals("124578")).ToList();
//    if (books != null &&  books.Count > 0)
//    {
//        foreach (var item in books)
//        {
//            Console.WriteLine(item.Title + " " + item.ISBN + " " + item.Publisher_Id);
//        }
//    }
//    else
//    {
//        Console.WriteLine("no book with this publisher id found");
//    }
//}

//void GetBook()
//{
//    using var context = new ApplicationDbContext();

//    //Book books = context.Books.First();
//    Fluent_Book? books = context.fluent_Books.FirstOrDefault();
//    if (books != null)
//    {
//        Console.WriteLine(books.Title + " " + books.ISBN);
//    }
//    else
//    {
//        Console.WriteLine("table empty");
//    }
//}

//void GetAllBooks()
//{
//    using var context = new ApplicationDbContext();

//    var books = context.Books.ToList();
//    foreach (var item in books)
//    {
//        Console.WriteLine(item.Title + " " + item.ISBN + " " + item.Price);
//    }
//}

//void AddBook()
//{
//    using var context = new ApplicationDbContext();
//    Book book = new() { Title = "Thor", ISBN = "12654375", Price = 10.99, Publisher_Id= 2};
//    var books = context.Books.Add(book);
//    context.SaveChanges();

//}

Console.WriteLine();