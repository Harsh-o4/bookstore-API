namespace books.api.dto;

public record updateBookDto(
    string name,
    string author,
    string genre,
    decimal price
);
