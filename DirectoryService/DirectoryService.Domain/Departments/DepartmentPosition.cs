namespace DirectoryService.Domain.Departments;

public sealed class DepartmentPosition
{
    private List<Guid> _positionsIds = new List<Guid>();

    public DepartmentPosition(Guid departmentId) => DepartmentId = departmentId;

    public IReadOnlyList<Guid> PositionsIds => _positionsIds;

    public Guid DepartmentId { get; private set; }

    public void Add(Guid locationId) => _positionsIds.Add(locationId);

    public void Remove(Guid locationId) => _positionsIds.Remove(locationId);
}