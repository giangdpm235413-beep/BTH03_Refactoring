namespace GildedRoseKata.ItemUpdaters;

/// <summary>
/// Lớp cơ sở (Template Method): quy định thứ tự cập nhật một ngày
///   1) đổi Quality theo SellIn hiện tại,
///   2) giảm SellIn 1 ngày,
///   3) nếu đã quá hạn (SellIn &lt; 0) thì áp dụng thêm luật "quá hạn".
/// Các lớp con chỉ cần cài đặt bước 1 và 3.
/// </summary>
public abstract class ItemUpdaterBase : IItemUpdater
{
    protected const int MinQuality = 0;
    protected const int MaxQuality = 50;

    public virtual void Update(Item item)
    {
        UpdateQuality(item);
        item.SellIn--;

        if (item.SellIn < 0)
        {
            UpdateQualityWhenExpired(item);
        }
    }

    /// <summary>Đổi Quality trong ngày (SellIn chưa bị giảm).</summary>
    protected abstract void UpdateQuality(Item item);

    /// <summary>Đổi Quality thêm khi item đã quá hạn bán.</summary>
    protected abstract void UpdateQualityWhenExpired(Item item);

    protected static void IncreaseQuality(Item item, int amount = 1)
    {
        item.Quality = Math.Min(MaxQuality, item.Quality + amount);
    }

    protected static void DecreaseQuality(Item item, int amount = 1)
    {
        item.Quality = Math.Max(MinQuality, item.Quality - amount);
    }
}
