using Application.Commands.Exams;
using Application.Models;
using Application.Models.ResponseDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<ApiResponse<IEnumerable<ExamDto>>>> GetAllExams()
        {
            var exams = await _examAppService.GetAllExamsAsync();
            var response = new ApiResponse<IEnumerable<ExamDto>>(true, "Exams retrieved successfully", exams, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific exam by its ID.
        /// </summary>
        /// <param name="id">The ID of the exam to retrieve.</param>
        /// <returns>The ExamDto of the requested exam, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ExamDto>>> GetExamById(int id)
        {
            var exam = await _examAppService.GetExamByIdAsync(id);
            if (exam == null)
            {
                var errorResponse = new ApiResponse<ExamDto>(false, "Exam not found", null, 404);
                return NotFound(errorResponse);
            }
            var response = new ApiResponse<ExamDto>(true, "Exam retrieved successfully", exam, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new exam.
        /// </summary>
        /// <param name="examDto">The details of the exam to be created.</param>
        /// <returns>The ID of the newly created exam.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateExam([FromBody] ExamDto examDto)
        {
            var createdExamId = await _examAppService.CreateExamAsync(new CreateExamCommand(examDto));
            var response = new ApiResponse<int>(true, "Exam created successfully", createdExamId, 201);
            return CreatedAtAction(nameof(GetExamById), new { id = createdExamId }, response);
        }

        /// <summary>
        /// Updates an existing exam.
        /// </summary>
        /// <param name="id">The ID of the exam to update.</param>
        /// <param name="examDto">The updated details of the exam.</param>
        /// <returns>Confirmation that the update was successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateExam(int id, [FromBody] ExamDto examDto)
        {
            if (id != examDto.Id)
            {
                var errorResponse = new ApiResponse<string>(false, "Exam ID in the request does not match the one in the body.", null, 400);
                return BadRequest(errorResponse);
            }

            await _examAppService.UpdateExamAsync(new UpdateExamCommand(id, examDto));
            var response = new ApiResponse<string>(true, "Exam updated successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an exam by its ID.
        /// </summary>
        /// <param name="id">The ID of the exam to delete.</param>
        /// <returns>Confirmation that the deletion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteExam(int id)
        {
            await _examAppService.DeleteExamAsync(id);
            var response = new ApiResponse<string>(true, "Exam deleted successfully", null, 204);
            return Ok(response);
        }
    }
}