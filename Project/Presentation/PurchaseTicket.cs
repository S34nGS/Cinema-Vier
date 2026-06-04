// TODO: rewrite this entire file
public static class PurchaseTicket
{
    public static List<string> DateMenu { get; } = [];
    public static List<string> TimeMenu { get; } = [];
    private static List<TimetableModel> CurrentTimetables = [];

    public static TicketModel? Start(MovieModel movie, AccountModel? customer = null)
    {
        // reset date menu
        DateMenu.Clear();
        SetUpDateMenu(movie);

        if (DateMenu.Count == 0)
        {
            int dates = UiHelper.SelectionMenu.WriteMenu(
                ["No available dates."],
                "Pick a date",
                true
            );

            if (dates == 0)
            {
                return null;
            }
        }

        int selectedDate = UiHelper.SelectionMenu.WriteMenu(DateMenu, "Pick a date");
        if (selectedDate == -1)
        {
            return null;
        }

        string selectedDateString = DateMenu[selectedDate];

        // reset time menu
        TimeMenu.Clear();
        SetUpTimeMenu(movie, selectedDateString);

        int selectedTime = UiHelper.SelectionMenu.WriteMenu(TimeMenu, "Pick a time");

        if (selectedTime == -1)
        {
            return null;
        }

        if (AccountsLogic.CurrentAccount == null && customer == null)
        {
            UserLogin.Start();
        }

        SeatSelection seatSelection = new();

        TimetableModel selectedTimetable = CurrentTimetables[selectedTime];
        List<SeatModel> selectedSeats = seatSelection.Start(selectedTimetable);

        string dateTimeString = $"{selectedDateString} {TimeMenu[selectedTime].Substring(0, 5)}";
        DateTime convertedDateTime = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", null);

        // ticket price for summary
        double ticketTotal = 0.0;
        selectedSeats.ForEach((seat) =>
        {
            ticketTotal += 12;

            if (seat.SeatPriority == 2)
            {
                ticketTotal += 3;
            }

            if (seat.SeatPriority == 3)
            {
                ticketTotal += 6;
            }
        });

        // selected menu items
        List<OrderItemModel> orderedMenuItems = [];

        // ask if user wants food or drinks before the movie
        List<string> orderMenuChoices =
        [
            "Continue without food and drinks before the movie",
            "Add food and drinks before the movie"
        ];

        int selectedOrderChoice = UiHelper.SelectionMenu.WriteMenu(orderMenuChoices, "Do you want to add snacks or drinks before the movie?");
        if (selectedOrderChoice == 1)
        {
            orderedMenuItems = FoodAndDrinkMenu.ShowFoodAndDrinkMenu();
        }

        // add free birthday popcorn gift if available
        if (AccountsLogic.CurrentAccount != null && AccountsLogic.CanUseFreePopcornGift(AccountsLogic.CurrentAccount))
        {
            // add free popcorn as birthday gift
            OrderItemModel freePopcornGift = new(
                0,
                "🎁 Birthday gift: Free popcorn",
                0.00,
                1
            );

            orderedMenuItems.Add(freePopcornGift);

            AccountsLogic accountsLogic = new();
            accountsLogic.UseFreePopcornGift(AccountsLogic.CurrentAccount);

            UiHelper.HoldUser("Happy birthday! A free popcorn gift has been added to your order.");
        }

        // selected lounge pre-order drinks
        List<OrderItemModel> loungePreOrderItems = [];

        // ask if user wants lounge pre-order drinks before the movie
        List<string> loungePreOrderChoices =
        [
            "Continue without lounge drink pre-order before the movie",
            "Add lounge drink pre-order before the movie"
        ];

        int selectedLoungePreOrderChoice = UiHelper.SelectionMenu.WriteMenu(
            loungePreOrderChoices,
            "Do you want to pre-order drinks from the lounge/bar before the movie?"
        );

        if (selectedLoungePreOrderChoice == 1)
        {
            // show only drinks for lounge pre-order
            MenuLogic loungeMenuLogic = new();
            loungePreOrderItems = FoodAndDrinkMenu.ShowOnlyDrinksMenu(loungeMenuLogic);
        }

        // calculate totals
        MenuLogic menuLogic = new();
        double menuTotal = menuLogic.CalculateMenuTotal(orderedMenuItems);

        // calculate lounge pre-order total
        double loungePreOrderTotal = menuLogic.CalculateMenuTotal(loungePreOrderItems);

        // calculate final total with lounge pre-order
        double finalTotal = PurchaseLogic.CalculateFullTotal(ticketTotal, menuTotal, loungePreOrderTotal);

        // show summary before payment
        BookingSummary.Start(
            ticketTotal,
            orderedMenuItems,
            menuTotal,
            loungePreOrderItems,
            loungePreOrderTotal,
            finalTotal,
            selectedSeats
        );

        // user must accept T&C
        if (!TermsAndConditions.Start())
        {
            UiHelper.HoldUser("Purchase cancelled. You must accept the terms and conditions before payment.");
            return null;
        }

        ReservationsLogic.CreateReservation(new ReservationModel(-1, AccountsLogic.CurrentAccount!.Id, convertedDateTime.ConvertDateToUnixTime(), (double)finalTotal, selectedTimetable.Id, selectedSeats));

        string selectedPaymentMethod = PaymentInformation.Start();

        return new TicketModel(-1, -1, convertedDateTime, selectedPaymentMethod);
    }

    public static void SetUpDateMenu(MovieModel movie)
    {
        // get all timetables for movie
        List<TimetableModel> timetables = TimetablesLogic.GetTimeTablesByMovieId(movie.Id);

        foreach (TimetableModel timetable in timetables)
        {
            if (
                timetable.StartTime > DateTime.Now.ConvertDateToUnixTime() &&
                timetable.StartTime < DateTime.Now.AddDays(14).ConvertDateToUnixTime()
            )
            {
                string date = TimeLogic.ConvertDateString(
                    timetable.StartTime.ConvertUnixTimeToDateTime(),
                    "dd-MM-yyyy"
                );

                if (DateMenu.Contains(date) == false)
                {
                    DateMenu.Add(date);
                }
            }
        }
    }

    private static void SetUpTimeMenu(MovieModel movie, string dateString)
    {
        // get all times for selected date
        List<TimetableModel> timetables = TimetablesLogic.GetTimeTablesByMovieId(movie.Id);

        CurrentTimetables.Clear();

        foreach (TimetableModel timetable in timetables)
        {
            if (dateString == timetable.StartTime.ConvertUnixTimeToDateTime().ConvertDateString("dd-MM-yyyy"))
            {
                DateTime now = DateTime.Now;

                if (TimeLogic.ConvertUnixTimeToDateTime(timetable.StartTime) > now)
                {
                    CurrentTimetables.Add(timetable);
                    TimeMenu.Add(
                        $"{timetable.StartTime.ConvertUnixTimeToDateTime().ConvertDateString("HH:mm")} {RoomsLogic.GetRoomById(timetable.RoomId).ScreenType}"
                    );
                }
            }
        }
    }
}
