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
      this.DirItems = new ObservableCollection<CategoryItem>(itemProvider.DirItems);

      this.ListBoxItems = new ObservableCollection<CategoryItem>();

      this.ListBoxItems.Add(new CategoryItem("Mortal Combat") { ItemName = "Mortal Combat" });
      this.ListBoxItems.Add(new CategoryItem("Opera Browser") { ItemName = "Opera Browser" });
      this.ListBoxItems.Add(new CategoryItem("Notepad") { ItemName = "Notepad" });
    }

    private ObservableCollection<CategoryItem> dirItems;
    public ObservableCollection<CategoryItem> DirItems
    {
      get => this.dirItems;
      set
      {
        this.dirItems = value;
        this.OnPropertyChanged();
      }
    }

    private ObservableCollection<CategoryItem> listBoxItems;
    public ObservableCollection<CategoryItem> ListBoxItems
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
