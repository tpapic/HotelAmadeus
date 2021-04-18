using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Application.Hotel;
using Application.Interfaces;
using Newtonsoft.Json;

namespace Infrastructure.Amadeus
{
    public class Amadeus : IAmadeus
    {
        private readonly IHttpClientFactory _clientFactory;
        public Amadeus(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Response> Search(Search.Query query)
        {
            // var url = "https://test.api.amadeus.com/v2/shopping/hotel-offers";
            var url = getQueryParams(query);
            var request = new HttpRequestMessage(HttpMethod.Get,
            // "https://test.api.amadeus.com/v2/shopping/hotel-offers?cityCode=ZAG&checkInDate=2021-04-22&checkOutDate=2021-04-29&roomQuantity=1&adults=1&radius=100&radiusUnit=KM&paymentPolicy=NONE&includeClosed=false&bestRateOnly=true&view=FULL&sort=NONE");
            url);

            request.Headers.Add("Authorization", "Bearer OLB7JT9SOLVyAn26W6KWcVQluyVL");

            // var params = getQueryParams(query);

            // request.Content = new FormUrlEncodedContent(params);

            var b = request.ToString();

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var resultJson = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Response>(resultJson);

            return result;
        }

        private string getQueryParams(Search.Query query)
        {
            var url = "https://test.api.amadeus.com/v2/shopping/hotel-offers";
            var builder = new UriBuilder(url); 

            var queryUrl = HttpUtility.ParseQueryString(builder.Query);
            queryUrl["cityCode"] = query.CityName;

            if(query.CheckInDate != null) {
                queryUrl["checkInDate"] = query.CheckInDate;
            }

            if (query.CheckOutDate != null)
            {
                queryUrl["checkOutDate"] = query.CheckOutDate;
            }

            if (query.RoomQuantity != null)
            {
                queryUrl["roomQuantity"] = query.RoomQuantity.ToString();
            }

            if (query.Adults != null)
            {
                queryUrl["adults"] = query.Adults.ToString();
            }

            if (query.Radius != null)
            {
                queryUrl["radius"] = query.Radius.ToString();
            }

            queryUrl["radiusUnit"] = "KM";
            queryUrl["bestRateOnly"] = "true";
            queryUrl["includeClosed"] = "true";
            

            builder.Query = queryUrl.ToString();

            return builder.ToString();

            // TODO
            // return new Dictionary<string, string>
            // {
            //     { "cityCode", query.CityName },
            //     { "checkInDate", query.CheckInDate },
            //     { "checkOutDate", query.CheckOutDate },
            //     { "roomQuantity", query.RoomQuantity.ToString() },
            //     { "adults", query.Adults.ToString() },
            //     { "radius", query.Radius.ToString() }
            // };
        }
    }
}