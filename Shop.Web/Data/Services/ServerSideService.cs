using Shop.Web.Data.Services.Interfaces;

namespace Shop.Web.Data.Services
{
    public class ServerSideService : IServerSideService
    {
        public async Task<bool> DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public Task<string> CodeGenerator(int length)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFile(IFormFile file, string path)
        {
            var CP = await CodeGenerator(30) + file.FileName;
            var uploadpath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Uploads\",path,CP);

            using (var stream = new FileStream(uploadpath,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uploadpath;
        }
    }
}
