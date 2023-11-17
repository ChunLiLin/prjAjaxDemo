using Microsoft.AspNetCore.Mvc;
using prjAjaxDemo.Models;
using prjAjaxDemo.Models.ViewModels;

namespace prjAjaxDemo.Controllers
{
    public class ApiController : Controller
    {
        //==================
        private readonly IWebHostEnvironment _host;
        private readonly DemoContext _context;
        public ApiController(IWebHostEnvironment host, DemoContext context)
        {
            _host = host;
            _context = context;
        }
        //===================
        public IActionResult Index(string name,int age=28)
        {
			//System.Threading.Thread.Sleep(5000);
			if (string.IsNullOrEmpty(name))
            {
                name = "Guast";
            }
            return Content($"<h2>Hello {name}, Its been {age} days after NMSL</h2>","text/html",System.Text.Encoding.UTF8);
        }
        public IActionResult register(Members? member,IFormFile formFile)
        {
            string strPath = "";
            if (formFile != null)
            {
                strPath = Path.Combine(_host.WebRootPath, "uploads", formFile.FileName);
                using (var fileStream = new FileStream(strPath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
                if (member.Name != null)
                {
                    member.FileName = formFile.FileName;
                    byte[]? imgByte = null;
                    using (var memoryStream = new MemoryStream())
                    {
                        formFile.CopyTo(memoryStream);
                        imgByte = memoryStream.ToArray();
                    }
                    member.FileData = imgByte;
                    _context.Members.Add(member);
                    _context.SaveChanges();
                    return Content("新增成功");
                }
            }           
            return Content("錯誤");            
            //return Content(strPath);
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.name}，{member.email},  You are {member.age} years old.");
        }
        [HttpPost]
        public IActionResult checkAccount(MemberViewModel? member) 
        {
            if (member != null)
            {
                string txt = member.name;

                if (txt != null)
                {
                    string feedback = "讚讚讚";
                    foreach (var item in _context.Members)
                    {
                        if (item.Name == txt)
                        {
                            feedback = "幹幹幹";
                        }
                    }
                    return Content(feedback);
                }
            }
            return Content("沒事");
        }
        public IActionResult Cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }
        public IActionResult districts(string city)
        {
            var districts = _context.Address.Where(c => c.City == city).Select(s => s.SiteId).Distinct();
            return Json(districts);
        }
        public IActionResult Roads(string siteId)
        {
            var roads = _context.Address.Where(s => s.SiteId == siteId).Select(r => r.Road);
            return Json(roads);
        }
        public IActionResult GetImageByte(int id)
        {
            Members member = _context.Members.Find(id);
            byte[] img = member.FileData;
            if (img != null)
            {
                return File(img,"image/jpeg");
            }
            return NotFound();
        }
    }
}
