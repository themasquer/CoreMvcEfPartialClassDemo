using EFpartialClassDemo.Entities;
using EFpartialClassDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFpartialClassDemo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            var products = _productService.Query().ToList();
            return View(products);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            var product = _productService.Query().SingleOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryService.Query().ToList(), "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
           if (ModelState.IsValid)
            {
                if (_productService.Add(product))
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Product with same name exists!");
            }
            ViewBag.CategoryId = new SelectList(_categoryService.Query().ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productService.Query().SingleOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            ViewBag.CategoryId = new SelectList(_categoryService.Query().ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (_productService.Update(product))
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Product with same name exists!");
            }
            ViewBag.CategoryId = new SelectList(_categoryService.Query().ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = _productService.Query().SingleOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
