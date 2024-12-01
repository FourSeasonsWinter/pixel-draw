
public interface Tool
{
    public abstract string Name { get; }
    public abstract void Use(Pixel pixel);
}

public enum Tools
{
    Brush, Eraser, Selector
}