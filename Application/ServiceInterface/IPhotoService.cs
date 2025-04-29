using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Application.ServiceInterface;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeleteAsync(string publicId);
}
