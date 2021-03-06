# Quick-Pdf-Editor

# Mở đầu
Quick Pdf Editor là phần mềm chỉnh sửa nhanh file pdf hoàn toàn miễn phí và mã nguồn mở.
Dưới đây là là cách để sử dụng project trên máy tính của bạn.

# Yêu cầu hệ thống:
- Visual studio 2017 cùng với .Net 4.7.2 trở lên
- Windows 10
- .Net core 2.2 trở lên

# Cài đặt: 
- Bước 1: Tải và cài đặt các phần mềm yêu cầu ở bước trên (nếu chưa có)
- Bước 2: Tải toàn bộ project về máy dưới dạng file zip và giải nén.
- Bước 3: Sau khi mở project, thêm file windows.winmd vào references. Đường dẫn: C:\Program Files (x86)\Windows Kits\10\UnionMetadata\...\Windows.winmd
- Lưu ý: Phải chuyển Filefilter sang All files
- Bước 4: Tải thư mục Icon ở link bên dưới và copy vào thư mục debug của project: 
https://1drv.ms/f/s!AqpU4ZslX7KVgfUk-0Zh8i3TVy5JPQ
- Bước 5: Tải Itext7 (nếu chưa có). Có thể tải bằng Nuget như sau: PM>Install-Package itext7 -Version 7.1.4


# Cách tạo setup:
Do Microsoft Visual Studio Installer Projects không cho phép project có file .winmd nên hãy sử dụng các phần mềm tạo setup khác như Inno Script Studio

Link Setup: https://1drv.ms/u/s!AqpU4ZslX7KVgfVH5763v5AhoO3EiA

# Built With
- Windows Presentation Foundation (WPF) (C#) - Tạo giao diện người dùng
- Windows 10 API - Đọc file pdf và chuyển đổi thành bitmap tạo giao diện đọc pdf
- Itext7 community - Thao tác chỉnh sửa và đọc, ghi file pdf

# Tính năng
- Thao tác file pdf trên nhiều tab
- Chèn trang từ file pdf khác
- Xóa trang trong file pdf
- Gộp nhanh nhiều file pdf thành một file.

# Tác giả
- Đào Mạnh Dũng (Bắt đầu dự án): https://github.com/manhdung99
- Phạm Duy Cường: https://github.com/phamduycuong

# Báo cáo đồ án
https://1drv.ms/w/s!AqpU4ZslX7KVgfU1uI8Ed4aOY3rLvA

# Video Demo
https://1drv.ms/f/s!AqpU4ZslX7KVgfVI8eS7Oijcq2vX2w
