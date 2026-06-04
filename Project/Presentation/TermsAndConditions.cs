public static class TermsAndConditions
{
    public static bool Start()
    {
        // show terms before payment
        List<string> menu =
        [
            "Accept terms and continue",
            "Cancel purchase"
        ];

        string header = @"
=== Terms and Conditions ===

By continuing, you agree to the cinema rules and payment conditions.

Tickets are only valid for the selected movie, date and time.
The user is responsible for entering correct information.
Food and drinks cannot be refunded after purchase.
";
        int selected = UiHelper.SelectionMenu.WriteMenu(menu, header);
        return selected == 0;
    }
}
