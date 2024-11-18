using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DtoLayer.CustomerOperation.Responses;
using DtoLayer.CustomerOperationFile.Requests;
using DtoLayer.CustomerOperationFile.Responses;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.CustomerOperationsFileServices
{
    public class CustomerOperationFileService : ICustomerOperationFileService
    {
        private readonly ICustomerOperationFileRepository _fileRepository;
        private readonly ICustomerOperationRepository _customerOperationRepository;

        public CustomerOperationFileService(ICustomerOperationFileRepository fileRepository, ICustomerOperationRepository customerOperationRepository)
        {
            _fileRepository = fileRepository;
            _customerOperationRepository = customerOperationRepository;
        }

        public async Task AddFileAsync(AddCustomerOperationFileRequest request)
        {
            // CustomerOperationId doğrulama
            var operation = await _customerOperationRepository.GetById(request.CustomerOperationId);
            if (operation == null)
            {
                throw new Exception("Invalid CustomerOperationId. The operation does not exist.");
            }

            // Dosya yükleme klasörü oluşturma
            var uploadFolder = Path.Combine("wwwroot", "uploads", "customeroperations");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Dosya kaydetme
            var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            // Dosya kaydını ekleme
            var file = new CustomerOperationFile
            {
                CustomerOperationId = request.CustomerOperationId,
                FilePath = filePath,
                FileName = fileName
            };

            await _fileRepository.Add(file);
        }



        public async Task<IEnumerable<DisplayCustomerOperationFileResponse>> GetFilesByOperationIdAsync(int operationId)
        {
            var files = await _fileRepository.GetFilesByOperationIdAsync(operationId);

            if (files == null || !files.Any())
            {
                return Enumerable.Empty<DisplayCustomerOperationFileResponse>();
            }

            return files.Select(f => new DisplayCustomerOperationFileResponse
            {
                Id = f.Id,
                FilePath = f.FilePath,
                FileName = f.FileName,
                UploadedDate = f.UploadedDate
            });
        }



        public async Task DeleteFileAsync(int id)
        {
            await _fileRepository.Delete(id);
        }
    }

}
