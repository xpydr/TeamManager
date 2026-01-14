namespace TeamManager.Data.Enums;

public enum UpdateResult
{
    Success,
    NotFound,
    Invalid,
    ConcurrencyConflict,
    DatabaseError
}