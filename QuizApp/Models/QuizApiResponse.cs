namespace QuizApp.Models;

public record QuizApiResponse(
    int ResponseCode,
    List<QuizQuestion> Results
);
