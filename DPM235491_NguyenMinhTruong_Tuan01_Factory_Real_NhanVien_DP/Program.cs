using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Factory_Real_ThanhToan_DP
{
    // 1. Abstract Product: Giao diện Thanh toán
    public interface IPhuongThucThanhToan
    {
        void XuLyThanhToan(double soTien, string maHoaDon);
    }

    // 2. Concrete Products: Các hình thức thanh toán thực tế
    public class ThanhToanTienMat : IPhuongThucThanhToan
    {
        public void XuLyThanhToan(double soTien, string maHoaDon)
        {
            Console.WriteLine($"[TIỀN MẶT] Da thu {soTien:N0} VNĐ tien mat cho hoa don '{maHoaDon}'. In bien nhan.");
        }
    }

    public class ThanhToanChuyenKhoan : IPhuongThucThanhToan
    {
        public void XuLyThanhToan(double soTien, string maHoaDon)
        {
            Console.WriteLine($"[CHUYỂN KHOẢN] Tao ma QR Banking cho hoa don '{maHoaDon}'. So tien: {soTien:N0} VNĐ. Cho xac nhan IPN...");
        }
    }

    public class ThanhToanViDienTu : IPhuongThucThanhToan
    {
        public void XuLyThanhToan(double soTien, string maHoaDon)
        {
            Console.WriteLine($"[VÍ ĐIỆN TỬ] Ket noi Cong thanh toan MoMo/VNPay cho hoa don '{maHoaDon}'. So tien: {soTien:N0} VNĐ. Thanh cong!");
        }
    }

    // 3. Creator Class: Lớp Factory cơ sở
    public abstract class ThanhToanFactory
    {
        public abstract IPhuongThucThanhToan TaoPhuongThucThanhToan();

        public void ThucHienGiaoDich(double soTien, string maHoaDon)
        {
            IPhuongThucThanhToan thanhToan = TaoPhuongThucThanhToan();
            thanhToan.XuLyThanhToan(soTien, maHoaDon);
        }
    }

    // 4. Concrete Creators: Các nhà máy cụ thể
    public class TienMatFactory : ThanhToanFactory
    {
        public override IPhuongThucThanhToan TaoPhuongThucThanhToan() => new ThanhToanTienMat();
    }

    public class ChuyenKhoanFactory : ThanhToanFactory
    {
        public override IPhuongThucThanhToan TaoPhuongThucThanhToan() => new ThanhToanChuyenKhoan();
    }

    public class ViDienTuFactory : ThanhToanFactory
    {
        public override IPhuongThucThanhToan TaoPhuongThucThanhToan() => new ThanhToanViDienTu();
    }

    // 5. Client Code (Main)
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  HE THONG XU LY THANH TOAN HOA DON - CONG TY NONG DUOC AN GIANG (FACTORY)");
            Console.WriteLine("==========================================================================\n");

            // 1. Bán lẻ thu tiền mặt
            Console.WriteLine("--- GIAO DỊCH 1: BÁN LẺ THUỐC TRỪ SÂU ---");
            ThanhToanFactory factoryTienMat = new TienMatFactory();
            factoryTienMat.ThucHienGiaoDich(450000, "HD-LE-001");

            // 2. Đại lý bán sỉ chuyển khoản QR
            Console.WriteLine("\n--- GIAO DỊCH 2: BÁN SỈ PHÂN BÓN ---");
            ThanhToanFactory factoryChuyenKhoan = new ChuyenKhoanFactory();
            factoryChuyenKhoan.ThucHienGiaoDich(15500000, "HD-SI-002");

            // 3. Khách mua qua App ví điện tử
            Console.WriteLine("\n--- GIAO DỊCH 3: THANH TOÁN QUA APP ---");
            ThanhToanFactory factoryViDienTu = new ViDienTuFactory();
            factoryViDienTu.ThucHienGiaoDich(1200000, "HD-APP-003");

            Console.ReadLine();
        }
    }
}