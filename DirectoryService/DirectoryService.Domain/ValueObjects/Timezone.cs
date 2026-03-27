using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public record Timezone
{
    private Timezone(string timezoneId) => Value = timezoneId;

    public string Value { get; }

    public static Result<Timezone, Error> Create(TimeZoneInfo timeZoneInfo, string invalidField)
    {
        if (timeZoneInfo.HasIanaId == false)
            return Error.Validation(invalidField + ".Timezone", "ошибка часового пояса", invalidField);

        return new Timezone(timeZoneInfo.Id);
    }
}