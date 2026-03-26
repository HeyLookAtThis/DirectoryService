namespace DirectoryService.Domain;

public record DepartmentPosition
{
    public DepartmentPosition(Guid departmentId, Guid positionId)
    {
        DepartmentId = departmentId;
        PositionId = positionId;
    }
    
    public Guid DepartmentId { get; }
    
    public Guid PositionId { get; }
}