using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Abstract_Real_XLĐH_DP
{
    // --- 1. ABSTRACT PRODUCTS (Các sản phẩm trong quy trình xử lý đơn hàng) ---

    // Abstract Product A: Kiểm tra kho hàng
    public interface IKiemTraKho
    {
        bool KiemTraHangTon(string tenSP, string soLo, int soLuong);
    }

    // Abstract Product B: Tính tổng tiền đơn hàng
    public interface ITinhTienDonHang
    {
        double TinhTongTien(double donGia, int soLuong, double phiVanChuyen);
    }

    // --- 2. CONCRETE PRODUCTS FOR ĐƠN HÀNG BÁN SỈ ---
    public class KiemTraKhoFIFOSi : IKiemTraKho
    {
        public bool KiemTraHangTon(string tenSP, string soLo, int soLuong)
        {
            Console.WriteLine($"[KHO SỈ - FIFO] Kiem tra lo '{soLo}' cho san pham '{tenSP}' (Uu tien lo gan het han). Du hang!");
            return true;
        }
    }

    public class TinhTienDonHangSi : ITinhTienDonHang
    {
        public double TinhTongTien(double donGia, int soLuong, double phiVanChuyen)
        {
            double tienHang = donGia * soLuong;
            double chietKhau = tienHang * 0.12; // Chiết khấu bán sỉ 12%
            return tienHang - chietKhau + phiVanChuyen;
        }
    }

    // --- 3. CONCRETE PRODUCTS FOR ĐƠN HÀNG BÁN LẺ ---
    public class KiemTraKhoChiDinhLe : IKiemTraKho
    {
        public bool KiemTraHangTon(string tenSP, string soLo, int soLuong)
        {
            Console.WriteLine($"[KHO LẺ - CHỈ ĐỊNH] Kiem tra truc tiep lo '{soLo}' cho san pham '{tenSP}'. Du hang!");
            return true;
        }
    }

    public class TinhTienDonHangLe : ITinhTienDonHang
    {
        public double TinhTongTien(double donGia, int soLuong, double phiVanChuyen)
        {
            // Bán lẻ không chiết khấu
            return (donGia * soLuong) + phiVanChuyen;
        }
    }

    // --- 4. ABSTRACT FACTORY ---
    public interface IXuLyDonHangFactory
    {
        IKiemTraKho TaoKiemTraKho();
        ITinhTienDonHang TaoTinhTien();
    }

    // --- 5. CONCRETE FACTORIES ---
    public class DonHangBanSiFactory : IXuLyDonHangFactory
    {
        public IKiemTraKho TaoKiemTraKho() => new KiemTraKhoFIFOSi();
        public ITinhTienDonHang TaoTinhTien() => new TinhTienDonHangSi();
    }

    public class DonHangBanLeFactory : IXuLyDonHangFactory
    {
        public IKiemTraKho TaoKiemTraKho() => new KiemTraKhoChiDinhLe();
        public ITinhTienDonHang TaoTinhTien() => new TinhTienDonHangLe();
    }

    // --- 6. CLIENT ---
    public class QuyTrinhDonHangClient
    {
        private readonly IKiemTraKho _kiemTraKho;
        private readonly ITinhTienDonHang _tinhTien;

        public QuyTrinhDonHangClient(IXuLyDonHangFactory factory)
        {
            _kiemTraKho = factory.TaoKiemTraKho();
            _tinhTien = factory.TaoTinhTien();
        }

        public void XuLyDonHang(string maDon, string tenSP, string soLo, double donGia, int soLuong, double phiVC)
        {
            Console.WriteLine($"=== XU LY DON HANG: {maDon} ===");
            if (_kiemTraKho.KiemTraHangTon(tenSP, soLo, soLuong))
            {
                double tongTien = _tinhTien.TinhTongTien(donGia, soLuong, phiVC);
                Console.WriteLine($"  ==> Xac nhan don hang thanh cong! Tong thanh toan: {tongTien:N0} VNĐ\n");
            }
        }
    }

    // --- 7. MAIN PROGRAM ---
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  HE THONG XU LY DON HANG NONG DUOC AN GIANG (ABSTRACT FACTORY)");
            Console.WriteLine("==========================================================================\n");

            // 1. Xử lý Đơn hàng Bán sỉ
            IXuLyDonHangFactory factorySi = new DonHangBanSiFactory();
            QuyTrinhDonHangClient clientSi = new QuyTrinhDonHangClient(factorySi);
            clientSi.XuLyDonHang("DH-SI-2026-001", "Thuoc Tru Sau AnGiang-Pest 500ml", "LO-2026-A1", 150000, 200, 500000);

            // 2. Xử lý Đơn hàng Bán lẻ
            IXuLyDonHangFactory factoryLe = new DonHangBanLeFactory();
            QuyTrinhDonHangClient clientLe = new QuyTrinhDonHangClient(factoryLe);
            clientLe.XuLyDonHang("DH-LE-2026-002", "Thuoc Tru Sau AnGiang-Pest 500ml", "LO-2026-A1", 150000, 3, 20000);

            Console.ReadLine();
        }
    }
}