namespace books.api.dto;

public record createBookDto(
    string name,
    string author,
    string genre,
    decimal price
);