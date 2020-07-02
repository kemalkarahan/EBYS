using Entities.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class ProfileEditViewModel : IEntity
    {
        public Staff Staff{ get; set; }
        public Staff Manager { get; set; }
        [Display(Name = "Cinsiyet")]
        [StringLength(maximumLength: 5, MinimumLength = 2)]
        public string Gender { get; set; }
        [Display(Name = "Doğum Tarihi")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Profil  Resmi")]
        public IFormFile ProfileImage { get; set; }
        [StringLength(maximumLength: 512)]
        [Display(Name = "Eğitim")]
        public string Education { get; set; }
        [StringLength(maximumLength: 512)]
        [Display(Name = "Yetenekler")]
        public string Skills { get; set; }
        [StringLength(maximumLength: 512)]
        [Display(Name = "Açıklama")]
        public string Notes { get; set; }
    }
}
