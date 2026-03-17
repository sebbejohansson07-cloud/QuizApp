using System.Text.Json.Serialization;

namespace QuizApp.Integration;

public record OpenTriviaCategoryDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name
);
