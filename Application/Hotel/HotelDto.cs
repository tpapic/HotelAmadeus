namespace Application.Hotel
{
    public class HotelDto
    {
        public string Name { get; set; }
        public string Rating { get; set; }
        public string Description { get; set; }
        public OfferDto ChepestOffer { get; set; }
    }

    public class OfferDto {
        public string Total { get; set; }
        public string Currency { get; set; }
    }
}