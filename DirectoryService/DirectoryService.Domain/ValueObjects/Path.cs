namespace DirectoryService.Domain.ValueObjects;

public record Path
{
    private const char SEPARATOR = '.';
    
    // EF core
    private Path()
    {
    }

    public Path(Path? parentPath, string identifier)
    {
        if (parentPath == null)
            Value = identifier;
        else
            Value = parentPath.Value + SEPARATOR + identifier;
    }

    public string Value { get; }
    
    public char Separator => SEPARATOR;
}