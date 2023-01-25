using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFDemo.Web.Models;

public partial class Genre
{
    private readonly ILazyLoader _loader;

    public Genre(ILazyLoader loader)
    {
        _loader = loader;
    }
    
    public int GenreId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; } = new List<Track>();
}
