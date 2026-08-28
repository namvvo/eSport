namespace eSport.UI.Client.Dto;

public interface IPlayerRef
{
    string Name { get; }
    string? Slug { get; }
}

public interface IStatItem
{
    IPlayerRef? Player { get; }
    string? Info { get; }
}