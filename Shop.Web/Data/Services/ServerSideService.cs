using Shop.Web.Data.Services.Interfaces;
using System.Security.Cryptography;

namespace Shop.Web.Data.Services
{
    public class ServerSideService : IServerSideService
    {
        public async Task<bool> DeleteFile(string path)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\", path);
            if (File.Exists(file))
            {
                File.Delete(file);
                return true;
            }
            return false;
        }

        public async Task<string> CodeGenerator(int length)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);
                string result = Convert.ToBase64String(bytes);
                result = result.Replace('/', '_').Replace('+', '-'); // Replace URL-unsafe characters
                return result.Substring(0, length); // Output first n characters of the string
            }
        }

        public async Task<string> UploadFile(IFormFile file, string path)
        {
            var CP = await CodeGenerator(30) + file.FileName;
            var uploadpath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Uploads\",path,CP);

            using (var stream = new FileStream(uploadpath,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return CP;
        }
    }
}
