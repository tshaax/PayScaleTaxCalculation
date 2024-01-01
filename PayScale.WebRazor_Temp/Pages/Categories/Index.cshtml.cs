//using PayScale.WebRazor.Data;
//using PayScale.WebRazor.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace BulkyWebRazor_Temp.Pages.Categories
//{
//    public class IndexModel : PageModel
//    {
//        private readonly ApplicationDbContext _db;
//        public List<Category> CategoryList { get; set; }
//        public IndexModel(ApplicationDbContext db)
//        {
//            _db = db;
//        }

//        public void OnGet()
//        {
//            CategoryList = new List<Category>
//            {
//                new Category
//                { 
//                    Id = 1, DisplayOrder = 0, 
//                    Name = "James"
//                },//_db.Categories.ToList();
//            };
//        }
//    }
//}
