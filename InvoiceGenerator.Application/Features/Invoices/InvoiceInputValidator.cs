using FluentValidation;
using InvoiceGenerator.Application.Features.Invoices.Dtos;

namespace InvoiceGenerator.Application.Features.Invoices
{
    // Input-only rules; checks that need the database (customer/items/custom fields exist, field enabled) live in InvoiceLinesResolver
    // Internal so AddValidatorsFromAssembly skips it; the request validators pull it in with Include()
    internal class InvoiceInputValidator : AbstractValidator<IInvoiceInput>
    {
        public InvoiceInputValidator()
        {
            RuleFor(r => r.DueDate)
                .GreaterThanOrEqualTo(r => r.InvoiceDate)
                .WithMessage("DueDate cannot be before InvoiceDate.");

            RuleFor(r => r.CustomerId).GreaterThan(0);
            RuleFor(r => r.DiscountAmount).GreaterThanOrEqualTo(0);
            RuleFor(r => r.TaxPercentage).InclusiveBetween(0, 100);

            RuleFor(r => r.Items)
                .NotEmpty()
                .WithMessage("An invoice needs at least one item.");

            RuleForEach(r => r.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ItemId).GreaterThan(0);
                item.RuleFor(i => i.Quantity).GreaterThan(0);
                item.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0).When(i => i.UnitPrice.HasValue);
            });

            RuleFor(r => r.CustomFields)
                .Must(fields => fields!.Select(f => f.CustomFieldId).Distinct().Count() == fields!.Count)
                .When(r => r.CustomFields is not null)
                .WithMessage("Each custom field can only be set once per invoice.");

            RuleForEach(r => r.CustomFields).ChildRules(field =>
            {
                field.RuleFor(f => f.CustomFieldId).GreaterThan(0);
                field.RuleFor(f => f.Value).NotEmpty().MaximumLength(500);
            });
        }
    }
}
