using Resources.Models;

namespace Resources.Interfaces;

public interface IFileService
{
    public ServiceResponse<string> SaveToFile(string content);
    public ServiceResponse<string> LoadFromFile();
}
