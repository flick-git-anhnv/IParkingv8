using iParkingv8.Object.ConfigObjects.OEMConfigs;
using Kztek.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.ConfigurationManager.Views.Interfaces
{
    public interface IOEMView
    {
        OEMConfig? GetConfig();
        void SetConfig(OEMConfig? config);
    }
}
