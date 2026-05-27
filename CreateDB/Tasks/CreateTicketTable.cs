public static class CreateTicketTable
{
    public static Task Execute()
    {
        TicketAccess ticket = new();
        ticket.CreateTable();

        return Task.CompletedTask;
    }
}