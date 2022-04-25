using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSIT133Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT133Web.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;
        public ApiController(DemoContext context, IWebHostEnvironment hostingEnvironment)
        //public ApiController(IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _host = hostingEnvironment;
        }
        //public IActionResult Index(string name, string age)
        public IActionResult Index(User user)
        {
            //return Content("獲得一隻貓");
            //System.Threading.Thread.Sleep(5000);
            if (string.IsNullOrEmpty(user.name))
            {
                user.name = "貓咪";
            }
            //int.TryParse(user.age, out int num);
            if (!user.age.HasValue)
            {
                user.age = 0;
            }
            return Content($"<h2>獲得一隻貓：{user.name}，{user.age}歲</h2>", "text/html", System.Text.Encoding.UTF8);
        }
        public IActionResult Register(Member member, IFormFile photo)
        {

            //DemoContext _context = new DemoContext();
            //if (string.IsNullOrEmpty(user.name))
            //{
            //    user.name = "貓咪";
            //}
            //if (!user.age.HasValue)
            //{
            //    user.age = 0;
            //}
            //return Content($"<h2>獲得一隻貓：{user.name}，{user.age}歲，正在看{user.email}</h2>", "text/html", System.Text.Encoding.UTF8);

            //將檔案存到uploads資料夾
            string uploadFolder = Path.Combine(_host.WebRootPath, "uploads", photo.FileName);
            using (var filestream = new FileStream(uploadFolder, FileMode.Create))
            {
                photo.CopyTo(filestream);
            }

            //將圖檔轉成二進位 MemoryStream
            byte[] imgByte = null;
            using (MemoryStream stream = new MemoryStream())
            {
                photo.CopyTo(stream);
                imgByte = stream.ToArray();
            }

            //寫進資料庫
            member.FileName = photo.FileName;
            member.FileData = imgByte;
            _context.Add(member);
            _context.SaveChanges();

            //string info = $"{_host.WebRootPath} - {_host.ContentRootPath}";
            string info = $"{photo.FileName} - {photo.Length} - {photo.ContentType}";
            return Content(info, "text/html", System.Text.Encoding.UTF8);
        }
        public IActionResult Address()
        {
            return View();
        }
        public IActionResult City()
        {
            var cities = _context.Addresses.Select(c => new { c.City }).Distinct().OrderBy(c => c.City);
            return Json(cities);
        }
        public IActionResult Districts(string city)
        {
            var districts = _context.Addresses.Where(a => a.City == city).Select(a => new { a.SiteId }).Distinct().OrderBy(a => a.SiteId);
            return Json(districts);
        }
        public IActionResult Roads(string siteid)
        {
            var roads = _context.Addresses.Where(a => a.SiteId == siteid).Select(a => new { a.Road }).Distinct().OrderBy(a => a.Road);
            return Json(roads);
        }
        public IActionResult GetImageByte(int id = 1)
        {
            Member member = _context.Members.Find(id);
            byte[] img = member.FileData;
            return File(img, "image/jpeg");
        }
    }
}
