using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.Model
{
    public class User : Entity
    {
        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string NFCCardId { get; set; }

        public virtual int ButtonCode { get; set; }


    }
}
