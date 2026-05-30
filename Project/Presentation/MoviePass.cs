static class MoviePass
{
    public static void Start()
    {
        List<string> fields = ["Amount of Pass points"];
        int amountOfPassPoints = 0;
        bool isValidInput = true;

        do
        {
            Dictionary<string, string> userInput = UiHelper.InputForm(
                fields, 
                isValidInput ? "How many Pass points would you like to top up?" : "Invalid input, please try again"
            );
            (bool validOrNot, int amount) = PurchaseLogic.amountOfPassPointsIsValid(userInput[fields[0]]);
            isValidInput = validOrNot;
            amountOfPassPoints = amount;

        } while (!isValidInput);

        List<string> paymentMethodsForMoviePass = [];
        foreach(string paymentMethod in PurchaseTicket.PaymentMethods)
        {
            paymentMethodsForMoviePass.Add(paymentMethod);
        }
        paymentMethodsForMoviePass.Remove("Movie pass");

        (bool completePayment, string selectedPaymentMethodString) = PurchaseTicket.Payment(paymentMethodsForMoviePass);
        if(!completePayment) Menu.Start();

        PurchaseLogic.TopUpMoviePass(amountOfPassPoints);
        UiHelper.SelectionMenu([$"Pass Points are added to your Movie pass."], "");
    }
}
