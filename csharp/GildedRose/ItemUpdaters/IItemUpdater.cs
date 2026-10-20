namespace GildedRoseKata.ItemUpdaters;

/// <summary>
/// Hợp đồng chung: mỗi loại item biết cách tự cập nhật SellIn và Quality của mình sau một ngày.
/// </summary>
public interface IItemUpdater
{
    void Update(Item item);
}
