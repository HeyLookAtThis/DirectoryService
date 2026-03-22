namespace DirectoryService.Domain.ValueObjects;

public record Depth
{
    private const short DEFAULT_VALUE = 1;

    public Depth(Path path)
    {
        Value = DEFAULT_VALUE;

        foreach (char symbol in path.Value)
            if (symbol == path.Separator)
                Value++;
    }

    public short Value { get; }
}