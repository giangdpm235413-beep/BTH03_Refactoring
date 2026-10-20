namespace GildedRoseKata.ItemUpdaters;

/// <summary>
/// Backstage pass: tăng 1 khi còn trên 10 ngày, tăng 2 khi còn 10 ngày trở xuống,
/// tăng 3 khi còn 5 ngày trở xuống, và về 0 sau buổi diễn.
/// </summary>
public class BackstagePassUpdater : ItemUpdaterBase
{
    private const int DoubleIncreaseThreshold = 10;
    private const int TripleIncreaseThreshold = 5;

    protected override void UpdateQuality(Item item)
    {
        if (item.SellIn <= TripleIncreaseThreshold)
        {
            IncreaseQuality(item, 3);
        }
        else if (item.SellIn <= DoubleIncreaseThreshold)
        {
            IncreaseQuality(item, 2);
        }
        else
        {
            IncreaseQuality(item);
        }
    }

    protected override void UpdateQualityWhenExpired(Item item) => item.Quality = 0;
}
