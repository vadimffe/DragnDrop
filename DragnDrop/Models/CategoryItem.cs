using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DragnDrop.Models
{
  public class CategoryItem : BaseViewModel
  {
    public CategoryItem(string itemName)
    {
      this.CategoryItems = new ObservableCollection<CategoryItem>();
      this.ItemName = itemName;
    }

    public void AddItem(CategoryItem directoryItem)
    {
      this.CategoryItems.Add(directoryItem);
    }

    public ObservableCollection<CategoryItem> Traverse(CategoryItem it)
    {
      var items = new ObservableCollection<CategoryItem>();

      foreach (var itm in it.CategoryItems)
      {
        Traverse(itm);
        items.Add(itm);
      }

      return items;
    }

    public CategoryItem DeepClone() => this.DeepClone(this);

    private CategoryItem DeepClone(CategoryItem originalCategoryModel)
    {
      var categoryModelCopy =
        new CategoryItem(originalCategoryModel.ItemName) { IsExpanded = originalCategoryModel.IsExpanded };

      foreach (CategoryItem childCategoryModel in originalCategoryModel.CategoryItems)
      {
        CategoryItem childCopy = this.DeepClone(childCategoryModel);
        categoryModelCopy.CategoryItems.Add(childCopy);
      }

      return categoryModelCopy;
    }

    protected virtual void OnSelected() => this.Selected?.Invoke(this, EventArgs.Empty);

    public event EventHandler Selected;

    private ObservableCollection<CategoryItem> categoryItems;
    public ObservableCollection<CategoryItem> CategoryItems
    {
      get => this.categoryItems;
      set
      {
        this.categoryItems = value;
        this.OnPropertyChanged();
      }
    }

    private bool isExpanded;
    public bool IsExpanded
    {
      get => this.isExpanded;
      set
      {
        this.isExpanded = value;
        this.OnPropertyChanged();
      }
    }

    private bool isSelected;
    public bool IsSelected
    {
      get => this.isSelected;
      set
      {
        this.isSelected = value;
        this.OnPropertyChanged();
        if (this.isSelected)
        {
          this.OnSelected();
        }
      }
    }

    private string itemName;
    public string ItemName
    {
      get => this.itemName;
      set
      {
        this.itemName = value;
        this.OnPropertyChanged();
      }
    }
  }
}
