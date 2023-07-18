using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectLine.CourierManager.SuperFunction.AppSettings
{
    public class AppSetingsSigleton
    {
        public static AppSetingsSigleton? appSettingsInstance;

        private AppSetingsSigleton() { }

        public static AppSetingsSigleton GetInstance()
        {
            if (appSettingsInstance == null)
                appSettingsInstance = new AppSetingsSigleton();
            return appSettingsInstance;
        }
    }
}
