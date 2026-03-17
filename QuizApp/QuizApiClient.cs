using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace QuizApp;

public class QuizApiClient(HttpClient httpClient) : IDisposable
{

    public static QuizApiClient Create()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://opentdb.com/"),
            Timeout = TimeSpan.FromSeconds(30)
        };

        return new QuizApiClient(httpClient);
    }

    public void Dispose() => httpClient.Dispose();

    public async Task<List<QuizQuestion>> GetQuizQuestionsAsync()
    {
        var url = "api.php?amount=5&difficulty=easy&type=multiple";

        var response = await httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var quizResult = await response.Content.ReadFromJsonAsync<OpenTriviaQuizResponse>();

        if (quizResult is null || quizResult.ResponseCode != 0)
        {
            throw new InvalidOperationException("Failed to fetch quiz questions from the API.");
        }

        var Result = new List<QuizQuestion>();

        foreach (var item in quizResult.Results)
        {
            Result.Add(new QuizQuestion
            (
                CleanUpHtmlEntities(item.Category),
                CleanUpHtmlEntities(item.Type),
                CleanUpHtmlEntities(item.Difficulty),
                CleanUpHtmlEntities(item.Question),
                CleanUpHtmlEntities(item.CorrectAnswer),
                item.IncorrectAnswers
            ));
        }

        return Result;
    }

    private string CleanUpHtmlEntities(string input)
    {
        return System.Net.WebUtility.HtmlDecode(input);
    }
}




public record QuizApiResponse(
    int ResponseCode,
    List<QuizQuestion> Results
);

public record QuizQuestion(
    string Category,
    string Type,
    string Difficulty,
    string Question,
    string CorrectAnswer,
    List<string> IncorrectAnswers
);


public record OpenTriviaQuizResponse(
    [property: JsonPropertyName("response_code")] int ResponseCode,
    [property: JsonPropertyName("results")] List<OpenTriviaQuestionDto> Results
);

public record OpenTriviaCategoriesResponse(
    [property: JsonPropertyName("trivia_categories")] List<OpenTriviaCategoryDto> TriviaCategories
);

public record OpenTriviaCategoryDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name
);

public record OpenTriviaQuestionDto(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("difficulty")] string Difficulty,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("question")] string Question,
    [property: JsonPropertyName("correct_answer")] string CorrectAnswer,
    [property: JsonPropertyName("incorrect_answers")] List<string> IncorrectAnswers
);