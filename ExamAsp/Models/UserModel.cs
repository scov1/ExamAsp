﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAsp.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}