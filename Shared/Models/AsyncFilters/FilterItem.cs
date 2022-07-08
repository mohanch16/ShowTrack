using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTrack.Shared.Models.AsyncFilters;

public class FilterItem
{
    public string Name { get; set; } = null!;

    public int Value { get; set; }

    // Note: this is important so the select can compare filteritems
    public override bool Equals(object o = null!) {
        var other = o as FilterItem;
        return other?.Value==Value;
    }

    // Note: this is important so the select can compare pizzas
    public override int GetHashCode() => Name.GetHashCode();
}