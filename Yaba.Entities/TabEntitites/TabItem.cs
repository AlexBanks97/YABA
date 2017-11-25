using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Entities.TabEntitites
{
    public class TabItem
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Tab Tab { get; set; }
        public TabCategory Category { get; set; }
        // Add prop for user whom created tab item

    }
}
