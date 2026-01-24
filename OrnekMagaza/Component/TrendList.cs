using Microsoft.AspNetCore.Mvc;
using OrnekMagaza.Data;
namespace OrnekMagaza.Component
{
    public class TrendList:ViewComponent
    {
        private readonly MagazaDb _context;
        public TrendList(MagazaDb context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var liste = _context.Products.Take(5).ToList();
            return View(liste);
        }
    }
}
