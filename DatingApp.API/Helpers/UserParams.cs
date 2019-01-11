﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Helpers
{
    /// <summary>
    /// correto com o video
    /// </summary>
    public class UserParams
    {
        private const int MaxPageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int pageSize =10;
        

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize =  (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int UserId { get; set; }
        public string Genero { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 99;

        public string OrderBy { get; set; }

    }
}
