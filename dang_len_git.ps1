# Script đưa bài lên GitHub CÁ NHÂN của bạn.
# Cách dùng (mở PowerShell trong thư mục này):
#   .\dang_len_git.ps1 -RepoUrl "https://github.com/TEN_CUA_BAN/BTH03_Refactoring.git"
# Trước đó: tạo repo TRỐNG trên github.com bằng chính tài khoản của bạn
# (không tick README / .gitignore / license).

param(
    [Parameter(Mandatory = $true)]
    [string]$RepoUrl,
    [string]$Message = "BTH03 Refactoring: Gilded Rose (Strategy + Factory), them Conjured"
)

Set-Location $PSScriptRoot

# 1. Kiểm tra Git
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Host "Chua cai Git. Tai tai https://git-scm.com roi chay lai." -ForegroundColor Red
    exit 1
}

# 2. Kiểm tra đã khai báo tên/email chưa
if (-not (git config user.name) -or -not (git config user.email)) {
    Write-Host "Chua khai bao Git. Hay chay 2 lenh sau (thay bang thong tin cua ban) roi chay lai:" -ForegroundColor Yellow
    Write-Host '  git config --global user.name "Ho Ten"'
    Write-Host '  git config --global user.email "email@cua-ban.com"'
    exit 1
}

# 3. Khởi tạo repo nếu chưa có
if (-not (Test-Path ".git")) {
    git init -b main
}

# 4. Commit
git add .
git diff --cached --quiet
if ($LASTEXITCODE -ne 0) {
    git commit -m $Message
} else {
    Write-Host "Khong co thay doi moi de commit."
}

# 5. Gắn remote (ghi đè nếu đã có origin)
$remotes = git remote
if ($remotes -contains "origin") {
    git remote set-url origin $RepoUrl
} else {
    git remote add origin $RepoUrl
}

# 6. Đẩy lên
git branch -M main
git push -u origin main

if ($LASTEXITCODE -eq 0) {
    Write-Host "Hoan tat! Mo $RepoUrl de kiem tra." -ForegroundColor Green
} else {
    Write-Host "Push that bai. Xem thong bao loi o tren (thuong la sai duong dan hoac dang nhap sai tai khoan GitHub)." -ForegroundColor Red
}
