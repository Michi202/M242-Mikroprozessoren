using M242.Api.Model.Authentication;
using M242.Model;
using M242.Model.Model;
using Microsoft.AspNetCore.Mvc;

namespace M242.Api.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        public IM242UnitofWork UnitofWork { get; set; }
        public AuthenticationController(ILogger<HomeController> logger, IM242UnitofWork unitofWork) { UnitofWork = unitofWork; }

        [HttpPost]
        public ActionResult Login([FromBody] LoginModel model)
        {
            var user = UnitofWork.GetAll<User>().FirstOrDefault(x => x.Username == model.Email && x.Password == model.Password);
            if (user == null) return NotFound();
            return Ok(new { id = user.Id, date = DateTime.Now });
        }

        public ActionResult NFCLogin(long id, string date)
        {
            var logindate = DateTime.Parse(date);
            var user = UnitofWork.Get<User>(id);
            if (user == null) return NotFound();
            var nfc = UnitofWork.GetAll<NFCRegisters>().Where(x => x.CreateDate > logindate).OrderBy(x => x.CreateDate).LastOrDefault();
            if (nfc == null) return Ok(false);

            if (user.NFCCardId == nfc.Nummber) return Ok(true);
            else return Ok(false);
        }


    }
}