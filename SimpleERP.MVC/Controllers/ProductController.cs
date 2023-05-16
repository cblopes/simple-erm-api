using Microsoft.AspNetCore.Mvc;
using SimpleERP.MVC.Models;
using SimpleERP.MVC.Services;

namespace SimpleERP.MVC.Controllers
{
    public class ProductController : MainController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string? code)
        {
            var products = await _productService.GetProductsAsync();

            if (code != null)
            {
                try
                {
                    var product = await _productService.GetProductByCodeAsync(code);

                    if (product.Code == null) throw new HttpRequestException("Produto não encontrado.");

                    products = new List<ProductViewModel> { product };

                    return View(products);
                }
                catch (HttpRequestException ex)
                {
                    return View(products);
                }
            }

            return View(products);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductModel input)
        {
            try
            {
                //if (!ModelState.IsValid) return View(input);

                var response = await _productService.CreateProductAsync(input);

                if (HasErrorsResponse(response.ResponseResult)) return View(input);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel input)
        {
            try
            {
                ModelState.Remove("Id");
                ModelState.Remove("Code");

                AlterProductModel product = new AlterProductModel { 
                    Description = input.Description, 
                    QuantityInStock = input.QuantityInStock,
                    Price = input.Price
                };

                var response = await _productService.AlterProductAsync(id, product);

                if (HasErrorsResponse(response.ResponseResult)) return View(input);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProductController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _productService.GetProductByIdAsync(id);

            var product = new DeleteProductModel
            {
                Id = response.Id,
                Code = response.Code,
                Description = response.Description,
                QuantityInStock = response.QuantityInStock,
                Price = response.Price
            };

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, DeleteProductModel input)
        {
            try
            {
                ModelState.Remove("ResponseResult");

                var response = await _productService.DeleteProductAsync(id);

                if (HasErrorsResponse(response.ResponseResult)) return View(response);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
