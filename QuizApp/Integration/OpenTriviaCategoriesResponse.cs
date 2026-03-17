using System.Text.Json.Serialization;

namespace QuizApp.Integration;

public record OpenTriviaCategoriesResponse(
    [property: JsonPropertyName("trivia_categories")] List<OpenTriviaCategoryDto> TriviaCategories
);
