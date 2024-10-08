﻿using System;

namespace CurrencyApiInfrastructure.DbBase
{
    public interface ISoftDeletable
    {
        public DateTime? DeletedAt { get; set; }

        public string DeletedBy { get; set; }
    }
}
