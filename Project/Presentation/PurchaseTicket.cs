static class PurchaseTicket
{
    static List<string> TimeMenu { get; } = new() {"9:30", "11:30", "13:30", "15:30", "17:30", "19:30", "21:30", "23:30"};
    static List<string> DateMenu { get; } = [];
    static List<string> PaymentMethods { get; } = new() {"Credit Card"};
    static List<string> CreditCardInput = new()
    {
        "Cardholder name",
        "Card number (13-19 digits, for example:4111 1111 1111 1111)",
        "Expiration date (MM/YY)",
        "CVC/CVV code (3-4 digits)"
    };
    // static List<string> IBANInput = new()
    // {
    //     "Cardholder name",
    //     "IBAN number (for example: NL12 ABNA 1234 5678 90)",
    // };

    public static PurchaseModel Start()
    {
        SetUp_dateMenu();
        string selectedDate = DateMenu[UiLib.SelectionMenu(DateMenu, "Pick a date")];
        string today = DateTime.Today.AddDays(0).ToString("dd-MM-yyyy");

        string selectedTime;
        if(selectedDate == today)
        {
            List<string> customTimeMenu = CustomizeTimeMenu();
            selectedTime = TimeMenu[UiLib.SelectionMenu(customTimeMenu, "Pick a time")];
        }
        else
        {
            selectedTime = TimeMenu[UiLib.SelectionMenu(TimeMenu, "Pick a time")];
        }

        DateTime selectedDateTime = DateTime.ParseExact($"{selectedDate} {selectedTime}", "dd-MM-yyyy H:mm", null);

        // added fixed ticket price for now
        decimal ticketTotal = 12.00m;

        // added list to store selected food and drink items
        List<OrderItemModel> orderedMenuItems = new List<OrderItemModel>();

        // added ask user if they want to add snacks or drinks
        List<string> orderMenuChoices = new()
        {
            "Continue without food and drinks",
            "Add food and drinks"
        };

        // added menu selection before payment
        int selectedOrderChoice = UiLib.SelectionMenu(orderMenuChoices, "Do you want to add snacks or drinks?");

        // added if user chooses menu items, open food and drink menu
        if (selectedOrderChoice == 1)
        {
            orderedMenuItems = FoodAndDrinkMenu.ShowFoodAndDrinkMenu();
        }

        // added create menu logic to calculate totals
        MenuLogic menuLogic = new MenuLogic();

        // added calculate food and drink total
        decimal menuTotal = menuLogic.CalculateMenuTotal(orderedMenuItems);

        // added calculate final total
        decimal finalTotal = ticketTotal + menuTotal;

        // added show booking summary before payment
        ShowBookingSummary(ticketTotal, orderedMenuItems, menuTotal, finalTotal);

        string selectedPaymentMethod = PaymentMethods[UiLib.SelectionMenu(PaymentMethods, "How do you want to pay?")];
        string invalidInputs = "";

        if(selectedPaymentMethod == "Credit Card")
        {
            do
            {
                if(invalidInputs != "")
                {
                    var creditCardInfo = UiLib.InputForm(CreditCardInput, $"Invalid input:{invalidInputs}please try again");
                    invalidInputs = PurchaseLogic.CreditCardCheck(creditCardInfo);
                }
                else
                {
                    var creditCardInfo = UiLib.InputForm(CreditCardInput, "Please fill in the payment information");
                    invalidInputs = PurchaseLogic.CreditCardCheck(creditCardInfo);
                }
            } while(invalidInputs != "");
        }
        // else if(selectedPaymentMethod == "IBAN")
        // {
        //     do
        //     {
        //         if(invalidInputs != "")
        //         {
        //             var iBANInfo = UiLib.InputForm(IBANInput, $"Invalid input:{invalidInputs}please try again");
        //             invalidInputs = PurchaseLogic.IBANCheck(iBANInfo);
        //         }
        //         else
        //         {
        //             var iBANInfo = UiLib.InputForm(IBANInput, "Please fill in the payment information");
        //             invalidInputs = PurchaseLogic.IBANCheck(iBANInfo);
        //         }
        //     } while(invalidInputs != "");
        // }

        UiLib.SelectionMenu([$"Payment successful. Reservation number: {PurchaseLogic.GenerateReservationNumber()}"], "");
        return new PurchaseModel(
            null,
            selectedDateTime,
            selectedPaymentMethod,
            orderedMenuItems,
            ticketTotal,
            menuTotal,
            finalTotal
        );
    }

    static void SetUp_dateMenu()
    {
        for (int i = 0; i < 14; i ++)
        {
            DateMenu.Add(DateTime.Today.AddDays(i).ToString("dd-MM-yyyy"));
        }
    }

    static List<string> CustomizeTimeMenu()
    {
        List<string> newTimeMenu = [];
        TimeSpan now = DateTime.Now.TimeOfDay;
        TimeSpan nowPlus20 = now.Add(TimeSpan.FromMinutes(20));

        foreach(string time in TimeMenu)
        {
            TimeSpan timeToCompare = TimeSpan.Parse(time);
            if(nowPlus20 <= timeToCompare)
            {
                newTimeMenu.Add(timeToCompare.ToString(@"h\:mm"));
            }
        }
        return newTimeMenu;
    }

    // added method to show full booking summary before payment
    static void ShowBookingSummary(
        decimal ticketTotal,
        List<OrderItemModel> orderedMenuItems,
        decimal menuTotal,
        decimal finalTotal)
    {
        Console.Clear();
        Console.WriteLine("Booking Summary");
        Console.WriteLine();

        Console.WriteLine($"Ticket total: €{ticketTotal:0.00}");
        Console.WriteLine();

        if (orderedMenuItems.Count > 0)
        {
            Console.WriteLine("Food and drink items:");
            Console.WriteLine();

            foreach (OrderItemModel item in orderedMenuItems)
            {
                Console.WriteLine($"Item name: {item.Name}");
                Console.WriteLine($"Quantity: {item.Quantity}");
                Console.WriteLine($"Price per item: €{item.PricePerItem:0.00}");
                Console.WriteLine($"Subtotal: €{item.SubTotal:0.00}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No food or drinks selected.");
            Console.WriteLine();
        }

        Console.WriteLine($"Food and drink total: €{menuTotal:0.00}");
        Console.WriteLine($"Final total: €{finalTotal:0.00}");
        Console.WriteLine();

        UiLib.HoldUser();
    }
}