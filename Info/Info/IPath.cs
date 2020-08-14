using System;
using System.Collections.Generic;
using System.Text;

namespace Info
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
