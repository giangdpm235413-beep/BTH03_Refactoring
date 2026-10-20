namespace GildedRoseKata.ItemUpdaters;

/// <summary>Sulfuras: vật phẩm huyền thoại, không bao giờ hết hạn và không đổi chất lượng.</summary>
public class SulfurasUpdater : ItemUpdaterBase
{
    public override void Update(Item item)
    {
        // Không làm gì: SellIn và Quality đều giữ nguyên.
    }

    protected override void UpdateQuality(Item item) { }

    protected override void UpdateQualityWhenExpired(Item item) { }
}
