using System;
using System.Web.Http;
using CSC_Assignment2.GlobalWeatherService;
using CSC_Assignment2.Models;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace CSC_Assignment2.Controllers
{
    [AllowAnonymous]
    public class WeatherController : ApiController
    {
        //[HttpPost]
        //[Route("api/v1/GetWeatherByLocation")]
        //public IHttpActionResult GetWeatherByLocation([FromBody] dynamic geo)
        //{
        //    double lat = double.Parse((String)geo.lat);
        //    double lng = double.Parse((String)geo.lng);
        //    return Ok(GeonamesWeather.GetWeatherByLocation(lat,lng));


        //}

        //[HttpPost]
        //[Route("api/v1/GetWeatherByLocationXML")]
        //public IHttpActionResult GetWeatherByLocationXML([FromBody] dynamic geo)
        //{
        //    double lat = double.Parse((String)geo.lat);
        //    double lng = double.Parse((String)geo.lng);
        //    return Ok(GeonamesWeather.GetWeatherByLocationXML(lat, lng));


        //}

        [HttpGet]
        [Route("api/v1/GetWeatherByCity/{cityName}/{countryName}")]
        public IHttpActionResult GetWeatherByCity(string cityName, string countryName)
        {
            GlobalWeatherSoapClient client = new GlobalWeatherSoapClient();
            string xml = client.GetWeather(cityName, countryName);

            XmlSerializer serializer = new XmlSerializer(typeof(CurrentWeather));
            CurrentWeather currentWeather;
            using (TextReader reader = new StringReader(xml))
            {
                currentWeather = (CurrentWeather)serializer.Deserialize(reader);
            }

            return Json(currentWeather);
        }
    }
}
