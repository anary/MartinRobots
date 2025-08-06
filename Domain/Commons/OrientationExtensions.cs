namespace Domain.Commons;
public static class OrientationExtensions
{
    private static readonly Dictionary<Orientation, (int dX, int dY)> _moveVectors = new()
    {
            { Orientation.N, (0, 1) }, { Orientation.E, (1, 0) },
            { Orientation.S, (0, -1) }, { Orientation.W, (-1, 0) }
    };

    public static (int dX, int dY) ToVector(this Orientation orientation) => _moveVectors[orientation];
}
