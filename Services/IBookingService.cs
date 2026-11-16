using BookingApi.Dtos;
using BookingApi.Models;

namespace BookingApi.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllAsync();
        Task<List<Booking>> GetByRoomAsync(int roomId);
        Task<(Booking? booking, string? error)> CreateAsync(CreateBookingDto dto);
    }
}
