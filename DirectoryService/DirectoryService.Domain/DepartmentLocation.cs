namespace DirectoryService.Domain;

public record DepartmentLocation
{
    public DepartmentLocation(Guid departmentId, Guid locationId)
    {
        DepartmentId = departmentId;
        LocationId = locationId;
    }
    
    public Guid DepartmentId { get; }
    
    public Guid LocationId { get; }
}