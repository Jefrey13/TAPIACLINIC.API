//public class AssignExamsService
//{
//    private readonly IConsultationExamRepository _consultationExamRepository;

//    public AssignExamsService(IConsultationExamRepository consultationExamRepository)
//    {
//        _consultationExamRepository = consultationExamRepository;
//    }

//    public void AssignExams(int consultationId, List<int> examIds, Dictionary<int, string> examResults)
//    {
//        // Iterate through the list of exams and assign each to the consultation
//        foreach (var examId in examIds)
//        {
//            var consultationExam = new ConsultationExam
//            {
//                ConsultationId = consultationId,
//                ExamId = examId,
//                Result = examResults.ContainsKey(examId) ? examResults[examId] : null,
//                ExamDate = DateTime.Now,
//                CreatedAt = DateTime.Now,
//                UpdatedAt = DateTime.Now
//            };

//            _consultationExamRepository.Add(consultationExam);
//        }
//    }
//}