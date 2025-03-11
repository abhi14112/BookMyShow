using BookMyShow.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/paypal")]
public class CheckoutController : ControllerBase
{
    private readonly PayPalService _payPalService;

    public CheckoutController(PayPalService payPalService)
    {
        _payPalService = payPalService;
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
        return Ok(captureResponse);
    }
}
public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string ReturnUrl { get; set; } = "https://your-frontend.com/success";
    public string CancelUrl { get; set; } = "https://your-frontend.com/cancel";
}