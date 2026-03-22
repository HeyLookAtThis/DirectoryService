using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Posotions;

public sealed class Position
{
    private List<Guid> _departments;
    
    private Position(Guid id, Name name, Description description)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = true;
        CreateAt = DateTime.UtcNow;
        UpdateAt = CreateAt;

        _departments = new List<Guid>();
    }

    public Guid Id { get; private set; }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreateAt { get; private set; }

    public DateTime UpdateAt { get; private set; }

    public List<Guid> Departments => _departments;

    public static Result<Position, Error> Create(string name, string? description)
    {
        string invalidField = "Position";

        Result<Name, Error> nameResult = ValueObjects.Name.Create(name, invalidField);

        if (nameResult.IsFailure)
            return nameResult.Error;

        Result<Description, Error> descriptionResult = ValueObjects.Description.Create(description, invalidField);

        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        return new Position(Guid.NewGuid(), nameResult.Value, descriptionResult.Value);
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