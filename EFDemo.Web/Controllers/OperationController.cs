using EFDemo.Web.Models;
using MethodTimer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationController : ControllerBase
{
    private readonly ILogger<OperationController> _logger;
    private readonly EfDemoContext _context;

    public OperationController(ILogger<OperationController> logger, EfDemoContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("read"), Time]
    public IActionResult Get()
    {
        // understand LINQ!!! IQueryable -> IEnumerable (complex logic can be)
        // use AsNoTracking for read-only
        // check memory and use key merge
        // use async to improve scalabillity 
        // use select (with includes)
        // use paging and filtering
        // use precompile
        // use single -> first -> find
        // properly type for related collections in a model

        // in-memory LINQ
        // IEnumerable<Track> items = _context.Tracks.Where(x => x.Name.Length >= 15);
        // var count = items.AsQueryable().Count(); // fix in-memory

        // open connection
        var items = _context.Tracks.AsEnumerable()
            .Where(x => x.Name.Contains("rock", StringComparison.OrdinalIgnoreCase));
        // foreach (var item in items)
        // {
        //     if (item.Bytes > 9999)
        //     {
        //         Console.WriteLine("Done");
        //         break;
        //     }
        // }

        // var count = items.AsQueryable().Count();

        // var tracks = _context.Tracks.Take(100).AsNoTrackingWithIdentityResolution()
        //     .Include(t => t.Album).ToArray();
        //
        // YAGNI
        // var tracks = _context.Tracks.Take(100).AsNoTrackingWithIdentityResolution()
        //     .Include(t => t.Album).Select(t => new {t.TrackId, t.Name, t.Album.Title}).ToArray();
        
        // split query to small
        // var tracks = _context.Tracks.AsNoTracking()
        //     .AsSingleQuery()
        //     .Include(t => t.Playlists)
        //     .Include(t => t.Album)
        //     .ToArray();

        // EF.CompileQuery()
        
        // var track = _context.Tracks.Find(3);
        // track = _context.Tracks.FirstOrDefault(t => t.TrackId == 3);
        // track = _context.Tracks.SingleOrDefault(t => t.TrackId == 3);
        
        return Ok();
    }
}