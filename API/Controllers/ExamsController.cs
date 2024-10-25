using Application.Commands.Exams;
using Application.Models;
using Application.Models.ResponseDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ExamDto>>>> GetAllExams()
        {
            var exams = await _examAppService.GetAllExamsAsync();

            if (exams == null || !exams.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<ExamDto>>(false, "No exams found", null, 404));
            }

            var response = new ApiResponse<IEnumerable<ExamDto>>(true, "Exams retrieved successfully", exams, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific exam by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ExamDto>>> GetExamById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<ExamDto>(false, "Invalid exam ID", null, 400));
            }

            var exam = await _examAppService.GetExamByIdAsync(id);
            if (exam == null)
            {
                return NotFound(new ApiResponse<ExamDto>(false, "Exam not found", null, 404));
            }

            var response = new ApiResponse<ExamDto>(true, "Exam retrieved successfully", exam, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new exam.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int?>>> CreateExam([FromBody] ExamDto examDto)
        {
            if (examDto == null)
            {
                return BadRequest(new ApiResponse<int?>(false, "Exam data is required", null, 400));
            }

            var createdExamId = await _examAppService.CreateExamAsync(new CreateExamCommand(examDto));
            var response = new ApiResponse<int?>(true, "Exam created successfully", createdExamId, 201);

            return CreatedAtAction(nameof(GetExamById), new { id = createdExamId }, response);
        }

        /// <summary>
        /// Updates an existing exam.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateExam(int id, [FromBody] ExamDto examDto)
        {
            if (id <= 0 || examDto == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid exam ID or data", null, 400));
            }

            var existingExam = await _examAppService.GetExamByIdAsync(id);
            if (existingExam == null)
            {
                return NotFound(new ApiResponse<string>(false, "Exam not found", null, 404));
            }

            await _examAppService.UpdateExamAsync(new UpdateExamCommand(id, examDto));
            var response = new ApiResponse<string>(true, "Exam updated successfully", null, 200);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an exam by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteExam(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid exam ID", null, 400));
            }

            var exam = await _examAppService.GetExamByIdAsync(id);
            if (exam == null)
            {
                return NotFound(new ApiResponse<string>(false, "Exam not found", null, 404));
            }

            await _examAppService.DeleteExamAsync(id);
            var response = new ApiResponse<string>(true, "Exam deleted successfully", null, 204);
            return Ok(response);
        }
    }
}