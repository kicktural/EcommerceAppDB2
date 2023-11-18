using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PictureController : Controller
    {

        private readonly IWebHostEnvironment _env;

        public PictureController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<JsonResult> UploadPictures(List<IFormFile> photoUrls)
        {

            List<string> Pictures = new();

            for (int i = 0; i < photoUrls.Count; i++)
            {
                string path = "/Uploads/" + Guid.NewGuid() + photoUrls[i].FileName;
                using FileStream fileStream = new(_env.WebRootPath + path, FileMode.Create);
                await photoUrls[i].CopyToAsync(fileStream);

                Pictures.Add(path);
            }

            return Json(Pictures);
        }
    }
}
