using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public record Description
{
    private const int MAX_LENGTH = 1000;

    private Description(string? description) => Value = description;

    public string? Value { get; }

    public static Result<Description, Error> Create(string? description, string invalidField)
    {
        if (description != null)
            if (description.Length > MAX_LENGTH)
                return Error.Validation(invalidField + ".description", "слишком длинное описание", invalidField);

        return new Description(description);
    }
}