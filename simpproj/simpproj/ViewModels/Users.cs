using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using simpproj.Models;
using System.ComponentModel.DataAnnotations;

namespace simpproj.ViewModels
{
    public class UsersForm
    {
        public bool IsNew { get; set; }

        // int? Determines if we are looking at the creation of new data or the alteration of existing data.
        public int? UserId { get; set; }

        [MaxLength(128)]
        public string Firstname { get; set; }

        [MaxLength(128)]
        public string Lastname { get; set; }

        [MaxLength(128)]
        public string Phonenumber { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(256), DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password_Confirmation { get; set; }
    }
}