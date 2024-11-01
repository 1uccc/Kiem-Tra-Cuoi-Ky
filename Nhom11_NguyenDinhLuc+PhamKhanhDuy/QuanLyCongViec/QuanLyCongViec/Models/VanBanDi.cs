using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models
{
    public class VanBanDi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaVanBanDi { get; set; }

        [Required(ErrorMessage = "Số văn bản là bắt buộc")]
        [Display(Name = "Số văn bản")]
        public string SoVanBan { get; set; }

        [Required(ErrorMessage = "Ngày gửi là bắt buộc")]
        [Display(Name = "Ngày gửi")]
        [DataType(DataType.Date)]
        public DateTime NgayGui { get; set; }

        [Required(ErrorMessage = "Trích yếu là bắt buộc")]
        [Display(Name = "Trích yếu")]
        public string TrichYeu { get; set; }

        [Required(ErrorMessage = "Đơn vị nhận là bắt buộc")]
        [Display(Name = "Đơn vị nhận")]
        public string DonViNhan { get; set; }
    }
}