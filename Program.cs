using books.api.dto;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string newGetBook = "getBook";

List<bookDto> books = [
  new(1,"Physics","HC Verma","Physics",350),
  new(2,"Maths","RD Sharma","Maths",600),
  new(3,"Maths","RS Agarwal","Maths",500)  
];

//GET ALL
app.MapGet("/books", () => books);

//GET by ID
app.MapGet("/books/{id}",(int id) =>
{
    var book = books.Find(book=>book.id==id);

    return book is null? Results.NotFound():Results.Ok(book);
}).WithName("getBook");

//POST
app.MapPost("/books",(createBookDto newBook) =>
{
  bookDto book = new(books.Count+1,newBook.name,newBook.author,newBook.genre,newBook.price);

  books.Add(book);

  return Results.CreatedAtRoute(newGetBook,new{id=book.id},book);
});

//PUT
app.MapPut("/books/{id}",(int id,updateBookDto book) =>
{ 
  var idx = books.FindIndex(book=>book.id==id);

  if (idx == -1)
  {
    return Results.NotFound();
  }

  books[idx] = new bookDto(id,book.name,book.author,book.genre,book.price);

  return Results.NoContent();
});

//DELETE
app.MapDelete("/books/{id}",(int id) =>
{
  books.RemoveAll(book=>book.id==id);
  return Results.NoContent();
});


app.Run();
