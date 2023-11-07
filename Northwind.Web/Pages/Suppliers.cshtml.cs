using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using Coldons.Lib;

namespace Northwind.Web.Pages;
public class SuppliersModel : PageModel
{
    private NorthwindContext db;
    public SuppliersModel(NorthwindContext injectedContext)
    {
        db = injectedContext;
    }

    public IEnumerable<Supplier>? Suppliers { get; set; }
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";

        Suppliers = db.Suppliers
            .OrderBy(c => c.Country)
            .ThenBy(c => c.CompanyName);
    }
}
