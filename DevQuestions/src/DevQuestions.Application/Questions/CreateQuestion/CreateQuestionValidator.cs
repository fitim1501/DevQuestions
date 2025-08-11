﻿using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.CreateQuestion;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Заголовок не может быть пустым").WithErrorCode("title.code")
            .MaximumLength(500).WithMessage("Заголовок невалидный");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Заголовок не может быть пустым")
            .MaximumLength(5000).WithMessage("Текст невалидный");

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}