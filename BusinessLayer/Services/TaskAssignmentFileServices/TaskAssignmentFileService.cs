using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DtoLayer.TaskAssignmentFile.Requests;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.TaskAssignmentFileServices
{
    public class TaskAssignmentFileService : ITaskAssignmentFileService
    {
        private readonly ITaskAssignmentFileRepository _taskAssignmentFileRepository;
        private readonly ITaskAssignmentRepository _taskAssignmentRepository;
        private readonly IMapper _mapper;

        public TaskAssignmentFileService(ITaskAssignmentFileRepository taskAssignmentFileRepository, IMapper mapper, ITaskAssignmentRepository taskAssignmentRepository)
        {
            _taskAssignmentFileRepository = taskAssignmentFileRepository;
            _mapper = mapper;
            _taskAssignmentRepository = taskAssignmentRepository;
        }
        public async Task AddTaskAssignmentFile(AddTaskAssignmentFileRequest request)
        {
            //string uploadsFolder = Path.Combine("C:\\Users\\kerem\\source\\repos\\Eco_CRM_Api_Consume_FrontEnd", "wwwroot", "uploads");
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Yükleme yapılacak tam dosya yolunu belirtelim
            string uploadsFolder = Path.Combine(baseDir, "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string filePath = Path.Combine(uploadsFolder, request.File.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }
            var taskAssignment = await _taskAssignmentRepository.GetById(request.TaskAssignmentId);
            if (taskAssignment != null)
            {
                taskAssignment.Status = "Teklif Verildi"; 
                await _taskAssignmentRepository.Update(taskAssignment); 
            }
            TaskAssignmentFile newFile = new TaskAssignmentFile
            {
                TaskAssignmentId = request.TaskAssignmentId,
                FilePath = filePath,
                FileName = request.File.FileName, 
                UploadedDate = DateTime.Now
            };

            await _taskAssignmentFileRepository.Add(newFile);
        }



    }
}
