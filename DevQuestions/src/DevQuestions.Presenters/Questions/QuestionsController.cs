﻿using DevQuestions.Contracts.Questions;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters.Questions
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuestionDto request, CancellationToken cancellationToken)
        {
            return Ok("Questions created");
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetQuestionDto request, CancellationToken cancellationToken)
        {
            return Ok("Questions get");
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
            [FromRoute] Guid questionId,
            [FromBody] AddAnswerDto request,
            CancellationToken cancellationToken)
        {
            return Ok("Answer added");
        }
    }
}
