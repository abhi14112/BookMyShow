using BookMyShow.Data;
using BookMyShow.Services;
using BookMyShow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/paypal")]
public class CheckoutController : ControllerBase
{
    private readonly PayPalService _payPalService;
    private readonly AppDbContext _context;
    public CheckoutController(PayPalService payPalService, AppDbContext context)
    {
        _payPalService = payPalService;
        _context = context;
    }
    [HttpGet("token")]
    public async Task<IActionResult> GetToken()
    {
        var token = await _payPalService.GetAccessToken();
        return Ok(new { access_token = token });
    }
    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] PaymentRequest request)
    {
        var order = await _payPalService.CreateOrder(request.Amount, request.Currency, request.ReturnUrl, request.CancelUrl);
        return Ok(order);
    }
    [HttpPost("capture-order/{orderId}")]
    public async Task<IActionResult> CaptureOrder(string orderId)
    {
        var captureResponse = await _payPalService.CaptureOrder(orderId);
        var booking = await _context.Bookings
            .Include(b => b.BookingSeats)
            .Where(b => b.Status == "InProgress" && b.PayPalOrderId == null).OrderByDescending(b => b.BookingTime).FirstOrDefaultAsync();

        if (booking != null)
        {
            booking.Status = "Confirmed";
            booking.PayPalOrderId = orderId;
            var seatIds = booking.BookingSeats.Select(bs => bs.SeatId).ToList();
            var seats = _context.Seats.Where(s => seatIds.Contains(s.Id));
            foreach (var seat in seats)
            {
                seat.SeatStatus = SeatStatus.BOOKED;
            }
            await _context.SaveChangesAsync();
        }
        return Ok(captureResponse);
    }
    [HttpPost("cancel-order/{bookingId}")]
    public async Task<IActionResult> CancelOrder(int bookingId)
    {

        var booking = await _context.Bookings
            .Include(b => b.BookingSeats)
            .FirstOrDefaultAsync(b => b.Id == bookingId && b.Status == "InProgress");
        if (booking != null) 
        {
            var seatIds = booking.BookingSeats.Select(bs => bs.SeatId).ToList();

            var seats = _context.Seats.Where(s => seatIds.Contains(s.Id));
            foreach(var seat in seats)
            {
                seat.SeatStatus = SeatStatus.AVAILABLE;
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            
        }
        return Ok(new { message = "Booking canceled" });
    }
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string ReturnUrl { get; set; } = "https://your-frontend.com/success";
        public string CancelUrl { get; set; } = "https://your-frontend.com/cancel";
    }
}