using COMPASS.Common.Interfaces;
using System;
using System.IO;

namespace COMPASS.MacOS.Services
{
    public class EnvironmentVarsService : IEnvironmentVarsService
    {
        public string CompassDataPath
        {
            get => IEnvironmentVarsService.DefaultDataPath;

            set
            {
            }
        }
    }
}
