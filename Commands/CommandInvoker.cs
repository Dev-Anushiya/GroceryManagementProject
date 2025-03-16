using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroceryManSystem.Models;

namespace GroceryManSystem.Commands
{
    public interface ICommand
    {
        void Execute();
    }
    public class CommandInvoker
    {
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }

    public class OrderUpdateCommand : ICommand
    {
        private readonly Order _order;

        public OrderUpdateCommand(Order order)
        {
            _order = order;
        }

        public void Execute()
        {
            // Update order status to 'Paid' in the database
            _order.Status = "Paid";
            // Save changes to the database
        }
    }

    public class InventoryUpdateCommand : ICommand
    {
        private readonly Order _item;

        public InventoryUpdateCommand(Order item)
        {
            _item = item;
        }

        public void Execute()
        {
            // Reduce inventory based on item quantity in the order
            //var inventoryItem = /* fetch inventory item by _item.Id */;
            //inventoryItem.StockLevel -= _item.TotalAmount;
            // Save changes to the database
        }
    }
}