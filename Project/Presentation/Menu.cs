static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static string header = "Welcome to Cinema Vier! Please select an option:";
    static public void Start()
    {
        List<string> menu = ["View Movies", "Login", "Register", "Rules and Conditions", "Exit"];
        int selected = UiLib.SelectionMenu(menu, header);

        if (selected == menu.IndexOf("Login"))
        {
            UserLogin.Start();
        }
        else if (selected == menu.IndexOf("Register"))
        {
            UserRegistration.Start();
        }
        else if (selected == menu.IndexOf("View Movies"))
        {
            MoviesMenu.Start();
            PurchaseModel purchaseTicket = PurchaseTicket.Start();
        }
        else if (selected == menu.IndexOf("Rules and Conditions"))
        {
            RulesAndConditions.Start();
        }
        else if (selected == menu.IndexOf("Exit"))
        {
            Console.WriteLine("Thank you for using Cinema Vier! Goodbye!");
        }
    }
}