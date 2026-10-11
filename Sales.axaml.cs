using Avalonia.Controls;
using System.Collections.ObjectModel;

namespace Avalonia_DB_Connection;

public class Sale
{
    public int id { get; set; }
    public string productname { get; set; } = "";
    public string totalsales { get; set; } = "";
    public object rank { get; }
}

public partial class Sales : UserControl
{
    public ObservableCollection<Sale> Product { get; } = new();

    public Sales()
    {
        InitializeComponent();

        Product.Add(new Sale { id = 1, productname = "Computer Mouse", totalsales = "5",  });
        Product.Add(new Sale { id = 2, productname = "HeadSet", totalsales = "7",  });
        
        
        MyTable.ItemsSource = Product;
    }
}