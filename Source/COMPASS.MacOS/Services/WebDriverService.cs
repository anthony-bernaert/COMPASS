using COMPASS.Common.Services;
using COMPASS.Common.Tools;
using System.IO;

namespace COMPASS.MacOS.Services
{
    class WebDriverService : WebDriverServiceBase
    {
        protected override Browser DetectInstalledBrowser()
        {
            
            return Browser.None;
        }
        
    }
}
