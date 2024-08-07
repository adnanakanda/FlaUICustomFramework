using Castle.Core.Internal;
using System.Diagnostics;

namespace Framework.Utils
{
    public static class ProcessHelper
    {
        public static void KillListOfProcesses(string processName)
        {
            Process.GetProcessesByName(processName).ForEach(p => p.Kill());
        }
    }
}
