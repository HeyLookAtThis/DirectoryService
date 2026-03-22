using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Locations;

public sealed class Location
{
    private List<Guid> _departments;
    
    private Location(Guid id, Name name, Address address, Timezone timezone)
    {
        Id = id;
        Name = name;
        Address = address;
        Timezone = timezone;
        IsActive = true;
        CreateAt = DateTime.UtcNow;
        UpdateAt = CreateAt;

        _departments = new List<Guid>();
    }

    public Guid Id { get; }

    public Name Name { get; private set; }

    public Address Address { get; private set; }

    public Timezone Timezone { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreateAt { get; private set; }

    public DateTime UpdateAt { get; private set; }

    public IReadOnlyList<Guid> Departments => _departments;

    public static Result<Location, Error> Create(
        string name,
        short postalCode,
        string country,
        string region,
        string city,
        string street,
        string house,
        TimeZoneInfo timeZoneInfo)
    {
        string invalidField = "location";

        Result<Name, Error> nameResult = ValueObjects.Name.Create(name, invalidField);

        if (nameResult.IsFailure)
            return nameResult.Error;

        Result<Address, Error> addressResult =
            ValueObjects.Address.Create(postalCode, country, region, city, street, house, invalidField);

        if (addressResult.IsFailure)
            return addressResult.Error;

        Result<Timezone, Error> timezoneResult = ValueObjects.Timezone.Create(timeZoneInfo, invalidField);

        if (timezoneResult.IsFailure)
            return timezoneResult.Error;

        return new Location(
            Guid.NewGuid(),
            nameResult.Value,
            addressResult.Value,
            timezoneResult.Value);
    }

    public void Delete()
    {
        IsActive = false;
        Update();
    }

    public void AddDepartment(Guid departmentId)
    {
        _departments.Add(departmentId);
        Update();
    }

    public void RemoveDepartment(Guid departmentId)
    {
        _departments.Remove(departmentId);
        Update();
    }

    private void Update() => UpdateAt = DateTime.UtcNow;
}