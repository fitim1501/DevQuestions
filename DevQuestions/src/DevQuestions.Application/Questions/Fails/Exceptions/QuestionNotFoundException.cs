﻿using System.Text.Json;
using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Application.Questions.Exceptions;

public class QuestionNotFoundException : NotFoundException
{
    protected QuestionNotFoundException(Error[] errors)
        : base(errors)
    {
    }
}