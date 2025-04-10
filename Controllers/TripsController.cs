using BlazorApp1.Data;
using BlazorApp1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/trips")]
public class TripsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TripsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("upload/{id}")]
    public async Task<IActionResult> UploadTripPhoto(int id, [FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Файл не було завантажено");
        }

        var trip = await _context.Trips.FindAsync(id);
        if (trip == null)
        {
            return NotFound("Поїздку не знайдено");
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        trip.Image = memoryStream.ToArray();

        await _context.SaveChangesAsync();

        return Ok(new { trip.Id });
    }


    [HttpGet("trip-image/{id}")]
    public async Task<IActionResult> GetTripImage(int id)
    {
        var trip = await _context.Trips.FindAsync(id);

        if (trip == null || trip.Image == null || trip.Image.Length == 0)
        {
            return NotFound();
        }

        string mimeType = "image/jpeg";
        if (trip.Image.Length > 4 && trip.Image[0] == 0x89 && trip.Image[1] == 0x50)
        {
            mimeType = "image/png";
        }

        return File(trip.Image, mimeType);
    }



    [HttpPost]
    public async Task<IActionResult> AddTrip([FromBody] Trip trip)
    {
        if (trip == null)
        {
            return BadRequest("Invalid trip data");
        }



        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Trip added successfully!" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        var trip = await _context.Trips.FindAsync(id);
        if (trip == null)
        {
            return NotFound("Поїздку не знайдено");
        }

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Поїздку успішно видалено" });
    }
}
