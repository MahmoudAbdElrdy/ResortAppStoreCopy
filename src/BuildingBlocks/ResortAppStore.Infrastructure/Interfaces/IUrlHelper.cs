﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces {
    public interface IUrlHelper {
        string GetCurrentUrl();
        string GetCurrentUrl(string url);
    }
}
