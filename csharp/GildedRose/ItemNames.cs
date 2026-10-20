namespace GildedRoseKata;

/// <summary>
/// Tập trung các tên item đặc biệt vào một chỗ, tránh "magic string" rải rác trong code.
/// </summary>
public static class ItemNames
{
    public const string AgedBrie = "Aged Brie";
    public const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    public const string BackstagePass = "Backstage passes to a TAFKAL80ETC concert";

    /// <summary>Mọi item có tên bắt đầu bằng tiền tố này được coi là Conjured.</summary>
    public const string ConjuredPrefix = "Conjured";
}
