using FluentValidation;
using Announcement_Board_API.Params;
using Announcement_Board_API.Models;

namespace Announcement_Board_API.Validations
{
    public class FilterParamsValidator : AbstractValidator<FilterParams>
    {
        public FilterParamsValidator()
        {
            RuleFor(x => x.Category)
                .Must(category => category == null || AvailableCategories.ValidCategories.ContainsKey(category))
                .WithMessage("Invalid category.");

            RuleFor(x => x.SubCategory)
                .Must((filterParams, subCategory) =>
                    subCategory == null ||
                        (filterParams.Category != null &&
                        (AvailableCategories.ValidCategories.ContainsKey(filterParams.Category) &&
                        AvailableCategories.ValidCategories[filterParams.Category].Contains(subCategory))))
                .WithMessage("Invalid subcategory.");
        }
    }
}
