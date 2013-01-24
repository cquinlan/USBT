using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USBTCommon
{
    public interface IOperation
    {
        // Settings and timer dictionaries
        Dictionary<string, string> settings { get; set; }
        Dictionary<string, bool> timers { get; set; }

        // Run and debug methods
        Window runOp(Window hWnd);

        void checkOp();

        // Call for information about the operation.

    }
}
