using DragnDrop.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DragnDrop
{
  public class MainViewModel : BaseViewModel
  {
    public MainViewModel()
    {
      var itemProvider = new ItemProvider();
      //this.CategoryTreeRoot = new CategoryItem("Categories") { IsExpanded = true };
      //this.TreeViewItems = new ObservableCollection<CategoryItem>() { this.CategoryTreeRoot };
      this.TreeViewItems = new ObservableCollection<CategoryItem>(itemProvider.TreeVItems);

      this.ListBoxItems = new ObservableCollection<string>();

      this.InitializeLitBoxItems();
    }

    public CategoryItem CategoryTreeRoot { get; }

    private void InitializeLitBoxItems()
    {
      this.ListBoxItems.Add("Fruit Ninja");
      this.ListBoxItems.Add("Opera Browser");
      this.ListBoxItems.Add("Notepad");
      this.ListBoxItems.Add("MS Excel");
      this.ListBoxItems.Add("MS Word");
      this.ListBoxItems.Add("MS PowerPoint");
      this.ListBoxItems.Add("MS Paint");
    }

    private ObservableCollection<CategoryItem> treeViewItems;
    public ObservableCollection<CategoryItem> TreeViewItems
    {
      get => this.treeViewItems;
      set
      {
        this.treeViewItems = value;
        this.OnPropertyChanged();
      }
    }

    private ObservableCollection<string> listBoxItems;
    public ObservableCollection<string> ListBoxItems
    {
      get => this.listBoxItems;
      set
      {
        this.listBoxItems = value;
        this.OnPropertyChanged();
      }
    }
  }
}
