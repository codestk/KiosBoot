 
using KiosBoot.ViewModels;
 
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KiosBoot
{
   public class MyDataTemplateSelector: DataTemplateSelector
    {
          public DataTemplate MainMenuViewTemplate { get; set; }
    public DataTemplate StartMenuTemplate { get; set; }
  
    protected override DataTemplate SelectTemplateCore(object item)
    {
        if (item is GameViewModel)
            return MainMenuViewTemplate;
        if (item is StartMenuViewModel)
            return StartMenuTemplate;
  
        return base.SelectTemplateCore(item);
    }
 
    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
    {
        return SelectTemplateCore(item);
    }
    }
}
