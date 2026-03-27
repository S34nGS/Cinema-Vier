static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static string header = "Welcome to Cinema Vier! Please select an option:";
    static public void Start()
    {
        List<string> menu = ["View Movies", "Login", "Register", "Exit"];
        int selected = UiLib.SelectionMenu(menu, header);

        if (selected == menu.IndexOf("Login"))
        {
            UserLogin.Start();
        }
        // needs to be added in the future when we have the user registration page created
        // else if (selected == menu.IndexOf("Register"))
        // {
        //     UserRegister.Start();
        // }
        // else if (selected == menu.IndexOf("View Movies"))
        // {
        //     MovieMenu.Start();
        // }
        else if (selected == menu.IndexOf("Exit"))
        {
            Console.WriteLine("Thank you for using Cinema Vier! Goodbye!");
        }
    }
}