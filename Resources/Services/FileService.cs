using Resources.Interfaces;
using Resources.Models;
using System.Data;

namespace Resources.Services;

public class FileService : IFileService
{
    private readonly string _filePath;

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public ServiceResponse<string> SaveToFile(string content)
    {
        try
        {
            using var sw = new StreamWriter(_filePath, false);
            sw.WriteLine(content);

            return new ServiceResponse<string> { Succeeded = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<string> { Succeeded = false, Message = ex.Message };
        }
    }
    public ServiceResponse<string> LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("\n\t No file was found.");
            }
            else
            {
                using var sr = new StreamReader(_filePath);
                var content = sr.ReadToEnd();

                return new ServiceResponse<string> { Succeeded = true, Result = content };
            }
        }
        catch (Exception ex)
        {
            return new ServiceResponse<string> { Succeeded = false, Message = ex.Message };
        }
    }
}
