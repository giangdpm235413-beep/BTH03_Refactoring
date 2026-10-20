# HƯỚNG DẪN SỬ DỤNG

## 1. Yêu cầu
- Windows với **Visual Studio 2022** (bản 17.8 trở lên, có workload ".NET desktop development") hoặc **.NET 8 SDK**.
- Git (nếu muốn clone/commit).

## 2. Mở và build
**Cách 1 – Visual Studio:** mở `csharp/GildedRose.sln` → menu **Build → Build Solution** (`Ctrl+Shift+B`).

**Cách 2 – Dòng lệnh:**
```
cd csharp
dotnet build
```

## 3. Chạy test
**Visual Studio:** menu **Test → Run All Tests** (`Ctrl+R, A`), xem kết quả ở **Test Explorer**.

**Dòng lệnh:**
```
cd csharp
dotnet test
```
Lần đầu cần Internet để tải các gói NuGet (xunit, Microsoft.NET.Test.Sdk).

## 4. Cấu trúc code
| Đường dẫn | Nội dung |
|---|---|
| `GildedRose/Item.cs` | Lớp dữ liệu (không sửa) |
| `GildedRose/ItemNames.cs` | Hằng số tên item |
| `GildedRose/GildedRose.cs` | `UpdateQuality()` |
| `GildedRose/ItemUpdaters/` | Interface, lớp cơ sở, 5 updater, Factory |
| `GildedRoseTests/LegacyGildedRose.cs` | Code gốc để so sánh |
| `GildedRoseTests/GildedRoseTest.cs` | Toàn bộ test |

## 5. Ví dụ sử dụng
```csharp
var items = new List<Item>
{
    new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
    new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
};

var app = new GildedRose(items);
app.UpdateQuality();   // qua một ngày
```

## 6. Cách thêm một loại item mới
Ví dụ thêm item "Frozen" giảm chất lượng 3 mỗi ngày:

1. Tạo `ItemUpdaters/FrozenItemUpdater.cs`:
```csharp
namespace GildedRoseKata.ItemUpdaters;

public class FrozenItemUpdater : ItemUpdaterBase
{
    protected override void UpdateQuality(Item item) => DecreaseQuality(item, 3);
    protected override void UpdateQualityWhenExpired(Item item) => DecreaseQuality(item, 3);
}
```
2. Thêm hằng số tên vào `ItemNames.cs` (nếu cần).
3. Thêm một dòng vào `ItemUpdaterFactory.Create(...)`.
4. Viết test trong `GildedRoseTest.cs`.

Không cần sửa `GildedRose.cs` hay các updater khác.

## 7. Đăng bài lên GitHub của bạn
1. Vào github.com, đăng nhập **tài khoản của bạn**, bấm **New repository**, đặt tên `BTH03_Refactoring`. **Không** tick thêm README, .gitignore hay license. Bấm **Create repository** rồi copy đường dẫn dạng `https://github.com/TEN_CUA_BAN/BTH03_Refactoring.git`.
2. Mở PowerShell trong thư mục `BTH03_Refactoring` (hoặc **View → Terminal** trong Visual Studio) và chạy:
```
git config --global user.name "Ho Ten"
git config --global user.email "email@cua-ban.com"
.\dang_len_git.ps1 -RepoUrl "https://github.com/TEN_CUA_BAN/BTH03_Refactoring.git"
```
(hai lệnh `git config` chỉ cần chạy một lần trên máy). Nếu PowerShell chặn script, chạy thêm: `Set-ExecutionPolicy -Scope Process Bypass`.
3. Lần đầu push, cửa sổ đăng nhập GitHub sẽ hiện ra: đăng nhập đúng tài khoản chủ repo.

Tệp `.gitignore` đã loại bỏ thư mục `bin/`, `obj/`, `.vs/`.
