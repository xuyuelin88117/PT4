//--------------------------------------------------//
//            WARNING! Altering this file           //
//  may cause Programming Taskbook to malfunction.  //
//--------------------------------------------------//
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Reflection;
using System.IO;

namespace PT4Main
{
    class PTMain
    {
        public static int Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // to be able to use the setting Encoding.GetEncoding(1251)
            for (int i = 0; i < 10; i++)
            {
                PT4.TaskMaker.StartCS();
                try
                {
                    PT4Tasks.MyTask.Solve();
                }
                catch (Exception ex)
                {
                    PT4.TaskMaker.RaisePT(ex);
                }
                int res = PT4.TaskMaker.FreePT();
                if (res == 1)
                    break;
            }
            return 0;
        }
    }
}

