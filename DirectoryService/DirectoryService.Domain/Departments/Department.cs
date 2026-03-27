using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;
using Path = DirectoryService.Domain.ValueObjects.Path;

namespace DirectoryService.Domain.Departments;

public sealed class Department
{
    private List<DepartmentLocation> _locations = new();
    private List<DepartmentPosition> _positions = new();
    
    private Department(Guid id, Name name, Identifier identifier, Guid? parentId, Path path, Depth depth)
    {
        Id = id;
        Name = name;
        Identifier = identifier;
        ParentId = parentId;
        Path = path;
        Depth = depth;
        IsActive = true;
        CreateAt = DateTime.UtcNow;
        UpdateAt = CreateAt;
    }

    public Guid Id { get; private set; }

    public Name Name { get; private set; }

    public Identifier Identifier { get; private set; }

    public Guid? ParentId { get; private set; }

    public Path Path { get; private set; }

    public Depth Depth { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreateAt { get; private set; }

    public DateTime UpdateAt { get; private set; }

    public IReadOnlyList<DepartmentLocation> Locations => _locations;

    public IReadOnlyList<DepartmentPosition> Positions => _positions;

    public static Result<Department, Error> Create(string name, string identifier, Guid? parentId, Path? parentPath)
    {
        string invalidField = "department";

        Result<Name, Error> nameResult = Name.Create(name, invalidField);

        if (nameResult.IsFailure)
            return nameResult.Error;

        Result<Identifier, Error> identifierResult = Identifier.Create(identifier, invalidField);

        if (identifierResult.IsFailure)
            return identifierResult.Error;

        Path path = new(parentPath, identifier);
        Depth depth = new(path);

        return new Department(Guid.NewGuid(), nameResult.Value, identifierResult.Value, parentId, path, depth);
    }

    public void AddLocation(Guid locationId)
    {
        _locations.Add(new DepartmentLocation(Id, locationId));
        Update();
    }
    
    public void AddPosition(Guid positionId)
    {
        _positions.Add(new DepartmentPosition(Id, positionId));
        Update();
    }
    
    public Result<bool, Error> TryToRemoveLocation(Guid locationId)
    {
        var removingDepartmentLocation =
            _locations.FirstOrDefault(departmentLocation => departmentLocation.LocationId == locationId);

        if (removingDepartmentLocation != null)
        {
            _locations.Remove(removingDepartmentLocation);
            Update();
            return true;
        }

        return Error.NotFound("department " + Id, "отсутствует локация " + locationId, "department");
    }
    
    public Result<bool, Error> TryToRemovePosition(Guid positionId)
    {
        var removingDepartmentPosition =
            _positions.FirstOrDefault(departmentPosition => departmentPosition.PositionId == positionId);

        if (removingDepartmentPosition != null)
        {
            _positions.Remove(removingDepartmentPosition);
            Update();
            return true;
        }

        return Error.NotFound("department " + Id, "отсутствует позиция " + positionId, "department");
    }

    public void Delete()
    {
        IsActive = false;
        Update();
    }
    
    private void Update() => UpdateAt = DateTime.UtcNow;
}