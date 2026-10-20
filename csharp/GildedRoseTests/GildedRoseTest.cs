using GildedRoseKata;
using GildedRoseKata.ItemUpdaters;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseTest
{
    private const string Normal = "+5 Dexterity Vest";

    // ---------- Hàm hỗ trợ ----------

    private static Item Run(string name, int sellIn, int quality, int days = 1)
    {
        var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var app = new GildedRose(new List<Item> { item });
        for (var d = 0; d < days; d++)
        {
            app.UpdateQuality();
        }
        return item;
    }

    // ---------- Item thường ----------

    [Fact]
    public void Normal_BeforeSellBy_DecreasesQualityBy1AndSellInBy1()
    {
        var item = Run(Normal, 10, 20);
        Assert.Equal(9, item.SellIn);
        Assert.Equal(19, item.Quality);
    }

    [Fact]
    public void Normal_AfterSellBy_DecreasesQualityBy2()
    {
        var item = Run(Normal, 0, 20);
        Assert.Equal(-1, item.SellIn);
        Assert.Equal(18, item.Quality);
    }

    [Fact]
    public void Normal_QualityIsNeverNegative()
    {
        var item = Run(Normal, 5, 0, days: 10);
        Assert.Equal(0, item.Quality);
    }

    // ---------- Aged Brie ----------

    [Fact]
    public void AgedBrie_IncreasesQualityBy1BeforeSellBy()
    {
        var item = Run(ItemNames.AgedBrie, 5, 10);
        Assert.Equal(11, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreasesQualityBy2AfterSellBy()
    {
        var item = Run(ItemNames.AgedBrie, 0, 10);
        Assert.Equal(12, item.Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceeds50()
    {
        var item = Run(ItemNames.AgedBrie, 5, 50, days: 5);
        Assert.Equal(50, item.Quality);
    }

    // ---------- Sulfuras ----------

    [Theory]
    [InlineData(5)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Sulfuras_NeverChanges(int sellIn)
    {
        var item = Run(ItemNames.Sulfuras, sellIn, 80, days: 5);
        Assert.Equal(sellIn, item.SellIn);
        Assert.Equal(80, item.Quality);
    }

    // ---------- Backstage pass ----------

    [Theory]
    [InlineData(15, 20, 21)] // trên 10 ngày: +1
    [InlineData(11, 20, 21)]
    [InlineData(10, 20, 22)] // 10 ngày trở xuống: +2
    [InlineData(6, 20, 22)]
    [InlineData(5, 20, 23)]  // 5 ngày trở xuống: +3
    [InlineData(1, 20, 23)]
    [InlineData(0, 20, 0)]   // sau buổi diễn: về 0
    [InlineData(-1, 20, 0)]
    public void BackstagePass_QualityRules(int sellIn, int quality, int expectedQuality)
    {
        var item = Run(ItemNames.BackstagePass, sellIn, quality);
        Assert.Equal(expectedQuality, item.Quality);
    }

    [Fact]
    public void BackstagePass_QualityNeverExceeds50()
    {
        var item = Run(ItemNames.BackstagePass, 3, 49);
        Assert.Equal(50, item.Quality);
    }

    // ---------- Conjured (tính năng mới) ----------

    [Fact]
    public void Conjured_DegradesTwiceAsFastBeforeSellBy()
    {
        var item = Run("Conjured Mana Cake", 3, 6);
        Assert.Equal(2, item.SellIn);
        Assert.Equal(4, item.Quality);
    }

    [Fact]
    public void Conjured_DegradesTwiceAsFastAfterSellBy()
    {
        var item = Run("Conjured Mana Cake", 0, 10);
        Assert.Equal(6, item.Quality);
    }

    [Fact]
    public void Conjured_QualityIsNeverNegative()
    {
        var item = Run("Conjured Mana Cake", 3, 1, days: 5);
        Assert.Equal(0, item.Quality);
    }

    // ---------- Factory ----------

    [Theory]
    [InlineData(Normal, typeof(NormalItemUpdater))]
    [InlineData(ItemNames.AgedBrie, typeof(AgedBrieUpdater))]
    [InlineData(ItemNames.Sulfuras, typeof(SulfurasUpdater))]
    [InlineData(ItemNames.BackstagePass, typeof(BackstagePassUpdater))]
    [InlineData("Conjured Mana Cake", typeof(ConjuredItemUpdater))]
    public void Factory_ReturnsCorrectUpdater(string name, Type expected)
    {
        var updater = ItemUpdaterFactory.Create(new Item { Name = name });
        Assert.IsType(expected, updater);
    }

    // ---------- Test đặc tả: code mới phải cho kết quả y hệt code cũ ----------

    public static IEnumerable<object[]> LegacyNames()
    {
        yield return new object[] { Normal };
        yield return new object[] { ItemNames.AgedBrie };
        yield return new object[] { ItemNames.Sulfuras };
        yield return new object[] { ItemNames.BackstagePass };
    }

    [Theory]
    [MemberData(nameof(LegacyNames))]
    public void NewCode_MatchesLegacyCode_ForOneDay(string name)
    {
        var qualities = name == ItemNames.Sulfuras
            ? new[] { 80 }
            : Enumerable.Range(0, 51).ToArray();

        foreach (var sellIn in Enumerable.Range(-3, 20))   // -3 .. 16
        {
            foreach (var quality in qualities)
            {
                AssertSameAsLegacy(name, sellIn, quality, days: 1);
            }
        }
    }

    [Theory]
    [MemberData(nameof(LegacyNames))]
    public void NewCode_MatchesLegacyCode_Over60Days(string name)
    {
        var qualities = name == ItemNames.Sulfuras
            ? new[] { 80 }
            : new[] { 0, 1, 10, 25, 49, 50 };

        foreach (var sellIn in new[] { -2, 0, 1, 5, 6, 10, 11, 20 })
        {
            foreach (var quality in qualities)
            {
                AssertSameAsLegacy(name, sellIn, quality, days: 60);
            }
        }
    }

    private static void AssertSameAsLegacy(string name, int sellIn, int quality, int days)
    {
        var legacyItem = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var newItem = new Item { Name = name, SellIn = sellIn, Quality = quality };

        var legacy = new LegacyGildedRose(new List<Item> { legacyItem });
        var current = new GildedRose(new List<Item> { newItem });

        for (var d = 1; d <= days; d++)
        {
            legacy.UpdateQuality();
            current.UpdateQuality();

            Assert.True(
                legacyItem.SellIn == newItem.SellIn && legacyItem.Quality == newItem.Quality,
                $"Khác nhau: {name}, SellIn đầu={sellIn}, Quality đầu={quality}, sau {d} ngày " +
                $"=> cũ({legacyItem.SellIn},{legacyItem.Quality}) mới({newItem.SellIn},{newItem.Quality})");
        }
    }
}
