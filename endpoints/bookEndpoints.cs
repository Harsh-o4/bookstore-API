using books.api.dto;

namespace books.api.endpoints;

public static class bookEndpoints
{
    const string newGetBook = "getBook";
    private static readonly List<bookDto> books = [
        new(1,"Physics","HC Verma","Physics",350),
        new(2,"Maths","RD Sharma","Maths",600),
        new(3,"Maths","RS Agarwal","Maths",500)
    ];

    public static void mapBooksEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/books");
        //GET ALL
        group.MapGet("/", () => books);

        //GET by ID
        group.MapGet("/{id}", (int id) =>
        {
            var book = books.Find(book => book.id == id);

            return book is null ? Results.NotFound() : Results.Ok(book);
        }).WithName("getBook");

        //POST
        group.MapPost("/", (createBookDto newBook) =>
        {
            bookDto book = new(books.Count + 1, newBook.name, newBook.author, newBook.genre, newBook.price);

            books.Add(book);

            return Results.CreatedAtRoute(newGetBook, new { id = book.id }, book);
        });

        //PUT
        group.MapPut("/{id}", (int id, updateBookDto book) =>
        {
            var idx = books.FindIndex(book => book.id == id);

            if (idx == -1)
            {
                return Results.NotFound();
            }

            books[idx] = new bookDto(id, book.name, book.author, book.genre, book.price);

            return Results.NoContent();
        });

        //DELETE
        group.MapDelete("/{id}", (int id) =>
        {
            books.RemoveAll(book => book.id == id);
            return Results.NoContent();
        });


    }
}
