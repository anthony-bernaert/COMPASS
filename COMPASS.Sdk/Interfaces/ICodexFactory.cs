using COMPASS.Sdk.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.Sdk.Interfaces
{
    public interface ICodexFactory
    {
        public ICodex CreateCodex();
    }
}
