using System.Text.Json.Serialization;

namespace QuizApp.Integration;

public record OpenTriviaQuizResponse(
    [property: JsonPropertyName("response_code")] int ResponseCode,
    [property: JsonPropertyName("results")] List<OpenTriviaQuestionDto> Results
);
