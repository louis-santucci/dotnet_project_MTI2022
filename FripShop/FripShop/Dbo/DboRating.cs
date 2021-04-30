using System;
using System.Collections.Generic;
using FripShop.Dbo;

#nullable disable

namespace FripShop.Dbo
{
    public class DboRating : IDbo
    {
        public long Id { get; set; }
        public long SellerId { get; set; }
        public int Note { get; set; }
        public string Comment { get; set; }
    }
}
