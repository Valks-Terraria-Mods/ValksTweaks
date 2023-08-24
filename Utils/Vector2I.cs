namespace ValksTweaks;

public struct Vector2I
{
    public static Vector2I Zero => new(0, 0);

    public int X { get; set; }
    public int Y { get; set; }

    public Vector2I(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool operator== (Vector2I a, Vector2I b) =>
        a.X == b.X && a.Y == b.Y;

    public static bool operator!= (Vector2I a, Vector2I b) =>
        a.X != b.X && a.Y != b.Y;

    public override bool Equals(object other)
    {
        if (other is not Vector2I)
            return false;

        return Equals((Vector2I)other);
    }

    public bool Equals(Vector2I other) => X == other.X && Y == other.Y;

    // See https://github.com/Unity-Technologies/UnityCsReference/blob/62633e3912ab891be4c6f5ef4500e69f59d85ed4/Runtime/Export/Math/Vector2Int.cs#L238
    public override int GetHashCode() => 
        X.GetHashCode() ^ (Y.GetHashCode() << 2);
}
