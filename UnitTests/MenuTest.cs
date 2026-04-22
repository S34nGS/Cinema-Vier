namespace UnitTests;

[TestClass]
public sealed class MenuTests
{
    [TestMethod]
    public void AddItem_ValidQuantity()
    {
        // arrange
        MenuLogic l = new();
        List<OrderItemModel> order = new();

        // act
        l.AddItemToOrder(order, 1, 2);

        // assert
        Assert.AreEqual(1, order.Count);
        Assert.AreEqual(2, order[0].Quantity);
    }

    [TestMethod]
    public void AddItem_InvalidQuantity()
    {
        // arrange
        MenuLogic l = new();
        List<OrderItemModel> order = new();

        // act + assert
        Assert.ThrowsException<Exception>(() =>
        {
            l.AddItemToOrder(order, 1, 0);
        });
    }

    [TestMethod]
    public void CalculateTotal_ReturnsValue()
    {
        // arrange
        MenuLogic l = new();
        List<OrderItemModel> order = new();

        l.AddItemToOrder(order, 1, 1);
        l.AddItemToOrder(order, 2, 1);

        // act
        decimal total = l.CalculateMenuTotal(order);

        // assert
        Assert.IsTrue(total > 0);
    }

    [TestMethod]
    public void RemoveItem_RemovesCorrectItem()
    {
        // arrange
        MenuLogic l = new();
        List<OrderItemModel> order = new();

        l.AddItemToOrder(order, 1, 1);

        // act
        l.RemoveItemFromOrder(order, order[0].MenuItemId);

        // assert
        Assert.AreEqual(0, order.Count);
    }

    [TestMethod]
    public void UpdateQuantity_ChangesValue()
    {
        // arrange
        MenuLogic l = new();
        List<OrderItemModel> order = new();

        l.AddItemToOrder(order, 1, 1);

        // act
        l.UpdateItemQuantity(order, order[0].MenuItemId, 5);

        // assert
        Assert.AreEqual(5, order[0].Quantity);
    }
}