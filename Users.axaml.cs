using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySqlConnector;

namespace Avalonia_DB_Connection;

public partial class Users : UserControl
{
    
    //Declare our Model for TableView use
    public ObservableCollection<Models> UserData { get; set; } = [];
    
    public Users()
    {
        InitializeComponent();
        //Loads our data to the table
        
        Loaded += Users_Loaded;
    }

    private void Users_Loaded(object? sender, RoutedEventArgs e)
    {
        DataContext = this;
        LoadData();
    }
    

    private void LoadData()
    {
        //Connection String
        string connString = "Server=localhost;Database=inventory;Uid=root;Pwd=;";
        //SQL Query
        string query = "SELECT * FROM registration";
        try
        {
            using MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();
            using MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            //Looping statement until reader has something to read
            while (reader.Read())
            {
                //Adds data to our model per loop
                UserData.Add(new Models
                {
                    id = reader.GetInt32("ID"),
                    firstname = reader.GetString("First_Name"),
                    lastname = reader.GetString("Last_Name"),
                    email = reader.GetString("Email_Address"),
                    username = reader.GetString("Username"),
                    password = reader.GetString("Password")
                });
            }

        }
        catch (Exception ex)
        {
            //SubHeader.Text = ex.Message;
           
            //Get the parent window which is MainWindow
            var vroot = TopLevel.GetTopLevel(this) as Window; 
            var msgbox = new MessageBox(vroot);
            //Set MessageBox title as Exit to trigger its Loaded Event
            msgbox.Title = "Notice";
            msgbox.Message.Text = ex.Message;
        
            //Checks if parent window is indeed open
            if (vroot != null)
            {
                //Make the window show as a Dialog Box and behave like one
                msgbox.ShowDialog(vroot);
            }
        }

    }

    public void UserTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //Enable textboxes
        Tb_Fname.IsEnabled = true;
        Tb_Lname.IsEnabled = true;
        Tb_Email.IsEnabled = true;
        Tb_Username.IsEnabled = true;
        Tb_Password.IsEnabled = true;
        Tb_Retype.IsEnabled = true;
        BtnConfirm.IsEnabled=true;
        BtnCancel.IsEnabled=true;
    }

    public void Cancel(object sender, RoutedEventArgs e)
    {
        //Clears tableview data
        if (UserTable.ItemsSource is IList list)
        {
            list.Clear();
        }
        LoadData();
        //Disable textboxes and buttons
        Tb_Fname.IsEnabled = false;
        Tb_Lname.IsEnabled = false;
        Tb_Email.IsEnabled = false;
        Tb_Username.IsEnabled = false;
        Tb_Password.IsEnabled = false;
        Tb_Retype.IsEnabled = false;
        BtnConfirm.IsEnabled=false;
        BtnCancel.IsEnabled=false;

        //Clears textboxes
        Tb_Fname.Text = "";
        Tb_Lname.Text = "";
        Tb_Email.Text = "";
        Tb_Username.Text = "";
        Tb_Password.Text = "";
        Tb_Retype.Text = "";
    }
    


    public void Update(object? sender, RoutedEventArgs e)
    {
        if (Tb_Password.Text == Tb_Retype.Text)
        {
            //Connection String
            string connString = "Server=localhost;Database=inventory;Uid=root;Pwd=;";

            //SQL Query using parameters to prevent SQL injection
            string query = "UPDATE registration " +
                           "SET First_Name = '" + Tb_Fname.Text + "', " +
                           "First_Name = '" + Tb_Lname.Text + "', " +
                           "Email_Address = '" + Tb_Email.Text + "', " +
                           "Username = '" + Tb_Username.Text + "', " +
                           "Password= '" + Tb_Password.Text + "' " +
                           " WHERE ID = " + Convert.ToInt32(Tb_ID.Text) + " ;";
            try
            {
                using MySqlConnection connection = new MySqlConnection(connString);
                connection.Open();
                using MySqlCommand command = new MySqlCommand(query, connection);

                // Bind your model values to the SQL parameters
                /* This doesn't work properly but this seems like a good idea 
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstname", Convert.ToString(Tb_Fname.Text));
                command.Parameters.AddWithValue("@lastname", Convert.ToString(Tb_Lname.Text));
                command.Parameters.AddWithValue("@email", Convert.ToString(Tb_Email.Text));
                command.Parameters.AddWithValue("@username", Convert.ToString(Tb_Username.Text));
                command.Parameters.AddWithValue("@password", Convert.ToString(Tb_Password.Text));
                */

                // ExecuteNonQuery is used for INSERT, UPDATE, and DELETE statements
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    //SubHeader.Text = ex.Message;
                    
                    //Get the parent window which is MainWindow 
                    var vroot = TopLevel.GetTopLevel(this) as Window;
                    var msgbox = new MessageBox(vroot);
                    //Set MessageBox title as Exit to trigger its Loaded Event
                    msgbox.Title = "Notice";
                    msgbox.Message.Text = "Updated Successfully!";

                    //Checks if parent window is indeed open
                    if (vroot != null)
                    {
                        //Make the window show as a Dialog Box and behave like one
                        msgbox.ShowDialog(vroot);
                    }
                    //Clears tableview data
                    if (UserTable.ItemsSource is IList list)
                    {
                        list.Clear();
                    }
                    LoadData();
                    //Disable textboxes
                    Tb_Fname.IsEnabled = false;
                    Tb_Lname.IsEnabled = false;
                    Tb_Email.IsEnabled = false;
                    Tb_Username.IsEnabled = false;
                    Tb_Password.IsEnabled = false;
                    Tb_Retype.IsEnabled = false;
                    BtnConfirm.IsEnabled=false;
                    BtnCancel.IsEnabled=false;
                    //Clears textboxes
                    Tb_Fname.Text = "";
                    Tb_Lname.Text = "";
                    Tb_Email.Text = "";
                    Tb_Username.Text = "";
                    Tb_Password.Text = "";
                    Tb_Retype.Text = "";
                }
            }
            catch (Exception ex)
            {
                //SubHeader.Text = ex.Message;
                //Get the parent window which is MainWindow
                var vroot = TopLevel.GetTopLevel(this) as Window;
                var msgbox = new MessageBox(vroot);
                //Set MessageBox title as Exit to trigger its Loaded Event
                msgbox.Title = "Notice";
                msgbox.Message.Text = ex.Message;

                //Checks if parent window is indeed open
                if (vroot != null)
                {
                    //Make the window show as a Dialog Box and behave like one
                    msgbox.ShowDialog(vroot);
                }
            }
        }
        else
        {
            //SubHeader.Text = ex.Message;
            //Get the parent window which is MainWindow
            var vroot = TopLevel.GetTopLevel(this) as Window;
            var msgbox = new MessageBox(vroot);
            //Set MessageBox title as Exit to trigger its Loaded Event
            msgbox.Title = "Notice";
            msgbox.Message.Text = "Passwords don't match!";

            //Checks if parent window is indeed open
            if (vroot != null)
            {
                //Make the window show as a Dialog Box and behave like one
                msgbox.ShowDialog(vroot);
            }
        }
    }
}