using System.ComponentModel;

namespace ShowTrack.Shared.Models;

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