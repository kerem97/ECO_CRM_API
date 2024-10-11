using DtoLayer.TaskAssignmentFile.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.TaskAssignmentFileServices
{
    public interface ITaskAssignmentFileService
    {
        Task AddTaskAssignmentFile(AddTaskAssignmentFileRequest request);
    }
}
