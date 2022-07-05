using System.ComponentModel;

namespace ShowTrack.Shared.Models;

public class Show
{    
    public string? Id { get; set; }

    public ShowType Type { get; set; }

    public string Title { get; set; } = null!;

    public string[]? Cast { get; set; }

    public int ReleaseYear { get; set; }

    public string[]? Directors { get; set; }

    public DateTime DateAdded { get; set; }

    public string Duration { get; set; } = null!;

    public string[] Categories { get; set; } = null!;

    public string Rating { get; set; } = null!;

    public string Description { get; set; } = null!;    

    public SubscriptionType SubscriptionType { get; set; }
}

public enum ShowType 
{
    None = 0,
    Movie = 1,
    [Description("TV Show")]
    TVShow = 2
}

public enum SubscriptionType
{
    None = 0,
    Netflix = 1,
    [Description("Amazon Prime Video")]
    PrimeVideo = 2,
    [Description("Disney Plus")]
    DisneyPlus = 3,
}