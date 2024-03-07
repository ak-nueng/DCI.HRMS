using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.Security.Model;

namespace DCI.Security.Persistence
{
    public interface IModuleDao
    {
        ArrayList SelectByModuleType(ModuleType type);
        ArrayList SelectByModuleType(ModuleType type , ApplicationType applicationType);
    }
}
