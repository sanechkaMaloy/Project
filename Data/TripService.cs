using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Data; // Простір імен для ApplicationDbContext
using BlazorApp1; // Простір імен для моделі Trip

public class TripService
{
    private readonly ApplicationDbContext _context;

    public TripService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Trip>> GetAllTripsAsync(int userId)
    {
        return await _context.Trips
       .Where(t => t.UserId == userId)
       .ToListAsync();
    }
}
