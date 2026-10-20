namespace GildedRoseKata.ItemUpdaters;

/// <summary>
/// Factory: chọn updater phù hợp theo tên item.
/// Muốn thêm loại item mới, chỉ cần viết thêm một updater và thêm một dòng ở đây
/// (nguyên tắc Open/Closed: mở rộng mà không phải sửa code cũ).
/// </summary>
public static class ItemUpdaterFactory
{
    // Các updater không giữ trạng thái nên dùng chung một instance cho nhẹ.
    private static readonly IItemUpdater Normal = new NormalItemUpdater();
    private static readonly IItemUpdater AgedBrie = new AgedBrieUpdater();
    private static readonly IItemUpdater Sulfuras = new SulfurasUpdater();
    private static readonly IItemUpdater BackstagePass = new BackstagePassUpdater();
    private static readonly IItemUpdater Conjured = new ConjuredItemUpdater();

    public static IItemUpdater Create(Item item)
    {
        return item.Name switch
        {
            ItemNames.AgedBrie => AgedBrie,
            ItemNames.Sulfuras => Sulfuras,
            ItemNames.BackstagePass => BackstagePass,
            var name when name != null
                          && name.StartsWith(ItemNames.ConjuredPrefix, StringComparison.Ordinal) => Conjured,
            _ => Normal
        };
    }
}
