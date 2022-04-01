using M242.Api.Model;
using M242.Model;
using M242.Model.Model;
using Microsoft.AspNetCore.Mvc;

namespace M242.Api.Controllers
{
    public class AuswertungenController : ControllerBase
    {
        public IM242UnitofWork UnitofWork { get; set; }
        private IConfiguration Configuration;
        public AuswertungenController(ILogger<HomeController> logger, IM242UnitofWork unitofWork, IConfiguration config) {UnitofWork = unitofWork; Configuration = config; }

        public ActionResult TreeMapNFCRegisters() { 
            var nfcs = UnitofWork.GetAll<NFCRegisters>();
            var users = UnitofWork.GetAll<User>();
            var data = nfcs.GroupBy(x => x.Nummber).Select(x => new { x = users.FirstOrDefault(y => y.NFCCardId == x.Key)?.Username ?? (x.Key + "(No User Found)"), y = x.Count() }).ToList();
            return Ok(new List<object>() { new { data = data } });
        }
        
        public ActionResult GetTodaysTempData() {
            var data = UnitofWork.GetAll<TempLogging>().Where(x => x.CreateDate.Date == DateTime.UtcNow.Date).ToList();
            return Ok(data.Select(x => new { time = x.CreateDate.ToString("t"), temp = x.Temperature, hum = x.Humidity }));
        }

        public ActionResult GetTempNearCriticalPoint() {
            var temp = UnitofWork.GetAll<TempLogging>().OrderBy(x => x.CreateDate).LastOrDefault();
            return Ok(Math.Round(((temp?.Temperature ?? 0) / double.Parse(Configuration["WebPageBlockPoint"])) * 100,2));
        }


    }
}