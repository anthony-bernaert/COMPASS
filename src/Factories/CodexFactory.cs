using COMPASS.Models;
using COMPASS.Sdk.Interfaces;
using COMPASS.Sdk.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.Factories
{
    public class CodexFactory : ICodexFactory
    {
        private readonly CodexCollection _targetCollection;

        public CodexFactory(CodexCollection targetCollection)
        {
            _targetCollection = targetCollection;
        }

        public ICodex CreateCodex()
        {
            return new Codex(_targetCollection);
        }
    }
}
