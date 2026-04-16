using Microsoft.AspNetCore.Mvc;
using StationeryStore.Api.Services;

namespace StationeryStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StationeryController : ControllerBase
{
    private readonly StationeryService _service;

    // Tiêm Service vào Controller
    public StationeryController(StationeryService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll().Select(item => new
        {
            item.Id,
            item.Name,
            item.Category,
            item.Price,
            item.Stock,
            Status = _service.CheckStockLevel(item.Stock) // Tính toán trạng thái tồn kho
        });

        return Ok(result);
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        return Ok(_service.GetStats());
    }
}