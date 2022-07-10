using ShowTrack.Shared.Models;

public class ShowBase
{    
    public ShowType Type { get; set; }

    public string Title { get; set; } = null!;

    public string[]? Cast { get; set; }

    public int ReleaseYear { get; set; }

    public string[]? Directors { get; set; }

    public string? DateAdded { get; set; }

    public string Duration { get; set; } = null!;

    public string[] Categories { get; set; } = null!;

    public string Rating { get; set; } = null!;

    public string Description { get; set; } = null!;    

    public string? Country { get; set; }
    
    public SubscriptionType SubscriptionType { get; set; }
}