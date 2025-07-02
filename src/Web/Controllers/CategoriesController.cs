using Application.Common.Interfaces.Messaging.Requests.Dispatchers;
using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.Get;
using Application.Features.Categories.Queries.GetById;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Categories;

namespace Web.Controllers;

[Authorize(Roles = nameof(UserRole.Admin))]
public class CategoriesController(IRequestDispatcher dispatcher) : ApiController(dispatcher)
{
    public async Task<IActionResult> Index()
    {
        var result = await Dispatcher.SendAsync(new GetCategoriesQuery());

        return View(result.Data);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var result = await Dispatcher.SendAsync(new GetCategoryByIdQuery(id));
        if (!result.Succeeded)
            return View("NotFound");

        return View(result.Data);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryViewModel category)
    {
        if (ModelState.IsValid)
        {
            var result = await Dispatcher.SendAsync(new CreateCategoryCommand(category.Name, category.Description));
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Category created successfully.";
                return RedirectToAction(nameof(Index));
            }

            AddModelErrors(result);
        }

        return View(category);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await Dispatcher.SendAsync(new GetCategoryByIdQuery(id));
        if (!result.Succeeded)
            return View("NotFound");

        var category = result.Data!;

        return View(new EditeCategoryViewModel() { Id = category.Id, Name = category.Name, Description = category.Description });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] EditeCategoryViewModel category)
    {
        if (ModelState.IsValid)
        {
            var result = await Dispatcher.SendAsync(new UpdateCategoryCommand(category.Id, category.Name, category.Description));
            if (!result.Succeeded)
                return View("NotFound");

            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Dispatcher.SendAsync(new GetCategoryByIdQuery(id));
        if (!result.Succeeded)
            return View("NotFound");

        var category = result.Data!;

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var result = await Dispatcher.SendAsync(new DeleteCategoryCommand(id));
        if (!result.Succeeded)
            return View("NotFound");

        TempData["SuccessMessage"] = "Category deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
