using System;

namespace DmarcRua
{
    [Flags]
    public enum RequestedReportingPolicy
    {
        All = 0,
        Any = 1,
        Dkim = 2,
        Spf = 4
    }
}