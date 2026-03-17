namespace QuizApp.Models;

public record QuizQuestion(
    string Category,
    string Type,
    string Difficulty,
    string Question,
    string CorrectAnswer,
    List<string> IncorrectAnswers
);
