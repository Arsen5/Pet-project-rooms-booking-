using BookingApi.Data;
using BookingApi.Dtos;
using BookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _db;

        public BookingService(AppDbContext db) { _db = db; }

        public async Task<(Booking? booking, string? error)> CreateAsync(CreateBookingDto dto)
        {
            if (dto.EndTime <= dto.StartTime)
                return (null, "Время окончания должно быть позже начала");
            var roomExist = await _db.Rooms.AnyAsync(r => r.Id ==  dto.RoomId);
            if (!roomExist) return (null, $"Переговорка с id={dto.RoomId} не найдена");

            var hasOverlap = await _db.Bookings.AnyAsync(b =>
                b.Id == dto.RoomId &&
                dto.StartTime < b.EndTime &&
                dto.EndTime > b.StartTime);
            if (hasOverlap) return (null, "В это время уже занято");
            var booking = new Booking
            {
                Id = dto.RoomId,
                UserName = dto.UserName,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
            };
            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();
            return (booking, null);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _db.Bookings.AsNoTracking().ToListAsync();
        }

        public async Task<List<Booking>> GetByRoomAsync(int roomId)
        {
            return await _db.Bookings
                .AsNoTracking()
                .Where(b => b.RoomId ==  roomId)
                .ToListAsync();
        }
    }
}
