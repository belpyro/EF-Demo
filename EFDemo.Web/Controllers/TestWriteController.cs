using EFDemo.Web.Models;
using MethodTimer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Web.Controllers;

[ApiController, Route("/api/test/write")]
public class TestWriteController : ControllerBase
{
    private readonly EfDemoContext _context;

    public TestWriteController(EfDemoContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet("{step}")]
    public IActionResult Test(int step)
    {
        switch (step)
        {
            case 1: //shadow properties
                return TestShadowProps();
            case 2: //query filters
                return TestQueryFilters();
            case 3: //change tracker default
                return TestDefaultChangeTracker();
            case 4: //change tracker UpdateGraph
                return TestUpdateGraph();
            case 5: //change tracker strategy
                return TestUpdateStrategy();
            default:
                throw new ArgumentException("step doesn't exist");
        }

        return Ok();
    }

    private IActionResult TestShadowProps()
    {
        var result = _context.Tracks.Take(10)
            .OrderBy(t => EF.Property<DateTime>(t, "UpdatedOn"))
            .ToArray();
        return Ok(result);
    }

    private IActionResult TestQueryFilters()
    {
        var result = _context.Tracks.IgnoreQueryFilters().Count();
        // var strategy = _context.Database.CreateExecutionStrategy();
        // strategy.Execute(() =>
        // {
        //     
        // })
        return Ok(result);
    }

    [Time]
    private IActionResult TestDefaultChangeTracker()
    {
        var items = _context.Tracks.Include(t => t.Album).Take(1000).ToList();
        items[0].Name = "Best track";
        var entry = _context.Entry(items[0]);
        entry.State = EntityState.Deleted;
        // entry.Property(t => t.Name).IsModified = true;
        // items.ForEach(t => { _context.Entry(t).Property<bool>("IsDeleted").CurrentValue = true; });
        _context.SaveChanges();
        return Ok();
    }

    private IActionResult TestUpdateGraph()
    {
        var track = _context.Tracks.Include(t => t.Album)
            .AsNoTracking()
            .First(t => t.Album != null);
        track.Name = "Demo Track";
        _context.ChangeTracker.TrackGraph(track, n =>
        {
            n.Entry.State = EntityState.Unchanged;
            if (n.Entry.Entity is Track)
            {
                n.Entry.Property("Name").IsModified = true;
            }
        });
        _context.SaveChanges();

        // track.Name = "Hello";
        // _context.SaveChanges();
        return Ok();
    }

    private IActionResult TestUpdateStrategy()
    {
        var artist = _context.Artists.First();
        artist.ArtistId = default;
        _context.SaveChanges();
        
        artist.Name = "Super Composer";
        _context.SaveChanges();
        return Ok();
    }
}