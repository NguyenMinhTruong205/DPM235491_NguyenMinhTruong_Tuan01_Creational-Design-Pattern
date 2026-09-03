using System;
using System.Collections.Generic;

namespace DPM235491_NguyenMinhTruong_Tuan01_Builder_Real_HoaDon_DP
{
    // 1. Product Class: Đối tượng Hóa đơn hoàn chỉnh
    public class HoaDonBanHang
    {
        public string MaHoaDon { get; set; }
        public string TenKhachHang { get; set; }
        public List<string> DanhSachSanPham { get; set; } = new List<string>();
        public double TongTienHang { get; set; }
        public double ChietKhauGiamGia { get; set; } // Phần trăm giảm giá
        public double ChiPhiVanChuyen { get; set; }
        public double ChiPhiDichVuPhu { get; set; }
        public string NhanVienLap { get; set; }

        public double TinhTongThanhToan()
        {
            double tienGiam = TongTienHang * (ChietKhauGiamGia / 100);
            return TongTienHang - tienGiam + ChiPhiVanChuyen + ChiPhiDichVuPhu;
        }

        public void HienThiHoaDon()
        {
            Console.WriteLine($"==================================================");
            Console.WriteLine($"HOA DON BAN HANG: {MaHoaDon}");
            Console.WriteLine($"Khach hang: {TenKhachHang} | Nhan vien lap: {NhanVienLap}");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Danh sach chi tiet hang hoa:");
            foreach (var sp in DanhSachSanPham)
            {
                Console.WriteLine($"  + {sp}");
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Tong tien hang       : {TongTienHang:N0} VNĐ");
            Console.WriteLine($"Chiet khau/Giam gia  : {ChietKhauGiamGia}% (-{(TongTienHang * ChietKhauGiamGia / 100):N0} VNĐ)");
            Console.WriteLine($"Chi phi van chuyen   : {ChiPhiVanChuyen:N0} VNĐ");
            Console.WriteLine($"Chi phi dich vu phu  : {ChiPhiDichVuPhu:N0} VNĐ");
            Console.WriteLine($"=> TONG THANH TOAN   : {TinhTongThanhToan():N0} VNĐ");
            Console.WriteLine($"==================================================\n");
        }
    }

    // 2. Builder Interface
    public interface IHoaDonBuilder
    {
        void BuildThongTinChung(string maHD, string tenKH, string nhanVien);
        void BuildSanPham(string tenSP, string soLo, int soLuong, double donGia);
        void BuildChietKhau(double phanTramGiam);
        void BuildChiPhiPhatSinh(double phiVanChuyen, double phiDichVuPhu);
        HoaDonBanHang GetHoaDon();
    }

    // 3. Concrete Builder
    public class HoaDonNongDuocBuilder : IHoaDonBuilder
    {
        private HoaDonBanHang _hoaDon = new HoaDonBanHang();

        public void BuildThongTinChung(string maHD, string tenKH, string nhanVien)
        {
            _hoaDon.MaHoaDon = maHD;
            _hoaDon.TenKhachHang = tenKH;
            _hoaDon.NhanVienLap = nhanVien;
        }

        public void BuildSanPham(string tenSP, string soLo, int soLuong, double donGia)
        {
            double thanhTien = soLuong * donGia;
            _hoaDon.TongTienHang += thanhTien;
            _hoaDon.DanhSachSanPham.Add($"{tenSP} (Lo: {soLo}) - SL: {soLuong} x {donGia:N0} = {thanhTien:N0} VNĐ");
        }

        public void BuildChietKhau(double phanTramGiam)
        {
            _hoaDon.ChietKhauGiamGia = phanTramGiam;
        }

        public void BuildChiPhiPhatSinh(double phiVanChuyen, double phiDichVuPhu)
        {
            _hoaDon.ChiPhiVanChuyen = phiVanChuyen;
            _hoaDon.ChiPhiDichVuPhu = phiDichVuPhu;
        }

        public HoaDonBanHang GetHoaDon()
        {
            HoaDonBanHang result = _hoaDon;
            _hoaDon = new HoaDonBanHang(); // Reset builder
            return result;
        }
    }

    // 4. Director: Người lập các loại Hóa đơn nghiệp vụ
    public class KeToanBanHangDirector
    {
        private IHoaDonBuilder _builder;

        public KeToanBanHangDirector(IHoaDonBuilder builder)
        {
            _builder = builder;
        }

        // Tạo Hóa đơn bán lẻ thông thường
        public void LapHoaDonBanLe(string maHD, string tenKH, string nhanVien)
        {
            _builder.BuildThongTinChung(maHD, tenKH, nhanVien);
            _builder.BuildSanPham("Thuoc Tru Sau AnGiang-Pest 500ml", "LO-2026-01", 2, 150000);
            _builder.BuildChietKhau(0);
            _builder.BuildChiPhiPhatSinh(0, 0);
        }

        // Tạo Hóa đơn bán sỉ (Áp dụng chiết khấu, vận chuyển, dịch vụ phụ)
        public void LapHoaDonBanSi(string maHD, string tenKH, string nhanVien)
        {
            _builder.BuildThongTinChung(maHD, tenKH, nhanVien);
            _builder.BuildSanPham("Phan Bon Sinh Hoc AnGiang-Bio 50kg", "LO-2025-88", 50, 600000);
            _builder.BuildSanPham("Thuoc Tru Sau AnGiang-Pest 500ml", "LO-2026-01", 20, 150000);
            _builder.BuildChietKhau(8); // Giảm 8%
            _builder.BuildChiPhiPhatSinh(400000, 100000); // Phí VC & Dịch vụ phụ
        }
    }

    // 5. Client Code
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  HE THONG LAP HOA DON BAN HANG - CONG TY NONG DUOC AN GIANG (BUILDER)");
            Console.WriteLine("==========================================================================\n");

            IHoaDonBuilder builder = new HoaDonNongDuocBuilder();
            KeToanBanHangDirector director = new KeToanBanHangDirector(builder);

            // 1. Lập Hóa đơn bán lẻ
            director.LapHoaDonBanLe("HD-LE-001", "Nguyen Van A (Dai ly nho)", "NV_BanHang_01");
            HoaDonBanHang hdLe = builder.GetHoaDon();
            hdLe.HienThiHoaDon();

            // 2. Lập Hóa đơn bán sỉ
            director.LapHoaDonBanSi("HD-SI-002", "Hợp Tac Xa Nong Nghiep An Giang", "NV_BanHang_02");
            HoaDonBanHang hdSi = builder.GetHoaDon();
            hdSi.HienThiHoaDon();

            Console.WriteLine("Nhan phim bat ky de thoat...");
            Console.ReadLine();
        }
    }
}