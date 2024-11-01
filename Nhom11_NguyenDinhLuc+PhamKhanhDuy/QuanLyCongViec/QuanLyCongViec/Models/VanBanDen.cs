using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models
{
    public class VanBanDen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaVanBanDen { get; set; }

        [Required(ErrorMessage = "Số văn bản là bắt buộc")]
        [Display(Name = "Số văn bản")]
        public string SoVanBan { get; set; }

        [Required(ErrorMessage = "Ngày nhận là bắt buộc")]
        [Display(Name = "Ngày nhận")]
        [DataType(DataType.Date)]
        public DateTime NgayNhan { get; set; }

        [Required(ErrorMessage = "Trích yếu là bắt buộc")]
        [Display(Name = "Trích yếu")]
        public string TrichYeu { get; set; }

        [Required(ErrorMessage = "Đơn vị gửi là bắt buộc")]
        [Display(Name = "Đơn vị gửi")]
        public string DonViGui { get; set; }

        [Required(ErrorMessage = "Nơi nhận là bắt buộc")]
        [Display(Name = "Nơi nhận")]
        public string NoiNhan { get; set; }
    }
}