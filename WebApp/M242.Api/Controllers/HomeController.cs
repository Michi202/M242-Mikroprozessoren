using M242.Api.Model;
using M242.Model;
using M242.Model.Model;
using Microsoft.AspNetCore.Mvc;

namespace M242.Api.Controllers
{
    public class HomeController : ControllerBase
    {
        public IM242UnitofWork UnitofWork { get; set; }
        private IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IM242UnitofWork unitofWork, IConfiguration config) { UnitofWork = unitofWork; Configuration = config; }

        [HttpPost]
        public ActionResult SendTemp([FromBody] IoTiKitTempModel model)
        {
            UnitofWork.Save(new TempLogging()
            {
                Humidity = model.humidity,
                Temperature = model.Temperature,
                IotikitIp = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
            });
            return Ok();
        }

        [HttpPost]
        public ActionResult SendNFC([FromBody] IoTiKitNFCModel model)
        {
            if (model?.CardId != null)
            {
                UnitofWork.Save(new NFCRegisters()
                {
                    Nummber = model.CardId.Remove(model.CardId.Length - 1)
                });
            }
            return Ok();
        }

        public ActionResult CheckTemp()
        {
            //var temps = UnitofWork.GetAll<TempLogging>().OrderBy(x => x.CreateDate).GroupBy(x => x.IotikitIp).Select(x => x.Last()).ToList();
            //var tempDiff = Math.Abs(temps[0]?.Temperature ?? 0 - temps[1]?.Temperature ?? 0);
            //var humpDiff = Math.Abs(temps[0]?.Humidity ?? 0 - temps[1]?.Humidity ?? 0);
            //var durchschnittlicheTempDerGeräte = (temps[0].Temperature + temps[1].Temperature) / 2;
            var temp = UnitofWork.GetAll<TempLogging>().OrderBy(x => x.CreateDate).LastOrDefault();
            if (temp?.Temperature > int.Parse(Configuration["WebPageBlockPoint"])) return Ok("Temp zu hoch");
            else return Ok();
        }
    }
}