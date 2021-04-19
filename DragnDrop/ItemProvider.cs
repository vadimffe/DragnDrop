using DragnDrop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragnDrop
{
  public class ItemProvider
  {
    private readonly CategoryItem _rootDirectoryItem;

    public ItemProvider()
    {
      _rootDirectoryItem = new CategoryItem("X") { ItemName = "X" };

      var childItem1 = new CategoryItem("Productive") { ItemName = "Productive" };

      var grandChildItem11 = new CategoryItem("Google Chrome") { ItemName = "Google Chrome" };
      var grandChildItem12 = new CategoryItem("Mozilla Firefox") { ItemName = "Mozilla Firefox" };

      childItem1.AddItem(grandChildItem11);
      childItem1.AddItem(grandChildItem12);

      var childItem2 = new CategoryItem("Unproductive") { ItemName = "Unproductive" };
      var childItem3 = new CategoryItem("Unknown") { ItemName = "Unknown" };

      var grandChildItem121 = new CategoryItem("Angry Birds") { ItemName = "Angry Birds" };
      childItem2.AddItem(grandChildItem121);

      var childList1 = new ObservableCollection<CategoryItem>
         {
            childItem1,
            childItem2,
            childItem3
         };

      _rootDirectoryItem.CategoryItems = childList1;
    }

    public ObservableCollection<CategoryItem> DirItems => _rootDirectoryItem.Traverse(_rootDirectoryItem);
  }
}
