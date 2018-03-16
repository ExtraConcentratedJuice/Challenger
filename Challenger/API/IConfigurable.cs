using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challenger.API
{
    public interface IConfigurable
    {
        Dictionary<string, object> ConfigurationValues();
    }
}
