using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.Application.Features.Authors.Queries.GetAuthors;
using MindLog.Application.Features.Books.Commands.UpdateBook;
using MindLog.Application.Features.Books.Dtos;
using MindLog.Application.Features.Books.Queries.GetBookDetails;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Books;

public class EditModel : PageModel
{
    private readonly IMediator _mediator;

    public EditModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class InputModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

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

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var details = await _mediator.Send(new GetBookDetailsQuery(id), cancellationToken);
            Input = new InputModel
            {
                Id = details.Id,
                AuthorId = details.AuthorId,
                Title = details.Title,
                Slug = details.Slug,
                CoverImagePath = details.CoverImagePath,
                Status = details.Status,
                Category = details.Category,
                ShortSummary = details.ShortSummary,
                FullReview = details.FullReview,
                Rating = details.Rating
            };

            await LoadOptionsAsync(cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }

        var dto = new UpdateBookDto(
            Input.Id,
            Input.AuthorId,
            Input.Title,
            Input.Slug,
            Input.CoverImagePath,
            Input.Status,
            Input.Category,
            Input.ShortSummary,
            Input.FullReview,
            Input.Rating);

        var command = new UpdateBookCommand(
            dto.Id,
            dto.AuthorId,
            dto.Title,
            dto.Slug,
            dto.CoverImagePath,
            dto.Status,
            dto.Category,
            dto.ShortSummary,
            dto.FullReview,
            dto.Rating
        );

        try
        {
            await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "ویرایش کتاب با موفقیت انجام شد.";
            return RedirectToPage("Details", new { id = Input.Id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
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