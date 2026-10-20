namespace GildedRoseKata.ItemUpdaters;

/// <summary>Aged Brie: càng cũ càng tăng chất lượng (+1/ngày, quá hạn +2), tối đa 50.</summary>
public class AgedBrieUpdater : ItemUpdaterBase
{
    protected override void UpdateQuality(Item item) => IncreaseQuality(item);

    protected override void UpdateQualityWhenExpired(Item item) => IncreaseQuality(item);
}
