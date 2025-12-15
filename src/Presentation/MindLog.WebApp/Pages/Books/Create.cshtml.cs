using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.Application.Features.Authors.Queries.GetAuthors;
using MindLog.Application.Features.Books.Commands.CreateBook;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Books;

[Authorize(Policy = ApplicationRoles.Admin)]
public class CreateModel : PageModel
{
    private readonly IMediator _mediator;

    public CreateModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class InputModel
    {
        [Display(Name = "نویسنده"), Required(ErrorMessage = "انتخاب نویسنده الزامی است.")]
        public Guid AuthorId { get; set; }

        [Display(Name = "عنوان کتاب"), Required(ErrorMessage = "عنوان کتاب را وارد کنید."), StringLength(200, ErrorMessage = "عنوان نباید بیش از ۲۰۰ کاراکتر باشد.")]
        public string Title { get; set; } = default!;

        [Display(Name = "نامک (Slug)"), Required(ErrorMessage = "نامک الزامی است."), StringLength(150, ErrorMessage = "نامک نباید بیش از ۱۵۰ کاراکتر باشد.")]
        public string Slug { get; set; } = default!;

        [Display(Name = "تصویر جلد"), Required(ErrorMessage = "مسیر تصویر جلد را وارد کنید."), StringLength(250, ErrorMessage = "مسیر تصویر نباید بیش از ۲۵۰ کاراکتر باشد.")]
        public string CoverImagePath { get; set; } = default!;

        [Display(Name = "وضعیت مطالعه"), Required(ErrorMessage = "وضعیت مطالعه را مشخص کنید.")]
        public ReadingStatus Status { get; set; }

        [Display(Name = "دسته‌بندی"), Required(ErrorMessage = "دسته‌بندی را مشخص کنید.")]
        public BookCategory Category { get; set; }

        [Display(Name = "خلاصه کوتاه"), Required(ErrorMessage = "خلاصه کوتاه الزامی است."), StringLength(500, ErrorMessage = "خلاصه کوتاه نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string ShortSummary { get; set; } = default!;

        [Display(Name = "مرور کامل"), Required(ErrorMessage = "متن مرور را وارد کنید."), StringLength(4000, ErrorMessage = "مرور کامل نباید بیش از ۴۰۰۰ کاراکتر باشد.")]
        public string FullReview { get; set; } = default!;

        [Display(Name = "امتیاز"), Range(1, 5, ErrorMessage = "امتیاز باید بین ۱ تا ۵ باشد."), Required(ErrorMessage = "امتیاز را مشخص کنید.")]
        public int? Rating { get; set; }
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IReadOnlyList<AuthorListItemDto> Authors { get; private set; } = [];

    public SelectList StatusOptions { get; private set; } = default!;
    public SelectList CategoryOptions { get; private set; } = default!;
    public SelectList AuthorOptions { get; private set; } = default!;

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        await LoadOptionsAsync(cancellationToken);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }

        var command = new CreateBookCommand(
            Input.AuthorId,
            Input.Title,
            Input.Slug,
            Input.CoverImagePath,
            Input.Status,
            Input.Category,
            Input.ShortSummary,
            Input.FullReview,
            Input.Rating
        );

        try
        {
            var id = await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "کتاب جدید با موفقیت ثبت شد.";
            return RedirectToPage("Details", new { id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }
    }

    private async Task LoadOptionsAsync(CancellationToken cancellationToken)
    {
        Authors = await _mediator.Send(new GetAuthorsQuery(), cancellationToken);
        AuthorOptions = new SelectList(
            Authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }),
            "Id",
            "FullName",
            Input.AuthorId);
        StatusOptions = new SelectList(Enum.GetValues<ReadingStatus>().Select(s => new {
            Value = s,
            Name = s switch
            {
                ReadingStatus.Read => "خوانده‌شده",
                ReadingStatus.Reading => "در حال مطالعه",
                _ => "برای مطالعه"
            }
        }), "Value", "Name", Input.Status);
        CategoryOptions = new SelectList(Enum.GetValues<BookCategory>().Select(c => new {
            Value = c,
            Name = c switch
            {
                BookCategory.Technical => "فنی",
                BookCategory.NonTechnical => "غیرفنی",
                _ => "متفرقه"
            }
        }), "Value", "Name", Input.Category);
    }
}
