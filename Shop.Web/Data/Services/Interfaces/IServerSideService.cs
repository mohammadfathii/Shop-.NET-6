namespace Shop.Web.Data.Services.Interfaces
{
    public interface IServerSideService {

        Task<string> UploadFile(IFormFile file,string path);
        Task<bool> DeleteFile(string path);
        Task<string> CodeGenerator(int length);
        string TokenGenerator(int length);
    }
}
