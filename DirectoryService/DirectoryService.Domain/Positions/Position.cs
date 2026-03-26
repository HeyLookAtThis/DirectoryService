using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Positions;

public sealed class Position
{
    private List<DepartmentPosition> _departments = new();
    
    private Position(Guid id, Name name, Description description)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = true;
        CreateAt = DateTime.UtcNow;
        UpdateAt = CreateAt;
    }

    public Guid Id { get; private set; }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreateAt { get; private set; }

    public DateTime UpdateAt { get; private set; }

    public IReadOnlyList<DepartmentPosition> Departments => _departments;

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
        _departments.Add(new DepartmentPosition(departmentId, Id));
        Update();
    }

    public Result<bool, Error> TryToRemovePosition(Guid departmentId)
    {
        var removingDepartmentPosition =
            _departments.FirstOrDefault(departmentPosition => departmentPosition.DepartmentId == departmentId);

        if (removingDepartmentPosition != null)
        {
            _departments.Remove(removingDepartmentPosition);
            Update();
            return true;
        }

        return Error.NotFound("position " + Id, "отсутствует подразделение " + departmentId, "position");
    }

    private void Update() => UpdateAt = DateTime.UtcNow;
}