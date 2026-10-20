using InvoiceGenerator.Application.Common;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Application.Features.Invoices
{
    public record InvoiceLines(List<InvoiceItem> Items, List<InvoiceCustomField> CustomFields);

    // Shared by create and update: runs the checks that need the database and turns the input into entities.
    // Input-only rules (dates, amounts, quantities, duplicates, value length) are in InvoiceInputValidator.
    internal static class InvoiceLinesResolver
    {
        public static async Task<Result<InvoiceLines>> ResolveAsync(
            int customerId,
            IReadOnlyList<InvoiceItemInput> items,
            IReadOnlyList<InvoiceCustomFieldInput>? customFields,
            ICustomerRepository customerRepository,
            IItemRepository itemRepository,
            ICustomFieldRepository customFieldRepository,
            CancellationToken cancellationToken)
        {
            customFields ??= [];

            if (await customerRepository.GetByIdAsync(customerId, cancellationToken) is null)
            {
                return Error.Validation($"Customer {customerId} does not exist.");
            }

            var itemIds = items.Select(i => i.ItemId).Distinct().ToList();
            var foundItems = (await itemRepository.FindAsync(i => itemIds.Contains(i.Id), cancellationToken))
                .ToDictionary(i => i.Id);
            var missingItemIds = itemIds.Where(id => !foundItems.ContainsKey(id)).ToList();
            if (missingItemIds.Count > 0)
            {
                return Error.Validation($"Items do not exist: {string.Join(", ", missingItemIds)}.");
            }

            var customFieldIds = customFields.Select(cf => cf.CustomFieldId).ToList();
            var foundCustomFields = (await customFieldRepository.FindAsync(cf => customFieldIds.Contains(cf.Id), cancellationToken))
                .ToDictionary(cf => cf.Id);
            var missingCustomFieldIds = customFieldIds.Where(id => !foundCustomFields.ContainsKey(id)).ToList();
            if (missingCustomFieldIds.Count > 0)
            {
                return Error.Validation($"Custom fields do not exist: {string.Join(", ", missingCustomFieldIds)}.");
            }

            var disabledCustomField = foundCustomFields.Values.FirstOrDefault(cf => !cf.Enabled);
            if (disabledCustomField is not null)
            {
                return Error.Validation($"Custom field '{disabledCustomField.Name}' is disabled.");
            }

            var invoiceItems = items
                .Select(i => new InvoiceItem
                {
                    ItemId = i.ItemId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice ?? foundItems[i.ItemId].Price
                })
                .ToList();

            var invoiceCustomFields = customFields
                .Select(cf => new InvoiceCustomField
                {
                    CustomFieldId = cf.CustomFieldId,
                    Value = cf.Value
                })
                .ToList();

            return new InvoiceLines(invoiceItems, invoiceCustomFields);
        }
    }
}
