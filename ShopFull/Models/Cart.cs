//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopFull.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public int Id { get; set; }
        public Nullable<int> accauntId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<int> ProductId { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Product Product { get; set; }
    }
}
