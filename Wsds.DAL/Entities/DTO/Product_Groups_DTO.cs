﻿using System;

namespace Wsds.DAL.Entities.DTO
{
    [Serializable]
    public class Product_Groups_DTO
    {
        public long id { get; set; }
        public string name { get; set; }
        public long? parent_id { get; set; }
        public long? id_product_cat { get; set; }
        public string prefix { get; set; }
        public string icon { get; set; }
        public bool? is_show { get; set; }
        public long? priority_index { get; set; }
        public long? priority_show { get; set; }
        public int? is_active { get; set; }
    }
}
