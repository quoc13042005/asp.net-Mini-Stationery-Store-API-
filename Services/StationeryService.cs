using StationeryStore.Api.Models;

namespace StationeryStore.Api.Services;

public class StationeryService
{
    private readonly List<StationeryItem> _items =
    [
        new StationeryItem { Id = 1, Name = "Bút bi Thiên Long", Category = "Bút", Price = 5000, Stock = 150 },
        new StationeryItem { Id = 2, Name = "Vở kẻ ngang Campus", Category = "Vở", Price = 15000, Stock = 50 },
        new StationeryItem { Id = 3, Name = "Giấy A4 Double A", Category = "Giấy", Price = 80000, Stock = 0 },
        new StationeryItem { Id = 4, Name = "Kẹp bướm", Category = "Phụ kiện", Price = 2000, Stock = 10 }
    ];

    public List<StationeryItem> GetAll() => _items;

    public object GetStats()
    {
        return new
        {
            TotalProducts = _items.Count,
            TotalStockValue = _items.Sum(x => x.Price * x.Stock),
            OutOfStockCount = _items.Count(x => x.Stock == 0)
        };
    }

    public string CheckStockLevel(int stock)
    {
        if (stock <= 0) return "Hết hàng";
        if (stock <= 20) return "Sắp hết";
        return "Còn nhiều";
    }
}