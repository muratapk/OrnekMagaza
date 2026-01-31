using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrnekMagaza.Data;
using OrnekMagaza.Models;
using System.IO;    
namespace OrnekMagaza.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly MagazaDb _context;

        public ProductImagesController(MagazaDb context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductImages.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }
        [HttpGet]
        public IActionResult CokluResim(int id)
        {
            ViewBag.ProductId = _context.Products.Where(p => p.ProductID == id).Select(p => p.ProductID).FirstOrDefault();
            return View();

        }
        [HttpPost]
        public IActionResult CokluResim(List<IFormFile> ImageFile, int ProductId)
        {
            foreach (var ImageFl in ImageFile)
            {
                // Burada her bir dosyayı işleyebilirsiniz
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFl.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFl.CopyTo(stream);
                }
                ProductImage pi = new ProductImage();
                pi.ProductId = ProductId;
                pi.ImageUrl = "/images/" + fileName;
                _context.ProductImages.Add(pi);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Products", new { id = ProductId });
        }


        // GET: ProductImages/Create
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.Products, "ProductID", "ProductName");
            //Viewbag ile ürünleri dropdown liste olarak gönderiyoruz
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductImageId,ImageUrl,ProductId,IsMain,Title")] ProductImage productImage,IFormFile ImageFile)
        {
            long maxFileSize = 5 * 1024 * 1024; // 5 MB
            string[] allowedExtensions= { ".jpg", ".jpeg", ".png", ".gif",".webp" };

            if (ImageFile != null && ImageFile.Length > 0)
            {  
                if (ImageFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("ImageFile", "Dosya Boyutu 5MB Büyük Olduğu için kabul edilmez.");
                    return View(productImage);
                }
                if (!allowedExtensions.Contains(Path.GetExtension(ImageFile.FileName).ToLower()))
                {
                    //dizi içerisnde yoksa contains içine var mı kontrol dosya uzanıtı toLower ile küçük harfe çevirir
                    ModelState.AddModelError("ImageFile", "Yalnızca JPG, JPEG, PNG, GIF ve WEBP dosya türlerine izin verilir.");
                    return View(productImage);
                }

                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                //path.getExtension gelen dosyanın uzantısını alır .jpg .png gibi

                var filePath = Path.Combine("wwwroot/images", fileName);
                // Save the file to the server nereye kayıt olacağını
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                    //filestream dosyayı oluşturur ve kopyalar
                }
                // Set the ImageUrl property to the relative path
                productImage.ImageUrl = "/images/" + fileName;
            }








            if (ModelState.IsValid)
            {
                _context.Add(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = new SelectList(_context.Products, "ProductID", "ProductName");
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            ViewBag.Products = new SelectList(_context.Products, "ProductID", "ProductName");
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductImage productImage)
        {
            long maxFileSize = 5 * 1024 * 1024; // 5 MB
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            
            if (productImage.ImageFile != null && productImage.ImageFile.Length > 0)
            {
                if (productImage.ImageFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("ImageFile", "Dosya Boyutu 5MB Büyük Olduğu için kabul edilmez.");
                    return View(productImage);
                }
                if (!allowedExtensions.Contains(Path.GetExtension(productImage.ImageFile.FileName).ToLower()))
                {
                    //dizi içerisnde yoksa contains içine var mı kontrol dosya uzanıtı toLower ile küçük harfe çevirir
                    ModelState.AddModelError("ImageFile", "Yalnızca JPG, JPEG, PNG, GIF ve WEBP dosya türlerine izin verilir.");
                    return View(productImage);
                }

                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productImage.ImageFile.FileName);
                //path.getExtension gelen dosyanın uzantısını alır .jpg .png gibi

                var filePath = Path.Combine("wwwroot/images", fileName);
                // Save the file to the server nereye kayıt olacağını
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productImage.ImageFile.CopyToAsync(stream);
                    //filestream dosyayı oluşturur ve kopyalar
                }
                // Set the ImageUrl property to the relative path
                productImage.ImageUrl = "/images/" + fileName;
            }
            else
            {
                // If no new image is uploaded, retain the existing image URL
                var existingImage = await _context.ProductImages.AsNoTracking().FirstOrDefaultAsync(x => x.ProductImageId == id);
                productImage.ImageUrl = existingImage.ImageUrl;
            }

            if (id != productImage.ProductImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImageExists(productImage.ProductImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = new SelectList(_context.Products, "ProductID", "ProductName");
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var productImage = await _context.ProductImages.FindAsync(id);
            var filepath = Path.Combine("wwwroot", productImage.ImageUrl.TrimStart('/'));

            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            if (productImage != null)
            {
                _context.ProductImages.Remove(productImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.ProductImageId == id);
        }
    }
}
