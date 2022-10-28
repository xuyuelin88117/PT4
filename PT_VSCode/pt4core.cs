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


namespace PT4
{
    public sealed class Node : IDisposable
    {
        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "nodenew")]
        static extern void nodenew(out int n, int data,
            int next, int prev, int left, int right, int parent);
        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "nodegetp")]
        public static extern void nodegetp(out int n);
        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "nodegetf")]
        static extern void nodegetf(int n, int fn, out int f, out int err);
        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "nodesetf")]
        static extern void nodesetf(int n, int fn, int f, out int err);
        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "nodedispose")]
        static extern void nodedispose(int n, out int err);

        static Dictionary<int, Node> nodes = new Dictionary<int, Node>();

        public int a;
        bool disposed;

        Node(int addr, int none)
        {
            a = addr;
            disposed = false;
            nodes[a] = this;
        }

        static int NodeToInt(Node node)
        {
            if (node == null)
                return 0;
            return node.a;
        }

        public static Node IntToNode(int a)
        {
            if (a == 0)
                return null;
            if (nodes.ContainsKey(a))
                return nodes[a];
            return new Node(a, 0);
        }

        Node(int data, Node next, Node prev, Node left,
                Node right, Node parent)
        {
            int n = 0;
            nodenew(out n, data, NodeToInt(next), NodeToInt(prev),
                    NodeToInt(left), NodeToInt(right), NodeToInt(parent));
            a = n;
            disposed = false;
            nodes[a] = this;
        }

        public Node()
            : this(0, null, null, null, null, null) { }

        public Node(int data)
            : this(data, null, null, null, null, null) { }

        public Node(int data, Node next)
            : this(data, next, null, null, null, null) { }

        public Node(int data, Node next, Node prev)
            : this(data, next, prev, null, null, null) { }

        public Node(Node left, Node right, int data)
            : this(data, null, null, left, right, null) { }

        public Node(Node left, Node right, int data, Node parent)
           : this(data, null, null, left, right, parent) { }

        public int Data
        {
            get
            {
                if (disposed)
                    throw new ObjectDisposedException("cannot access a disposed Node");
                int val, err;
                nodegetf(a, 0, out val, out err);
                if (err != 0)
                    throw new ArgumentException("cannot get the Data property");
                return val;
            }
            set
            {
                if (disposed)
                    throw new ObjectDisposedException("cannot access a disposed Node");
                int err;
                nodesetf(a, 0, value, out err);
                if (err != 0)
                    throw new ArgumentException("cannot set the Data property");
            }
        }

        Node getNodeProp(int n, string name)
        {
            if (disposed)
                throw new ObjectDisposedException("cannot access a disposed Node");
            int val, err;
            nodegetf(a, n, out val, out err);
            if (err != 0)
                throw new ArgumentException("cannot get the " + name + " property");
            return IntToNode(val);
        }

        void setNodeProp(int n, string name, Node value)
        {
            if (disposed)
                throw new ObjectDisposedException("cannot access a disposed Node");
            int err;
            nodesetf(a, n, NodeToInt(value), out err);
            if (err != 0)
                throw new ArgumentException("cannot set the " + name + " property");
        }

        public Node Next
        {
            get { return getNodeProp(1, "Next"); }
            set { setNodeProp(1, "Next", value); }
        }

        public Node Prev
        {
            get { return getNodeProp(2, "Prev"); }
            set { setNodeProp(2, "Prev", value); }
        }

        public Node Left
        {
            get { return getNodeProp(3, "Left"); }
            set { setNodeProp(3, "Left", value); }
        }

        public Node Right
        {
            get { return getNodeProp(4, "Right"); }
            set { setNodeProp(4, "Right", value); }
        }

        public Node Parent
        {
            get { return getNodeProp(5, "Parent"); }
            set { setNodeProp(5, "Parent", value); }
        }

        public void Dispose()
        {
            if (disposed)
                return;
            int err;
            nodedispose(a, out err);
            disposed = true;
        }
    }

    
    class InnerPTData
    {
        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "startpt")]
        public static extern void startpt_(int options);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "freept")]
        public static extern void freept_();

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "checkpt")]
        public static extern void checkpt_(StringBuilder infoS, out int infoT);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "raisept")]
        public static extern void raisept_(string s1, byte[] s2);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "finishpt")]
        public static extern int finishpt_();

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "nodeputp")]
        public static extern void putp_(int sNode);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "getb")]
        public static extern void getb_(out int a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "putb")]
        public static extern void putb_(int a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "putn")]
        public static extern void putn_(int a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "putr")]
        public static extern void putr_(double a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "putc")]
        public static extern void putc_(char a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "puts")]
        public static extern void puts_(string a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "getn")]
        public static extern void getn_(out int a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "getr")]
        public static extern void getr_(out double a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "getc")]
        public static extern void getc_(out char a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "gets")]
        public static extern void gets_(StringBuilder a);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "disposep")]
        public static extern void disposep_(IntPtr sNode);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "currentlocale2")]
        static extern void currentlocale2_(StringBuilder a);

        public static string currentlocale_()
        {
            StringBuilder a = new StringBuilder(200);
            currentlocale2_(a);
            return a.ToString();
        }

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "show")]
        public static extern void show_(byte[] s);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "hidetask")]
        public static extern void hidetask_();

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "ptmbox")]
        public static extern void ptmbox_(string s);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "getsysdir")]
        public static extern void getsysdir_(StringBuilder s);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "getlibwithprefix")]
        public static extern int getlibwithprefix_(string taskname, string prefix, string ext);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "getlibwithprefixinfo")]
        public static extern void getlibwithprefixinfo_(StringBuilder info);

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "setlibloader")]
        public static extern void setlibloader_(IntPtr p);

    }

   
    public sealed class TaskMaker
    {
        internal static string messPut1;
        internal static string messPut2;

        static TaskMaker()
        {
            messPut1 = "В методе Put указано целое число вне допустимого диапазона.";
            messPut2 = "В методе Put указан параметр недопустимого типа ";
            try
            {
                if (InnerPTData.currentlocale_() == "en")
                {
                    messPut1 = "Invalid value of integer argument in the Put method.";
                    messPut2 = "The Put method has an argument of invalid type: ";
                }
            }
            catch
            { }
        }

        public static void StartCS()
        {
            InnerPTData.startpt_(64 + 4194304);
        }

        public static void StartCSF9()
        {
            InnerPTData.startpt_(64 + 128);
        }

        public static void StartVB()
        {
            InnerPTData.startpt_(256);
        }

        public static void StartVBOld()
        {
            InnerPTData.startpt_(2 + 1024);
        }

        public static void StartVBF9()
        {
            InnerPTData.startpt_(256 + 128);
        }

        public static void StartVBOldF9()
        {
            InnerPTData.startpt_(2 + 128 + 1024);
        }

        //2018.08>
        public static void StartFS()
        {
            InnerPTData.startpt_(8388608);
        }
        
        public static void RaisePT(Exception e)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            int byteCount = utf8.GetByteCount(e.Message);
            var bytes = new Byte[byteCount];
            int bytesEncodedCount = utf8.GetBytes(e.Message, 0, e.Message.Length, bytes, 0);
            InnerPTData.raisept_(e.GetType().Name, bytes);
        }

        public static int FreePT()
        {
            int InfoT;
            StringBuilder InfoS = new StringBuilder(200);
            InnerPTData.checkpt_(InfoS, out InfoT);
            int res = InnerPTData.finishpt_();
            if (InfoT == 0)
                InnerPTData.ptmbox_(InfoS.ToString());
            InnerPTData.freept_();
            return res;
        }
    }

    //2018.08>
    public interface PTPrintable { }
    //2018.08<

    //2021.04>
    static class LoadNETTopics
    {
        public delegate int NFuncSN(string S, int N); 
        public static NFuncSN PLoadNETLib = LoadNETLib;

        static MethodInfo activate, inittaskgroup;

        static string GetTopic(string LibName)
        {
            var s = Path.GetFileNameWithoutExtension(LibName).Substring(4);
            var p = s.LastIndexOf('_');
            if (p > -1)
                s = s.Substring(0, p);
            return s;
        }
        static int LoadNETLib(string LibName, int Step)
        {
            if (Step == 1)
            {
                Assembly lib = null;
                Type libClass = null;
                activate = null;
                inittaskgroup = null;
                try
                {
                    lib = Assembly.LoadFrom(LibName + ".dll"); 
                    LibName = GetTopic(LibName);
                    libClass = lib.GetType("xPT4"+LibName + ".xPT4" + LibName);
                    if (libClass == null)
                    {
                        LibName = LibName.ToUpper();
                        foreach (Type t in lib.GetTypes())
                        {
                            if (t.FullName.ToUpper() == "XPT4" + LibName + ".XPT4" + LibName)
                            {
                                libClass = t;
                                break;
                            }
                        }
                    }
                    if (libClass == null)
                        return 1;
                    inittaskgroup = libClass.GetMethod("inittaskgroup");
                    activate = libClass.GetMethod("activate");
                    if (inittaskgroup != null && activate == null)
                        return 2; // dll and inittaskgroup found, activate not found
                    if (inittaskgroup == null || activate == null)
                        return 3; // dll found, activate or inittaskgroup not found
                    return 0; // 1st step OK
                }
                catch
                {
                    return 4;
                }
            }
            else
                if (activate != null && inittaskgroup != null)
                try
                {
                    activate.Invoke(null, new object[1] { LibName });
                    inittaskgroup.Invoke(null, null);
               return 0; // 2nd step OK
                }
                catch(Exception) 
                {
                    return 2; // error while run methods
                }
            else
                return 1;  // empty methods
        }
    }
    //2021.04<
    

}