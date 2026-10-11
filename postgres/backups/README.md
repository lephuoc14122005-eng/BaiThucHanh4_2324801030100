# PostgreSQL database backups

Các file `.backup` trong thư mục này là bản dump custom-format được tạo bằng `pg_dump` 11.4 từ bốn database khai báo trong `../createdatabases.sql`.

## Khôi phục trên Windows

Mở terminal tại thư mục này và chạy từng lệnh sau (cần PostgreSQL đã chạy, `pg_restore` có trong PATH; đổi host, port hoặc user nếu cấu hình khác):

```powershell
pg_restore -h localhost -p 5432 -U postgres --create --no-owner --no-privileges -d postgres .\lab_netmicro_payments.backup
pg_restore -h localhost -p 5432 -U postgres --create --no-owner --no-privileges -d postgres .\lab_netmicro_jobs.backup
pg_restore -h localhost -p 5432 -U postgres --create --no-owner --no-privileges -d postgres .\lab_netmicro_policy.backup
pg_restore -h localhost -p 5432 -U postgres --create --no-owner --no-privileges -d postgres .\lab_netmicro_pricing.backup
```

`--create` sẽ tạo database từ archive; không cần tạo bốn database trước. Các bản backup được lập ngay sau khi chạy script khởi tạo, nên hiện chứa database rỗng (chưa có bảng hay dữ liệu nghiệp vụ). Archive đã được kiểm tra bằng `pg_restore --list`.
