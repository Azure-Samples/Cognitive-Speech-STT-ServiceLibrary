﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface.IService
{
    public interface ILuisService
    {
        Task<dynamic> GetIntention(string text);
    }
}
