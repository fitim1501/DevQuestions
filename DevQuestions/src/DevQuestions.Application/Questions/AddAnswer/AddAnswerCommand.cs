using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions.AddAnswer;

public record AddAnswerCommand(Guid QuestionId, AddAnswerDto AddAnswerDto);