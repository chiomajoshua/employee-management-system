﻿namespace EMS.Data.Entities
{
    public class Wallet : BaseEntity
    {
        public decimal Balance { get; set; }
        public virtual Employee Employee { get; set; }
    }
}