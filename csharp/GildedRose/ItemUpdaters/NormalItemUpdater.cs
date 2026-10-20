namespace GildedRoseKata.ItemUpdaters;

/// <summary>Item thường: giảm 1/ngày, quá hạn thì giảm thêm 1 (tổng 2).</summary>
public class NormalItemUpdater : ItemUpdaterBase
{
    protected override void UpdateQuality(Item item) => DecreaseQuality(item);

    protected override void UpdateQualityWhenExpired(Item item) => DecreaseQuality(item);
}
