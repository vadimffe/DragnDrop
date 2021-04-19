using DragnDrop.Models;
using System.Collections.ObjectModel;

namespace DragnDrop
{
  public class ItemProvider
  {
    private readonly CategoryItem rootItem;

    public ItemProvider()
    {
      this.rootItem = new CategoryItem("Categories");

      var childItem1 = new CategoryItem("Productive");
      var childItem2 = new CategoryItem("Unproductive");
      var childItem3 = new CategoryItem("Uncategorized");
      var childItem4 = new CategoryItem("Unknown");

      var grandChildItem1 = new CategoryItem("Mozilla Firefox");
      var grandChildItem2 = new CategoryItem("Google Chrome");

      var grandChildItem3 = new CategoryItem("Angry Birds");
      var grandChildItem4 = new CategoryItem("Mortal Combat");

      childItem1.AddItem(grandChildItem1);
      childItem1.AddItem(grandChildItem2);

      childItem2.AddItem(grandChildItem3);
      childItem2.AddItem(grandChildItem4);

      var childList1 = new ObservableCollection<CategoryItem>
         {
            childItem1,
            childItem2,
            childItem3,
            childItem4
         };

      this.rootItem.CategoryItems = childList1;
    }

    public ObservableCollection<CategoryItem> TreeVItems => this.rootItem.Traverse(this.rootItem);
  }
}
