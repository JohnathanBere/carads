using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using simpproj.Models;
using System.ComponentModel.DataAnnotations;

namespace simpproj.ViewModels
{
    public class UsersNew
    {
        [MaxLength(128)]
        public string Firstname { get; set; }

        [MaxLength(128)]
        public string Lastname { get; set; }

        [MaxLength(128)]
        public string Phonenumber { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UsersEdit
    {
        [MaxLength(128)]
        public string Firstname { get; set; }

        [MaxLength(128)]
        public string Lastname { get; set; }

        [MaxLength(128)]
        public string Phonenumber { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        [Required, MaxLength(128)]
        public string Username { get; set; }
    }

    public class UsersResetPassword
    {
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}