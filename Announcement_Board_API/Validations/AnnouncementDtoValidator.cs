using Announcement_Board_API.DTOs;
using Announcement_Board_API.Models;
using FluentValidation;

namespace Announcement_Board_API.Validations
{
    public class AnnouncementDtoValidator : AbstractValidator<AnnouncementDto>
    {

        public AnnouncementDtoValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(a => a.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => status.ToLower() == "active" || status.ToLower() == "inactive")
                .WithMessage("Status must be either 'active' or 'inactive'.");

            RuleFor(a => a.Category)
                .NotEmpty().WithMessage("Category is required.")
                .Must(category => AvailableCategories.ValidCategories.ContainsKey(category))
                .WithMessage("Invalid category.");

            RuleFor(a => a.SubCategory)
                .NotEmpty().WithMessage("SubCategory is required.")
                .Must((announcement, subCategory) =>
                    AvailableCategories.ValidCategories.TryGetValue(announcement.Category, out var subCategories) &&
                    subCategories.Contains(subCategory))
                .WithMessage("Invalid subcategory for the selected category.");
        }
    }
}
