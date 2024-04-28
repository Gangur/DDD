namespace Presentation
{
    public record OrderDto(
        Guid Id, 
        Guid CustomerId,
        List<LineItemDto> LineItems);
}
