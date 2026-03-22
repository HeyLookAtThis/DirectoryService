namespace DirectoryService.Domain.Departments;

public sealed class DepartmentLocation
{
    private List<Guid> _locationsIds = new List<Guid>();

    public DepartmentLocation(Guid departmentId) => DepartmentId = departmentId;

    public IReadOnlyList<Guid> LocationsIds => _locationsIds;

    public Guid DepartmentId { get; private set; }

    public void Add(Guid locationId) => _locationsIds.Add(locationId);

    public void Remove(Guid locationId) => _locationsIds.Remove(locationId);
}