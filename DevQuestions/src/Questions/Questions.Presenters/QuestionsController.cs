﻿using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Questions;
using DevQuestions.Application.Questions.Features.AddAnswerCommand;
using DevQuestions.Application.Questions.Features.CreateQuestionCommand;
using DevQuestions.Application.Questions.Features.GetQuestionsWithFiltersQuery;
using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Presenters.ResponseExtenstions;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters.Questions
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] ICommandHandler<Guid, CreateQuestionCommand> commandHandler,
            [FromBody] CreateQuestionDto request,
            CancellationToken cancellationToken)
        {
            var command = new CreateQuestionCommand(request);

            var result = await commandHandler.Handle(command, cancellationToken);
            return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<QuestionResponse, GetQuestionsWithFiltersQuery> queryHandler,
            [FromQuery] GetQuestionDto request,
            CancellationToken cancellationToken)
        {
            var query = new GetQuestionsWithFiltersQuery(request);

            var result = await queryHandler.Handle(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{questionId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid questionId, CancellationToken cancellationToken)
        {
            return Ok("Questions get");
        }

        [HttpPut("{questionId:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid questionId,
            [FromBody] UpdateQuestionDto request,
            CancellationToken cancellationToken)
        {
            return Ok("Questions updated");
        }

        [HttpDelete("{questionId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid questionId, CancellationToken cancellationToken)
        {
            return Ok("Questions deteled");
        }

        [HttpPut("{questionId:guid}/solution")]
        public async Task<IActionResult> SelectSolution(
            [FromRoute] Guid questionId,
            [FromQuery] Guid answerId,
            CancellationToken cancellationToken)
        {
            return Ok("Solution selected");
        }

        [HttpPost("{questionId:guid}/answers")]
        public async Task<IActionResult> AddAnswer(
            [FromServices] ICommandHandler<Guid, AddAnswerCommand> commandHandler,
            [FromRoute] Guid questionId,
            [FromBody] AddAnswerDto request,
            CancellationToken cancellationToken)
        {
            var command = new AddAnswerCommand(questionId, request);

            var result = await commandHandler.Handle(command, cancellationToken);
            return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
        }
    }
}