﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class ValidationStepResult
    {
        public string name { get; set; }
        public string status { get; set; }
        public Error error { get; set; }
    }
}
