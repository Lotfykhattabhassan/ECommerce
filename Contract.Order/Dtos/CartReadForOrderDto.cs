namespace Contract.Order.Dtos
{
    public record CartItemForOrderDto(
        Guid ProductId,
        int Quantity,
        decimal UnitPrice);

    public record CartReadForOrderDto(
        List<CartItemForOrderDto> Items);
}