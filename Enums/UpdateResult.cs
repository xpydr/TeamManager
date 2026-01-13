namespace TeamManager.Enums;

public enum UpdateResult
{
    Success,
    NotFound,
    Invalid,
    ConcurrencyConflict,
    DatabaseError
}