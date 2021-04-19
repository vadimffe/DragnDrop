using DragnDrop.Models;
using System;
using System.Collections;
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

    ListBox dragSource = null;

    private void TreeView_Drop(object sender, DragEventArgs e)
    {
      ListBox parent = (ListBox)sender;
      object data = e.Data.GetData(typeof(string));
      ((IList)dragSource.ItemsSource).Remove(data);
      parent.Items.Add(data);
    }

    private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      ListBox parent = (ListBox)sender;
      dragSource = parent;
      object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

      if (data != null)
      {
        DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
      }
    }

    private static object GetDataFromListBox(ListBox source, Point point)
    {
      UIElement element = source.InputHitTest(point) as UIElement;
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

    // Recursive call
    private bool TryGetSelectedNode(
      CategoryItem parentNode,
      out (CategoryItem Parent, CategoryItem SelectedChild) selectedNode)
    {
      selectedNode = (null, null);
      if (parentNode == null)
      {
        return false;
      }

      if (parentNode.IsSelected)
      {
        selectedNode.SelectedChild = parentNode;
        return true;
      }

      foreach (CategoryItem childNode in parentNode.CategoryItems)
      {
        if (this.TryGetSelectedNode(childNode, out selectedNode))
        {
          if (selectedNode.Parent == null)
          {
            selectedNode.Parent = parentNode;
          }

          return true;
        }
      }

      return false;
    }
  }
}
