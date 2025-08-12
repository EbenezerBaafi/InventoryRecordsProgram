using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public record InventoryItem : IInventoryEntity
    {
        #region fields
        public int Id { get; init; }
        public string Name { get; init; }
        public int Quantity { get; init; }
        public DateTime DateAdded { get; init; }

        #endregion fields

        #region constructors

        // Constructor to initialize the record
        public InventoryItem(int id, string name, int quantity, DateTime dateAdded)
        {
            this.Id = id;
            this.Name = name;
            this.Quantity = quantity;
            this.DateAdded = dateAdded;
        }
        #endregion constructors

    }

}
