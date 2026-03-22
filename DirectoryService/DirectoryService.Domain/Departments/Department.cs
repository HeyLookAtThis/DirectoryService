using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;
using Path = DirectoryService.Domain.ValueObjects.Path;

namespace DirectoryService.Domain.Departments;

public sealed class Department
{
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

        Locations = new DepartmentLocation(Id);
        Positions = new DepartmentPosition(Id);
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

    public DepartmentLocation Locations { get; private set; }
    
    public DepartmentPosition Positions { get; private set; }

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

    public void Delete()
    {
        IsActive = false;
        UpdateAt = DateTime.UtcNow;
    }
}