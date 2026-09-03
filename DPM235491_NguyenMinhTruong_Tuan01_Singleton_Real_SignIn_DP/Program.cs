using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Singleton_Real_SignIn_DP
{
    // Singleton Session Manager: Quản lý Phiên Đăng Nhập & Phân Quyền Nhân Viên
    public sealed class PhienDangNhapSession
    {
        private static PhienDangNhapSession _instance = null;
        private static readonly object _lock = new object();

        public string MaNhanVien { get; private set; }
        public string TenNhanVien { get; private set; }
        public string QuyenHan { get; private set; } // Ví dụ: "QuanLyKho", "NhanVienBanHang", "Admin"
        public DateTime ThoiGianDangNhap { get; private set; }

        private PhienDangNhapSession() { }

        public static PhienDangNhapSession Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PhienDangNhapSession();
                    }
                    return _instance;
                }
            }
        }

        // Thực hiện Đăng nhập hệ thống
        public void DangNhap(string maNV, string tenNV, string quyen)
        {
            MaNhanVien = maNV;
            TenNhanVien = tenNV;
            QuyenHan = quyen;
            ThoiGianDangNhap = DateTime.Now;
            Console.WriteLine($"[DANG NHAP THANH CONG] Nhan vien: {TenNhanVien} ({MaNhanVien}) - Quyen: {QuyenHan}");
        }

        // Kiểm tra quyền hạn trước khi cho phép thực hiện thao tác
        public bool KiemTraQuyen(string quyenYeuCau)
        {
            if (string.IsNullOrEmpty(MaNhanVien))
            {
                Console.WriteLine("[TU TCOI] Chua co nhan vien nao dang nhap vao he thong!");
                return false;
            }

            if (QuyenHan.Equals(quyenYeuCau, StringComparison.OrdinalIgnoreCase) || QuyenHan == "Admin")
            {
                Console.WriteLine($"[CHO PHEP] Nhan vien {TenNhanVien} co quyen '{quyenYeuCau}' de thuc hien thao tac.");
                return true;
            }

            Console.WriteLine($"[TU TCOI] Nhan vien {TenNhanVien} (Quyen: {QuyenHan}) KHONG CO QUYEN '{quyenYeuCau}'!");
            return false;
        }

        public void HienThiThongTinSession()
        {
            Console.WriteLine($"\n--- PHIEN DANG NHAP HIENTAI ---");
            Console.WriteLine($"  - NV: {TenNhanVien} ({MaNhanVien})");
            Console.WriteLine($"  - Quyen: {QuyenHan}");
            Console.WriteLine($"  - Thoi gian: {ThoiGianDangNhap:dd/MM/yyyy HH:mm:ss}\n");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  HE THONG PHAN QUYEN DANG NHAP - CONG TY NONG DUOC AN GIANG (SINGLETON)");
            Console.WriteLine("==========================================================================\n");

            // 1. Thực hiện Đăng nhập ở Module Bán hàng
            PhienDangNhapSession session1 = PhienDangNhapSession.Instance;
            session1.DangNhap("NV-DPM235491", "Nguyen Minh Truong", "NhanVienBanHang");
            session1.HienThiThongTinSession();

            // 2. Thử thực hiện chức năng Bán hàng
            Console.WriteLine("--- THỬ THỰC HIỆN BÁN HÀNG ---");
            session1.KiemTraQuyen("NhanVienBanHang");

            // 3. Giả lập gọi Session ở Module Quản lý Kho khác
            Console.WriteLine("\n--- TRUY CẬP TỪ MODULE KHO ---");
            PhienDangNhapSession session2 = PhienDangNhapSession.Instance;

            // Kiểm tra xem 2 instance có thực sự là 1 không
            if (session1 == session2)
            {
                Console.WriteLine("-> Xac nhan: Ca hai Module deu dung chung 1 Session duy nhat.");
            }

            // Thử thực hiện hành động yêu cầu quyền Kho
            session2.KiemTraQuyen("QuanLyKho");

            Console.ReadLine();
        }
    }
}