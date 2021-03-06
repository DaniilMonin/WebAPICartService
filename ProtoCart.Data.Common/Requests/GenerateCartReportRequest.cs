﻿using System;

namespace ProtoCart.Data.Common.Requests
{
    public class GenerateCartReportRequest : Request
    {
        public int ReportTemplateId { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
    }
}