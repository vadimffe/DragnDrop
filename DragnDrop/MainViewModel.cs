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

      this.ListBoxItems = new ObservableCollection<string>();

      this.ListBoxItems.Add("Mortal Combat");
      this.ListBoxItems.Add("Opera Browser");
      this.ListBoxItems.Add("Notepad");
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
