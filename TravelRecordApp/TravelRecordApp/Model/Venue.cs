using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{
    /*
        public class Category
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Main
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Geocodes
        {
            public Main main { get; set; }
        }

        public class Location
        {
            public string country { get; set; }
            public string cross_street { get; set; }
            public string formatted_address { get; set; }
            public string locality { get; set; }
            public string postcode { get; set; }
            public string region { get; set; }
            public string address { get; set; }
            public string address_extended { get; set; }
        }

        public class Venue
        {
            public IList<Category> categories { get; set; }
            public int distance { get; set; }
            public Geocodes geocodes { get; set; }
            public Location location { get; set; }
            public string name { get; set; }
            public string timezone { get; set; }
        }

        public class Response
        {
            public IList<Venue> venues { get; set; }
        }

        public class VenueRoot
        {
            public Response response { get; set; }
            public static string GenreateURL(double latitude, double longitude)
            {
                string url = string.Format(Constants.VENUE_SEARCH, latitude, longitude);
                return url;
            }

        }*/

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    /*public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public Icon icon { get; set; }
    }

    public class Main
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Roof
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Geocodes
    {
        public Main main { get; set; }
        public Roof roof { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string country { get; set; }
        public string cross_street { get; set; }
        public string formatted_address { get; set; }
        public string locality { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
        public string address_extended { get; set; }
        public List<string> neighborhood { get; set; }
    }

    public class Child
    {
        public string fsq_id { get; set; }
        public string name { get; set; }
    }

    public class RelatedPlaces
    {
        public List<Child> children { get; set; }
    }

    public class Venue
    {
        public string fsq_id { get; set; }
        public List<Category> categories { get; set; }
        public List<object> chains { get; set; }
        public int distance { get; set; }
        public Geocodes geocodes { get; set; }
        public Location location { get; set; }
        public string name { get; set; }
        public RelatedPlaces related_places { get; set; }
        public string timezone { get; set; }
    }
     public class Response
    {
        public IList<Venue> venues { get; set; }
    }*/
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public Icon icon { get; set; }
    }

    public class Main
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Roof
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Geocodes
    {
        public Main main { get; set; }
        public Roof roof { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string country { get; set; }
        public string cross_street { get; set; }
        public string formatted_address { get; set; }
        public string locality { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
        public string address_extended { get; set; }
        public List<string> neighborhood { get; set; }
    }

    public class Child
    {
        public string fsq_id { get; set; }
        public string name { get; set; }
    }

    public class RelatedPlaces
    {
        public List<Child> children { get; set; }
    }

    public class Result
    {
        public string fsq_id { get; set; }
        public List<Category> categories { get; set; }
        public List<object> chains { get; set; }
        public int distance { get; set; }
        public Geocodes geocodes { get; set; }
        public Location location { get; set; }
        public string name { get; set; }
        public RelatedPlaces related_places { get; set; }
        public string timezone { get; set; }

        //public double latitude = 21.1702401;
        //       pu double longitude = 72.8310607;
        public async static Task<List<Result>> GetVenues(double latitude, double longitude)
        {
            List<Result> venues = new List<Result>();

            var url = Root1.GenreateURL(latitude, longitude);

            /*
           var client = new RestClient(url);
           RestRequest request = new RestRequest("", Method.Get);
           request.AddHeader("Accept", "application/json");
           request.AddHeader("Authorization", Constants.key);
          var response = await client.ExGetAsync(request);
          var json = JsonConvert.DeserializeObject<VenueRoot>(result);
            */


            /*HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get,
            };
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", Constants.key);
              var response = await client.SendAsync(request);    
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                venues = venueRoot.response.venues as List<Venue>;
            }*/
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "fsq3DCjjiW66poKmwL7aoRcMr3CcZiRv5FwWPh3wCR5R69o=");

            var responce = await client.GetAsync(url);
            Console.WriteLine(responce.StatusCode);
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await responce.Content.ReadAsStringAsync();
                var venueRoot = JsonConvert.DeserializeObject<Root>(json);
                venues = venueRoot.results;
            }
            return venues;
        }
    }

    public class Root
    {
        public List<Result> results { get; set; }
    }


    public class Root1
    {
        
        public static string GenreateURL(double latitude, double longitude)
        {
            string url = string.Format(Constants.VENUE_SEARCH, latitude, longitude);
            return url;
        }
    }


}
