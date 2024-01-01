using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PayScale.Models;
using PayScale.WebRazor.Models;

namespace PayScale.WebRazor.Pages.CalculateTaxes
{
    public class CreateModel : PageModel
    {

        public List<SelectListItem> Options { get; set; }

        public List<PostalCodeTaxType> PostalCodeTaxTypes { get; set; }

        public List<SelectListItem> PostalCodeOptions { get; set; }

        public TaxCalculation TaxCalculation { get; set; }

        public void OnGet()
        {
            //var postalCode = _db.PostalCode.Select(s => new { s.Code, s.Id } );
            //Options = postalCode.Select(a =>
            //                   new SelectListItem
            //                   {
            //                       Value = a.Id.ToString(),
            //                       Text = a.Code
            //                   }).ToList();

           // PostalCodeOptions = 
                
        }

        public IActionResult OnPost()
        {
            //TO DO: Create logic to Calculate taxes
            //_db.TaxCalculation.Add(TaxCalculation);
            //_db.SaveChanges();
            //TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
    }
}
