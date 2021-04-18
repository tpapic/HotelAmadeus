using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Hotel
{
    public class Search
    {
        public class Query : IRequest<Result<List<HotelDto>>>
        {
            public string CityName { get; set; }
            public string? CheckInDate { get; set; }
            public string? CheckOutDate { get; set; }
            public int? RoomQuantity { get; set; } = 1;
            public int? Adults { get; set; } = 1;
            public int? Radius { get; set; } = 100;

            public override string ToString()
            {
                var _PropertyInfos = this.GetType().GetProperties();

                var sb = new StringBuilder();

                foreach (var info in _PropertyInfos)
                {
                    var value = info.GetValue(this, null) ?? "(null)";
                    sb.Append(info.Name + "-" + value.ToString());
                }

                return sb.ToString();
            }
        }

        public class Handler : IRequestHandler<Query, Result<List<HotelDto>>>
        {
            private readonly IMapper _mapper;
            private readonly IAmadeusProxy _amadeusProxy;
            public Handler(IMapper mapper, IAmadeusProxy amadeusProxy)
            {
                _amadeusProxy = amadeusProxy;
                _mapper = mapper;
            }
            public async Task<Result<List<HotelDto>>> Handle(Query request, CancellationToken cancellationToken)
            {

                var search = await _amadeusProxy.Search(request);

                var result = search.data?.Select(x =>  new HotelDto {
                    Name = x.hotel?.name,
                    Rating = x.hotel?.rating,
                    Description = x.hotel?.description?.text,
                    ChepestOffer = new OfferDto() {
                        Total = x.offers?.FirstOrDefault()?.price.total,
                        Currency = x.offers?.FirstOrDefault()?.price.currency
                    }
                }).ToList();

                return Result<List<HotelDto>>.Success(result);
            }
        }
    }
}