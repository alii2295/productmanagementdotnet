using gestionproduit.Data;
using gestionproduit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace gestionproduit.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // ✅ READ - Display list of products with sorting, filtering
        public async Task<IActionResult> Index(string searchTerm, decimal? minPrice, decimal? maxPrice, string sortOrder, bool showInactive = false)
        {
            var products = _context.Products
                                   .Where(p => showInactive || p.IsActive) // ✅ Show active products by default
                                   .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm));
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParam"] = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            ViewBag.ShowInactive = showInactive;

            return View(await products.ToListAsync());
        }


        // ✅ CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // ✅ CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (imageFile != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    product.ImagePath = "/images/" + fileName;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                // Set TempData message for success
                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction(nameof(Index));
                TempData["SuccessMessage"] = "Product added successfully!";
            }
            return View(product);
        }

        // ✅ EDIT - GET
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // ✅ EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    product.ImagePath = "/images/" + fileName;
                }

                _context.Update(product);
                await _context.SaveChangesAsync();

                Console.WriteLine($"✅ Product '{product.Name}' updated successfully!");

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // ✅ DELETE - GET
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // ✅ DELETE - POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsActive = false; // ✅ Soft delete by marking inactive
                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = $"Product '{product.Name}' was deleted.";
                TempData["ToastType"] = "error";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsActive = true;
                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = $"Product '{product.Name}' was restored.";
                TempData["ToastType"] = "success";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
