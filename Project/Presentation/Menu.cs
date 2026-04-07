static class Menu
{
    static string header = "Welcome to Cinema Vier! Please select an option:";

    static public void Start()
    {
        List<string> menu = ["View Movies", "Login", "Register", "Create Reservation", "Exit"];
        int selected = UiLib.SelectionMenu(menu, header);

        if (selected == menu.IndexOf("Login"))
        {
            UserLogin.Start();
        }
        else if (selected == menu.IndexOf("Register"))
        {
            UserRegistration.Start();
        }
        else if (selected == menu.IndexOf("Create Reservation"))
        {
            Reservation.Start();
        }
        else if (selected == menu.IndexOf("Exit"))
        {
            Console.WriteLine("Thank you for using Cinema Vier! Goodbye!");
        }
    }
}