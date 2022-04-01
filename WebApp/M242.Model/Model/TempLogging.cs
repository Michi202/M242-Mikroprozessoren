using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.Model
{
    public class TempLogging : Entity
    {
        public virtual double Temperature { get; set; }
        public virtual double Humidity { get; set; }

        public virtual string IotikitIp { get; set; }
    }
}