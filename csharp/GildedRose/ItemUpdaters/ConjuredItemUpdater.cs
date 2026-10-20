namespace GildedRoseKata.ItemUpdaters;

/// <summary>Conjured (tính năng mới): xuống cấp nhanh gấp đôi item thường (-2/ngày, quá hạn -4).</summary>
public class ConjuredItemUpdater : ItemUpdaterBase
{
    private const int DegradeRate = 2;

    protected override void UpdateQuality(Item item) => DecreaseQuality(item, DegradeRate);

    protected override void UpdateQualityWhenExpired(Item item) => DecreaseQuality(item, DegradeRate);
}
