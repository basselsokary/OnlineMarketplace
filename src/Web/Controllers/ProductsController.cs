using Application.Common.Interfaces.Messaging.Requests.Dispatchers;
using Application.Features.Categories.Queries.Get;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Products;

namespace Web.Controllers;

[Authorize]
public class ProductsController(IRequestDispatcher dispatcher) : ApiController(dispatcher)
{
    [AllowAnonymous]
    public async Task<IActionResult> Index(Guid categoryId)
    {
        var categoriesResult = await Dispatcher.SendAsync(new GetCategoriesQuery());
        ViewBag.Categories = new SelectList(categoriesResult.Data, "Id", "Name", categoryId);

        var productsResult = await Dispatcher.SendAsync(new GetProductsQuery(categoryId));
        return View(productsResult.Data);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await Dispatcher.SendAsync(new GetProductByIdQuery(id));

        if (!result.Succeeded)
            return View("NotFound");

        return View(result.Data);
    }

    public async Task<ActionResult> Create()
    {
        await ViewCategories();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var command = new CreateProductCommand(
                viewModel.Name,
                viewModel.Description,
                new(viewModel.PriceAmount, viewModel.PriceCurrency),
                viewModel.StockQuantity,
                viewModel.SelectedCategoryIds,
                viewModel.ImageUrl
            );

            var productResult = await Dispatcher.SendAsync(command);
            if (productResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Product created successfully.";
                return RedirectToAction(nameof(Index));
            }

            AddModelErrors(productResult);
        }

        await ViewCategories();
        return View(viewModel);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await Dispatcher.SendAsync(new GetProductByIdQuery(id));
        if (!result.Succeeded)
            return View("NotFound");

        var product = result.Data;

        var viewModel = new EditProductViewModel
        {
            Id = product!.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price.Amount,
            StockQuantity = product.UnitsInStock,
            SelectedCategoryIds = product.Categories.Select(c => c.Id).ToList(),
            ImageUrl = product.ImageUrl
        };

        await ViewCategories(viewModel.SelectedCategoryIds);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EditProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await Dispatcher.SendAsync(new UpdateProductCommand(
                viewModel.Id,
                viewModel.Name,
                viewModel.Description,
                new(viewModel.Price),
                viewModel.StockQuantity,
                viewModel.SelectedCategoryIds,
                viewModel.ImageUrl
            ));
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Product updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            AddModelErrors(result);
        }

        await ViewCategories(viewModel.SelectedCategoryIds);
        return View(viewModel);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Dispatcher.SendAsync(new GetProductByIdQuery(id));
        if (!result.Succeeded)
            return View("NotFound");

        return View(result.Data);
    }

    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var result = await Dispatcher.SendAsync(new DeleteProductCommand(id));
        if (!result.Succeeded)
            return View("NotFound");

        return RedirectToAction(nameof(Index));
    }

    private async Task ViewCategories(List<Guid>? selectedCategoryIds = null)
    {
        var categories = (await Dispatcher.SendAsync(new GetCategoriesQuery())).Data;

        if (selectedCategoryIds == null)
            ViewData["CategoryList"] = new MultiSelectList(categories, "Id", "Name");
        else
            ViewData["CategoryList"] = new MultiSelectList(categories, "Id", "Name", selectedCategoryIds);
    }
}
