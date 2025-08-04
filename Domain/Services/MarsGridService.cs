namespace Domain.Services;

/// <summary>
/// Service to manage the Mars grid boundaries and scents.
/// It can be baked by a database or any other storage.
/// </summary>
/// <param name="maxX">Max x</param>
/// <param name="maxY">Nax y</param>
public class MarsGridService(int maxX, int maxY)
{
    private readonly int _maxX = maxX;
    private readonly int _maxY = maxY;
    private readonly HashSet<(int, int)> _scents = [];

    public bool IsScented(int x, int y) => _scents.Contains((x, y));
    public void AddScent(int x, int y) => _scents.Add((x, y));
    public bool IsOutofBounds(int x, int y) => x < 0 || x > _maxX || y < 0 || y > _maxY;
}
