//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LapOnline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CTDONHANGs = new HashSet<CTDONHANG>();
        }
    
        public int MaSP { get; set; }
        public Nullable<int> MaGiamGia { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> RAM { get; set; }
        public string ViXuLy { get; set; }
        public string KichThuocMH { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuongCon { get; set; }
        public System.DateTime NgaySX { get; set; }
        public int MaLoaiSP { get; set; }
        public int MaNSX { get; set; }
        public string HinhAnh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDONHANG> CTDONHANGs { get; set; }
        public virtual LOAISP LOAISP { get; set; }
        public virtual NHASANXUAT NHASANXUAT { get; set; }
    }
}
