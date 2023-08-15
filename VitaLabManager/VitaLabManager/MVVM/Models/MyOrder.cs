using System;
using System.Collections.Generic;
using VitLabData.DTOs;

namespace VitaLabManager.MVVM.Models
{
    public class MyOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderDataDTO> OrderDatas { get; set; }

    }
}
