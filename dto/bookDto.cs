namespace books.api.dto;

public record bookDto(
    int id,
    string name,
    string author,
    string genre,
    decimal price
);
