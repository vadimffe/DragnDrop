using DragnDrop.Models;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DragnDrop
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private ObservableCollection<string> listBoxItems;
    private TreeViewItem draggedItem;
    private ListBox? dragSource = null;
    private object mydata;

    private int FindTreeLevel(DependencyObject control)
    {
      var level = -1;
      if (control != null)
      {
        var parent = VisualTreeHelper.GetParent(control);
        while (!(parent is TreeView) && (parent != null))
        {
          if (parent is TreeViewItem)
            level++;
          parent = VisualTreeHelper.GetParent(parent);
        }
      }
      return level;
    }

    private void TextBox_PreviewDragEnter(object sender, DragEventArgs e)
    {
      e.Handled = true;
    }

    private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
      TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

      TreeViewItem f = e.Source as TreeViewItem;

      if (treeViewItem != null)
      {
        treeViewItem.Focus();
        e.Handled = true;

        ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(f);
        if (parent != null)
        {
          parent.Items.Remove(f);
        }
      }
    }

    static TreeViewItem VisualUpwardSearch(DependencyObject source)
    {
      while (source != null && !(source is TreeViewItem))
        source = VisualTreeHelper.GetParent(source);

      return source as TreeViewItem;
    }

    private void TreeView_Drop(object sender, DragEventArgs e)
    {
      e.Effects = DragDropEffects.None;
      e.Handled = true;
      this.draggedItem = new TreeViewItem() { Header = this.mydata };
      TreeViewItem targetItem = this.GetNearestContainer(e.OriginalSource as UIElement);
      CategoryItem target = (CategoryItem)targetItem.Header;

      Debug.WriteLine(FindTreeLevel(e.OriginalSource as DependencyObject));
      if (targetItem != null && this.draggedItem != null && FindTreeLevel(e.OriginalSource as DependencyObject) != 1)
      {
        e.Effects = DragDropEffects.Move;

        CategoryItem draggedTreeItem = new CategoryItem(string.Empty) { ItemName = this.mydata.ToString() };
        this.AddChild(draggedTreeItem, target);
        this.listBoxItems.Remove(this.mydata.ToString());
      }
    }

    public void AddChild(CategoryItem sourceItem, CategoryItem targetItem)
    {
      if (targetItem == null)
      {
        return;
      }

      targetItem.CategoryItems.Add(sourceItem);

      if (sourceItem.CategoryItems != null)
      {
        foreach (CategoryItem item in sourceItem.CategoryItems)
        {
          this.AddChild(item, sourceItem);
        }
      }
    }

    private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      ListBox parent = (ListBox)sender;
      this.dragSource = parent;
      this.listBoxItems = parent.ItemsSource as ObservableCollection<string>;
      this.mydata = GetDataFromListBox(this.dragSource, e.GetPosition(parent));

      if (this.mydata != null)
      {
        DragDrop.DoDragDrop(parent, this.mydata, DragDropEffects.Move);
      }
    }

    private static object? GetDataFromListBox(ListBox source, Point point)
    {
      UIElement? element = source.InputHitTest(point) as UIElement;
      if (element != null)
      {
        object data = DependencyProperty.UnsetValue;
        while (data == DependencyProperty.UnsetValue)
        {
          data = source.ItemContainerGenerator.ItemFromContainer(element);

          if (data == DependencyProperty.UnsetValue)
          {
            element = VisualTreeHelper.GetParent(element) as UIElement;
          }

          if (element == source)
          {
            return null;
          }
        }

        if (data != DependencyProperty.UnsetValue)
        {
          return data;
        }
      }

      return null;
    }

    private TreeViewItem GetNearestContainer(UIElement? element)
    {
      TreeViewItem? container = element as TreeViewItem;
      while ((container == null) && (element != null))
      {
        element = VisualTreeHelper.GetParent(element) as UIElement;
        container = element as TreeViewItem;
      }

      return container;
    }
  }
}
