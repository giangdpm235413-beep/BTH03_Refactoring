using GildedRoseKata.ItemUpdaters;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;

    public GildedRose(IList<Item> items)
    {
        _items = items;
    }

    public IList<Item> Items => _items;

    /// <summary>
    /// Cập nhật cuối ngày cho toàn bộ item.
    /// Mỗi loại item có một "updater" riêng; lớp này không cần biết chi tiết luật từng loại.
    /// </summary>
    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            ItemUpdaterFactory.Create(item).Update(item);
        }
    }
}
