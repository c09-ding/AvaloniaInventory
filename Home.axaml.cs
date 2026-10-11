using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace Avalonia_DB_Connection;

public class SoldItemModel
{
    public int id { get; set; }
    public string rank { get; set; } = "";
    public string productname { get; set; } = "";
    public string Itemsold { get; set; } = "";
    public string item { get; set; } = " ";
    public string status { get; set; } = "";
}


public class LowStock
{
    public int id { get; set; }
    public string item { get; set; } = "";
    public int Quantity { get; set; }
    public string status { get; set; } = "";
}

public partial class Home : UserControl
{
    public ObservableCollection<SoldItemModel> SoldItem { get; } = new();
    public ObservableCollection<LowStock> ItemStock { get; } = new();

    public Home()
    {
        InitializeComponent();

        DataContext = this;

        SoldItem.Add(new SoldItemModel
        {
            id = 1,
            rank = "1st",
            productname = "Mouse",
            Itemsold= "10"
        });

        SoldItem.Add(new SoldItemModel
        {
            id = 2,
            rank = "2nd",
            productname = "Headset",
            Itemsold = "5"
        });
        
        SoldItem.Add(new SoldItemModel
        {
            id = 3,
            rank = "3rd",
            productname = "Kwyboard",
            Itemsold = "3"
        });


    
      
        ItemStock.Add(new LowStock
        {
            id = 1,
            item = "Mouse",
            Quantity = 15,
            status = "Low Stock"
        });

        ItemStock.Add(new LowStock
        {
            id = 2,
            item = "Headset",
            Quantity = 13,
            status = "Low Stock" 
        });

        ItemStock.Add(new LowStock
        {
            id = 3,
            item = "Monitor",
            Quantity = 10,
            status = "Critical"
        });


    }
}


