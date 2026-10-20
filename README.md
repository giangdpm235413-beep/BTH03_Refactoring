# BTH03_Refactoring

Bài thực hành 03: **Refactoring** – bài toán *Gilded Rose* (C#, .NET 8, xUnit).

## Mục tiêu
- Refactor hàm `UpdateQuality()` nhiều `if` lồng nhau thành thiết kế hướng đối tượng dễ đọc, dễ mở rộng.
- Thêm tính năng mới **Conjured** mà không phải sửa code cũ.
- Đảm bảo hành vi cũ không bị thay đổi bằng test.

## Cấu trúc
```
csharp/
  GildedRose.sln
  GildedRose/            # code chính
    Item.cs              # giữ nguyên theo đề bài
    ItemNames.cs         # hằng số tên item
    GildedRose.cs        # UpdateQuality() gọn
    ItemUpdaters/        # mỗi loại item một lớp
  GildedRoseTests/       # test xUnit + bản code cũ để so sánh
BAO_CAO.md               # báo cáo
HUONG_DAN.md             # hướng dẫn build/chạy/mở rộng
```

## Chạy nhanh
```
cd csharp
dotnet test
```
Hoặc mở `csharp/GildedRose.sln` bằng Visual Studio rồi chọn **Test → Run All Tests**.

Chi tiết xem `HUONG_DAN.md` và `BAO_CAO.md`.
