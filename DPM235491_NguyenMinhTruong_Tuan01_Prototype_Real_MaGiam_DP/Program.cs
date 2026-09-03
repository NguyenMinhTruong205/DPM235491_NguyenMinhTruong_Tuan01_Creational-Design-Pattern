using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Prototype_Real_MaGiamGia_DP
{
    // Class chứa thông tin điều kiện áp dụng Mã giảm giá
    public class DieuKienApDung
    {
        public double GiaTriDonToiThieu { get; set; }
        public string LoaiKhachHang { get; set; } // "DaiLySi", "KhachHangLe", "VIP"

        public DieuKienApDung(double giaTriMin, string loaiKH)
        {
            GiaTriDonToiThieu = giaTriMin;
            LoaiKhachHang = loaiKH;
        }
    }

    // Prototype Class: Mã Giảm Giá / Voucher Khuyến Mãi Nông Dược
    public class MaGiamGiaNongDuoc
    {
        public string CodeVoucher { get; set; }
        public double PhanTramGiam { get; set; }
        public double GiamToiDa { get; set; }
        public DateTime HanSuDung { get; set; }
        public DieuKienApDung DieuKien { get; set; }

        public MaGiamGiaNongDuoc(string code, double phanTram, double giamMax, DateTime hsd, DieuKienApDung dieuKien)
        {
            CodeVoucher = code;
            PhanTramGiam = phanTram;
            GiamToiDa = giamMax;
            HanSuDung = hsd;
            DieuKien = dieuKien;
        }

        // Deep Copy: Nhân bản sâu đối tượng voucher mẫu
        public MaGiamGiaNongDuoc SaoChepDeepCopy(string codeMoi, DateTime hsdMoi)
        {
            MaGiamGiaNongDuoc clone = (MaGiamGiaNongDuoc)this.MemberwiseClone();
            clone.CodeVoucher = codeMoi;
            clone.HanSuDung = hsdMoi;
            clone.DieuKien = new DieuKienApDung(this.DieuKien.GiaTriDonToiThieu, this.DieuKien.LoaiKhachHang);
            return clone;
        }

        public void HienThiThongTin()
        {
            Console.WriteLine($"[MA GIAM GIA: {CodeVoucher}] Giam {PhanTramGiam}% (Toi da: {GiamToiDa:N0} VNĐ)");
            Console.WriteLine($"  - Han su dung: {HanSuDung:dd/MM/yyyy}");
            Console.WriteLine($"  - Dieu kien: Don toi thieu {DieuKien.GiaTriDonToiThieu:N0} VNĐ | Ap dung: {DieuKien.LoaiKhachHang}\n");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  HE THONG QUAN LY MA GIAM GIA (PROTOTYPE PATTERN) - NONG DUOC AN GIANG");
            Console.WriteLine("==========================================================================\n");

            // 1. Tạo Mã giảm giá mẫu ban đầu (Prototype)
            DieuKienApDung dkSi = new DieuKienApDung(10000000, "DaiLySi");
            MaGiamGiaNongDuoc voucherMau = new MaGiamGiaNongDuoc("KM-VUBUM-10", 10, 2000000, DateTime.Now.AddDays(30), dkSi);

            Console.WriteLine("--- MA GIAM GIA MAU (PROTOTYPE) ---");
            voucherMau.HienThiThongTin();

            // 2. Nhân bản (Clone Deep Copy) ra Mã đợt mới mà không ảnh hưởng mã gốc
            MaGiamGiaNongDuoc voucherDot2 = voucherMau.SaoChepDeepCopy("KM-VUBUM-DOT2", DateTime.Now.AddDays(60));
            voucherDot2.DieuKien.LoaiKhachHang = "KhachHangVIP"; // Thay đổi điều kiện khách hàng trên bản sao

            Console.WriteLine("--- MA GIAM GIA NHAN BAN (DOT 2) ---");
            voucherDot2.HienThiThongTin();

            Console.ReadLine();
        }
    }
}