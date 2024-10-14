using Application.Commands.Exams;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamAppService _examAppService;

        public ExamsController(IExamAppService examAppService)
        {
            _examAppService = examAppService;
        }

        /// <summary>
        /// Retrieves all the exams in the system.
        /// </summary>
        /// <returns>A list of ExamDto containing details of all exams.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetAllExams()
        {
            var exams = await _examAppService.GetAllExamsAsync();
            return Ok(exams);
        }

        /// <summary>
        /// Retrieves a specific exam by its ID.
        /// </summary>
        /// <param name="id">The ID of the exam to retrieve.</param>
        /// <returns>The ExamDto of the requested exam, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDto>> GetExamById(int id)
        {
            var exam = await _examAppService.GetExamByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Ok(exam);
        }

        /// <summary>
        /// Creates a new exam.
        /// </summary>
        /// <param name="examDto">The details of the exam to be created.</param>
        /// <returns>The ID of the newly created exam.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateExam([FromBody] ExamDto examDto)
        {
            var createdExamId = await _examAppService.CreateExamAsync(new CreateExamCommand(examDto));
            return CreatedAtAction(nameof(GetExamById), new { id = createdExamId }, createdExamId);
        }

        /// <summary>
        /// Updates an existing exam.
        /// </summary>
        /// <param name="id">The ID of the exam to update.</param>
        /// <param name="examDto">The updated details of the exam.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, [FromBody] ExamDto examDto)
        {
            if (id != examDto.Id)
            {
                return BadRequest("Exam ID in the request does not match the one in the body.");
            }

            await _examAppService.UpdateExamAsync(new UpdateExamCommand(id, examDto));
            return NoContent();
        }

        /// <summary>
        /// Deletes an exam by its ID.
        /// </summary>
        /// <param name="id">The ID of the exam to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            await _examAppService.DeleteExamAsync(id);
            return NoContent();
        }
    }
}