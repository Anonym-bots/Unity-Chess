

public static class ChessUtils
{

    public static readonly BoardPosition[] KnightOffsets = {
            new(-2, -1),
            new(-2, 1),
            new(2, -1),
            new(2, 1),
            new(-1, -2),
            new(-1, 2),
            new(1, -2),
            new(1, 2),
    };

    public static readonly BoardPosition[] SurroundingOffsets = {
            new(-1, 0),
            new(1, 0),
            new(0, -1),
            new(0, 1),
            new(-1, 1),
            new(-1, -1),
            new(1, -1),
            new(1, 1),
    };

    public static readonly BoardPosition[] DiagonalOffsets = {
            new(-1, 1),
            new(-1, -1),
            new(1, -1),
            new(1, 1)
    };

    public static readonly BoardPosition[] CardinalOffsets = {
            new(-1, 0),
            new(1, 0),
            new(0, -1),
            new(0, 1),
    };

    public static readonly BoardPosition[] WhitePawnAttackOffsets = {
            new(1, 1),
            new(1, -1)
    };

    public static readonly BoardPosition[] BlackPawnAttackOffsets = {
            new(-1, 1),
            new(-1, -1)
    };
}
