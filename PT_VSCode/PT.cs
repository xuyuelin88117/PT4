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

    public class PT
    {

        //2018.08>
        static int precision = 2;
        static int width = 0;
        static int spaces = 0;
        static bool lineBreak = false;

        public static void SetWidth(int w)
        {
            if (w >= 0)
                width = w;
        }
        public static void SetPrecision(int p)
        {
            precision = p;
        }
        //2018.08>

        [DllImport("xPT4", CharSet = CharSet.Ansi, EntryPoint = "task")]
        static extern void Task1(string name);

        public static void Task(string name)
        {
            InnerPTData.setlibloader_(Marshal
                .GetFunctionPointerForDelegate(LoadNETTopics.PLoadNETLib));
            Task1(name);
        }


        public static bool GetBool()
        {
            int b;
            InnerPTData.getb_(out b);
            if (b == 0) return false;
            else return true;
        }


        public static int GetInt()
        {
            int n;
            InnerPTData.getn_(out n);
            return n;
        }

        public static double GetDouble()
        {
            double r;
            InnerPTData.getr_(out r);
            return r;
        }

        public static char GetChar()
        {
            char c;
            InnerPTData.getc_(out c);
            return c;
        }

        public static Node GetNode()
        {
            int n;
            Node.nodegetp(out n);
            return Node.IntToNode(n);
        }


        static void PutNode(Node aNode)
        {
            if (aNode == null)
                InnerPTData.putp_(0);
            else
                InnerPTData.putp_(aNode.a);
        }

        static void Put(bool a)
        {
            if (a)
                InnerPTData.putb_(1);
            else
                InnerPTData.putb_(0);
        }

        static void Put(long a)
        {
            if (a < int.MinValue || a > int.MaxValue)
                throw new ArgumentException(TaskMaker.messPut1);
            else
                InnerPTData.putn_((int)a);

        }

        static void Put(ulong a)
        {
            if (a > int.MaxValue)
                throw new ArgumentException(TaskMaker.messPut1);
            else
                InnerPTData.putn_((int)a);

        }

        static void Put(uint a)
        {
            if (a > int.MaxValue)
                throw new ArgumentException(TaskMaker.messPut1);
            else
                InnerPTData.putn_((int)a);

        }

        public static string GetString()
        {
            StringBuilder s = new StringBuilder(200);
            InnerPTData.gets_(s);
            return s.ToString();
        }

        public static void Put(params object[] args)
        {
            if (args == null) PutNode(null);
            else
                for (int i = 0; i < args.Length; i++)
                    if (args[i] == null) PutNode(null);
                    else if (args[i] is int) InnerPTData.putn_((int)args[i]);
                    else if (args[i] is double) InnerPTData.putr_((double)args[i]);
                    else if (args[i] is string) InnerPTData.puts_((string)args[i]);
                    else if (args[i] is char) InnerPTData.putc_((char)args[i]);
                    //2018.08>
                    else if (args[i] is IEnumerable)
                    {
                        var e = (args[i] as IEnumerable).GetEnumerator();
                        while (e.MoveNext())
                            Put(e.Current);
                    }
                    else if (args[i] is Node) PutNode((Node)args[i]);
                    else if (args[i] is bool) Put((bool)args[i]);
                    else if (args[i] is float) InnerPTData.putr_((float)args[i]);
                    else if (args[i] is decimal) InnerPTData.putr_((double)(decimal)args[i]);
                    else if (args[i] is long) Put((long)args[i]);
                    else if (args[i] is short) InnerPTData.putn_((short)args[i]);
                    else if (args[i] is ushort) InnerPTData.putn_((ushort)args[i]);
                    else if (args[i] is uint) Put((uint)args[i]);
                    else if (args[i] is ulong) Put((ulong)args[i]);
                    else if (args[i] is PTPrintable) Put(args[i].ToString());
                    else if (args[i].GetType().FullName.StartsWith("System.Tuple"))
                        foreach (var e in args[i].GetType().GetProperties())
                            Put(e.GetValue(args[i], null));
                    else if (args[i].GetType().FullName.StartsWith("System.ValueTuple"))
                        foreach (var e in args[i].GetType().GetFields())
                            Put(e.GetValue(args[i]));
                    //2018.08<
                    else throw new ArgumentException(TaskMaker.messPut2 + args[i].GetType().Name + ".");
        }

        
        static void ShowStr(string a)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            int byteCount = utf8.GetByteCount(a);
            var bytes = new Byte[byteCount];
            int bytesEncodedCount = utf8.GetBytes(a, 0, a.Length, bytes, 0);
            InnerPTData.show_(bytes);
        }

        static void ShowChar(Char a)
        {
            ShowStr(a.ToString());
        }


        public static void Show(int a)
        {
            ShowStr(a.ToString().PadLeft(width));
        }

        public static void Show(string a)
        {
            ShowStr(a.PadRight(width));
        }

        public static void Show(char a)
        {
            ShowStr(a.ToString().PadRight(width));
        }

        public static void Show(double a)
        {
            ShowStr((precision > 0 ? string.Format("{0," + width + ":f" + precision + "}", a)
              : (precision == 0 ? string.Format("{0," + width + ":e}", a)
              : string.Format("{0," + width + ":e" + (-precision) + "}", a))).Replace(',', '.'));
        }


        static void ShowArray(Array a, int[] indexes, int i)
        {
            if (i == a.Rank)
            {
                Show(a.GetValue(indexes));
            }
            else
            {
                if (lineBreak)
                    for (int j = 0; j < spaces; ++j)
                        ShowChar(' ');
                ShowChar('[');
                lineBreak = false;
                for (int k = 0; k < a.GetLength(i); ++k)
                {
                    indexes[i] = k;
                    spaces += 1;
                    ShowArray(a, indexes, i + 1);
                    spaces -= 1;
                    if (k < a.GetLength(i) - 1)
                        if (!lineBreak)
                            ShowChar(',');
                }
                if (lineBreak)
                    for (int j = 0; j < spaces; ++j)
                        ShowChar(' ');
                ShowChar(']');
                ShowChar('\n');
                lineBreak = true;
            }
        }

        public static void Show<T>(T[] a)
        {
            ShowArray(a, new int[a.Rank], 0);
        }

        public static void Show(params object[] args)
        {
            var b = false;
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i] == null) ShowStr("null");
                else if (args[i] is int) Show((int)args[i]);
                else if (args[i] is short) ShowStr(((short)args[i]).ToString().PadLeft(width));
                else if (args[i] is long) ShowStr(((long)args[i]).ToString().PadLeft(width));
                else if (args[i] is byte) ShowStr(((byte)args[i]).ToString().PadLeft(width));
                else if (args[i] is uint) ShowStr(((uint)args[i]).ToString().PadLeft(width));
                else if (args[i] is ushort) ShowStr(((ushort)args[i]).ToString().PadLeft(width));
                else if (args[i] is ulong) ShowStr(((ulong)args[i]).ToString().PadLeft(width));
                else if (args[i] is sbyte) ShowStr(((sbyte)args[i]).ToString().PadLeft(width));
                else if (args[i] is double) Show((double)args[i]);
                else if (args[i] is float) Show((float)args[i]);
                else if (args[i] is decimal) Show((double)(decimal)args[i]);
                else if (args[i] is char) Show((char)args[i]);
                else if (args[i] is string) Show((string)args[i]);
                else if (args[i] is bool) Show(((bool)args[i]).ToString().PadRight(width));
                else if (args[i] is Node) Show("Node");
                else
                {
                    string fname = args[i].GetType().FullName;
                    string sname = args[i].GetType().Name;
                    if (fname.StartsWith("System.Tuple"))
                    {
                        lineBreak = false;
                        ShowChar('(');
                        foreach (var e in args[i].GetType().GetProperties())
                        {
                            if (b)
                                ShowChar(',');
                            else
                                b = true;
                            Show(e.GetValue(args[i], null));
                        }
                        ShowChar(')');
                        b = false;
                    }
                    else if (fname.StartsWith("System.ValueTuple"))
                    {
                        lineBreak = false;
                        ShowChar('(');
                        foreach (var e in args[i].GetType().GetFields())
                        {
                            if (b)
                                ShowChar(',');
                            else
                                b = true;
                            Show(e.GetValue(args[i]));
                        }
                        ShowChar(')');
                        b = false;
                    }
                    else if (fname.StartsWith("System.Collections.Generic.KeyValuePair") ||
                        fname.StartsWith("System.Collections.DictionaryEntry"))
                    {
                        lineBreak = false;
                        ShowChar('(');
                        foreach (var e in args[i].GetType().GetProperties())
                        {
                            if (b)
                                ShowChar(':');
                            else
                                b = true;
                            Show(e.GetValue(args[i], null));
                        }
                        ShowChar(')');
                        b = false;
                    }
                    else if (args[i] is System.Array)
                    {
                        var a = args[i] as System.Array;
                        ShowArray(a, new int[a.Rank], 0);
                    }
                    else if (args[i] is IEnumerable)
                    {
                        var isdictorset = sname.Equals("Dictionary`2") || sname.Equals("SortedDictionary`2") ||
                            sname.StartsWith("SortedList") || sname.Equals("Hashtable") ||
                            sname.Equals("HashSet`1") || sname.Equals("SortedSet`1");
                        char lbr = '[';
                        char rbr = ']';
                        if (isdictorset)
                        {
                            lbr = '{';
                            rbr = '}';
                        }
                        if (lineBreak)
                            for (int j = 0; j < spaces; ++j)
                                ShowChar(' ');
                        ShowChar(lbr);
                        lineBreak = false;
                        var e = (args[i] as IEnumerable).GetEnumerator();
                        while (e.MoveNext())
                        {
                            if (b)
                            {
                                if (!lineBreak)
                                    ShowChar(',');
                            }
                            else
                                b = true;
                            spaces += 1;
                            Show(e.Current);
                            spaces -= 1;
                        }
                        if (lineBreak)
                            for (int j = 0; j < spaces; ++j)
                                ShowChar(' ');
                        ShowChar(rbr);
                        ShowChar('\n');
                        lineBreak = true;
                        b = false;
                    }
                    else
                        Show(args[i].ToString());
                }
            }
        }

        public static void ShowLine<T>(T[] a)
        {
            Show(a);
        }

        public static void ShowLine(params object[] args)
        {
            lineBreak = false;
            Show(args);
            if (!lineBreak)
                ShowChar('\n');
        }


        public static void HideTask()
        {
            InnerPTData.hidetask_();
        }

        //2016.01>
        public static IEnumerable<int> GetEnumerableInt()
        {
            return Enumerable.Repeat(0, GetInt()).Select(e => GetInt()).ToArray();
        }
        public static IEnumerable<double> GetEnumerableDouble()
        {
            return Enumerable.Repeat(0.0, GetInt()).Select(e => GetDouble()).ToArray();
        }
        public static IEnumerable<char> GetEnumerableChar()
        {
            return Enumerable.Repeat(' ', GetInt()).Select(e => GetChar()).ToArray();
        }
        public static IEnumerable<string> GetEnumerableString()
        {
            return Enumerable.Repeat("", GetInt()).Select(e => GetString()).ToArray();
        }
        public static IEnumerable<int> GetEnumerableInt(int count)
        {
            return Enumerable.Repeat(0, count).Select(e => GetInt()).ToArray();
        }
        public static IEnumerable<double> GetEnumerableDouble(int count)
        {
            return Enumerable.Repeat(0.0, count).Select(e => GetDouble()).ToArray();
        }
        public static IEnumerable<char> GetEnumerableChar(int count)
        {
            return Enumerable.Repeat(' ', count).Select(e => GetChar()).ToArray();
        }
        public static IEnumerable<string> GetEnumerableString(int count)
        {
            return Enumerable.Repeat("", count).Select(e => GetString()).ToArray();
        }
        //2016.01<

        public static IEnumerable<int> GetSeqInt()
        {
            return Enumerable.Repeat(0, GetInt()).Select(e => GetInt()).ToArray();
        }
        public static IEnumerable<double> GetSeqDouble()
        {
            return Enumerable.Repeat(0.0, GetInt()).Select(e => GetDouble()).ToArray();
        }
        public static IEnumerable<char> GetSeqChar()
        {
            return Enumerable.Repeat(' ', GetInt()).Select(e => GetChar()).ToArray();
        }
        public static IEnumerable<string> GetSeqString()
        {
            return Enumerable.Repeat("", GetInt()).Select(e => GetString()).ToArray();
        }
        public static IEnumerable<int> GetSeqInt(int count)
        {
            return Enumerable.Repeat(0, count).Select(e => GetInt()).ToArray();
        }
        public static IEnumerable<double> GetSeqDouble(int count)
        {
            return Enumerable.Repeat(0.0, count).Select(e => GetDouble()).ToArray();
        }
        public static IEnumerable<char> GetSeqChar(int count)
        {
            return Enumerable.Repeat(' ', count).Select(e => GetChar()).ToArray();
        }
        public static IEnumerable<string> GetSeqString(int count)
        {
            return Enumerable.Repeat("", count).Select(e => GetString()).ToArray();
        }

        public static int[] GetArrInt()
        {
            return Enumerable.Repeat(0, GetInt()).Select(e => GetInt()).ToArray();
        }
        public static double[] GetArrDouble()
        {
            return Enumerable.Repeat(0.0, GetInt()).Select(e => GetDouble()).ToArray();
        }
        public static char[] GetArrChar()
        {
            return Enumerable.Repeat(' ', GetInt()).Select(e => GetChar()).ToArray();
        }
        public static string[] GetArrString()
        {
            return Enumerable.Repeat("", GetInt()).Select(e => GetString()).ToArray();
        }
        public static int[] GetArrInt(int count)
        {
            return Enumerable.Repeat(0, count).Select(e => GetInt()).ToArray();
        }
        public static double[] GetArrDouble(int count)
        {
            return Enumerable.Repeat(0.0, count).Select(e => GetDouble()).ToArray();
        }
        public static char[] GetArrChar(int count)
        {
            return Enumerable.Repeat(' ', count).Select(e => GetChar()).ToArray();
        }
        public static string[] GetArrString(int count)
        {
            return Enumerable.Repeat("", count).Select(e => GetString()).ToArray();
        }

        public static List<int> GetListInt()
        {
            return Enumerable.Repeat(0, GetInt()).Select(e => GetInt()).ToList();
        }
        public static List<double> GetListDouble()
        {
            return Enumerable.Repeat(0.0, GetInt()).Select(e => GetDouble()).ToList();
        }
        public static List<char> GetListChar()
        {
            return Enumerable.Repeat(' ', GetInt()).Select(e => GetChar()).ToList();
        }
        public static List<string> GetListString()
        {
            return Enumerable.Repeat("", GetInt()).Select(e => GetString()).ToList();
        }
        public static List<int> GetListInt(int count)
        {
            return Enumerable.Repeat(0, count).Select(e => GetInt()).ToList();
        }
        public static List<double> GetListDouble(int count)
        {
            return Enumerable.Repeat(0.0, count).Select(e => GetDouble()).ToList();
        }
        public static List<char> GetListChar(int count)
        {
            return Enumerable.Repeat(' ', count).Select(e => GetChar()).ToList();
        }
        public static List<string> GetListString(int count)
        {
            return Enumerable.Repeat("", count).Select(e => GetString()).ToList();
        }

        public static int[,] GetMatrInt(int rowCount, int colCount)
        {
            var res = new int[rowCount, colCount];
            for (int i = 0; i < rowCount; ++i)
                for (int j = 0; j < colCount; ++j)
                    res[i, j] = GetInt();
            return res;
        }
        public static int[,] GetMatrInt(int rowCount)
        {
            return GetMatrInt(rowCount, rowCount);
        }
        public static int[,] GetMatrInt()
        {
            return GetMatrInt(GetInt(), GetInt());
        }
        public static double[,] GetMatrDouble(int rowCount, int colCount)
        {
            var res = new double[rowCount, colCount];
            for (int i = 0; i < rowCount; ++i)
                for (int j = 0; j < colCount; ++j)
                    res[i, j] = GetDouble();
            return res;
        }
        public static double[,] GetMatrDouble(int rowCount)
        {
            return GetMatrDouble(rowCount, rowCount);
        }
        public static double[,] GetMatrDouble()
        {
            return GetMatrDouble(GetInt(), GetInt());
        }
        public static char[,] GetMatrChar(int rowCount, int colCount)
        {
            var res = new char[rowCount, colCount];
            for (int i = 0; i < rowCount; ++i)
                for (int j = 0; j < colCount; ++j)
                    res[i, j] = GetChar();
            return res;
        }
        public static char[,] GetMatrChar(int rowCount)
        {
            return GetMatrChar(rowCount, rowCount);
        }
        public static char[,] GetMatrChar()
        {
            return GetMatrChar(GetInt(), GetInt());
        }
        public static string[,] GetMatrString(int rowCount, int colCount)
        {
            var res = new string[rowCount, colCount];
            for (int i = 0; i < rowCount; ++i)
                for (int j = 0; j < colCount; ++j)
                    res[i, j] = GetString();
            return res;
        }
        public static string[,] GetMatrString(int rowCount)
        {
            return GetMatrString(rowCount, rowCount);
        }
        public static string[,] GetMatrString()
        {
            return GetMatrString(GetInt(), GetInt());
        }

        public static int[][] GetArrArrInt(int rowCount, int colCount)
        {
            var res = new int[rowCount][];
            for (int i = 0; i < rowCount; ++i)
            {
                res[i] = new int[colCount];
                for (int j = 0; j < colCount; ++j)
                    res[i][j] = GetInt();
            }
            return res;
        }
        public static int[][] GetArrArrInt(int rowCount)
        {
            return GetArrArrInt(rowCount, rowCount);
        }
        public static int[][] GetArrArrInt()
        {
            return GetArrArrInt(GetInt(), GetInt());
        }
        public static double[][] GetArrArrDouble(int rowCount, int colCount)
        {
            var res = new double[rowCount][];
            for (int i = 0; i < rowCount; ++i)
            {
                res[i] = new double[colCount];
                for (int j = 0; j < colCount; ++j)
                    res[i][j] = GetDouble();
            }
            return res;
        }
        public static double[][] GetArrArrDouble(int rowCount)
        {
            return GetArrArrDouble(rowCount, rowCount);
        }
        public static double[][] GetArrArrDouble()
        {
            return GetArrArrDouble(GetInt(), GetInt());
        }
        public static char[][] GetArrArrChar(int rowCount, int colCount)
        {
            var res = new char[rowCount][];
            for (int i = 0; i < rowCount; ++i)
            {
                res[i] = new char[colCount];
                for (int j = 0; j < colCount; ++j)
                    res[i][j] = GetChar();
            }
            return res;
        }
        public static char[][] GetArrArrChar(int rowCount)
        {
            return GetArrArrChar(rowCount, rowCount);
        }
        public static char[][] GetArrArrChar()
        {
            return GetArrArrChar(GetInt(), GetInt());
        }
        public static string[][] GetArrArrString(int rowCount, int colCount)
        {
            var res = new string[rowCount][];
            for (int i = 0; i < rowCount; ++i)
            {
                res[i] = new string[colCount];
                for (int j = 0; j < colCount; ++j)
                    res[i][j] = GetString();
            }
            return res;
        }
        public static string[][] GetArrArrString(int rowCount)
        {
            return GetArrArrString(rowCount, rowCount);
        }
        public static string[][] GetArrArrString()
        {
            return GetArrArrString(GetInt(), GetInt());
        }

        public static List<List<int>> GetListListInt(int rowCount, int colCount)
        {
            var res = new List<List<int>>(rowCount);
            for (int i = 0; i < rowCount; ++i)
            {
                res.Add(new List<int>(colCount));
                for (int j = 0; j < colCount; ++j)
                    res[i].Add(GetInt());
            }
            return res;
        }
        public static List<List<int>> GetListListInt(int rowCount)
        {
            return GetListListInt(rowCount, rowCount);
        }
        public static List<List<int>> GetListListInt()
        {
            return GetListListInt(GetInt(), GetInt());
        }
        public static List<List<double>> GetListListDouble(int rowCount, int colCount)
        {
            var res = new List<List<double>>(rowCount);
            for (int i = 0; i < rowCount; ++i)
            {
                res.Add(new List<double>(colCount));
                for (int j = 0; j < colCount; ++j)
                    res[i].Add(GetDouble());
            }
            return res;
        }
        public static List<List<double>> GetListListDouble(int rowCount)
        {
            return GetListListDouble(rowCount, rowCount);
        }
        public static List<List<double>> GetListListDouble()
        {
            return GetListListDouble(GetInt(), GetInt());
        }
        public static List<List<char>> GetListListChar(int rowCount, int colCount)
        {
            var res = new List<List<char>>(rowCount);
            for (int i = 0; i < rowCount; ++i)
            {
                res.Add(new List<char>(colCount));
                for (int j = 0; j < colCount; ++j)
                    res[i].Add(GetChar());
            }
            return res;
        }
        public static List<List<char>> GetListListChar(int rowCount)
        {
            return GetListListChar(rowCount, rowCount);
        }
        public static List<List<char>> GetListListChar()
        {
            return GetListListChar(GetInt(), GetInt());
        }
        public static List<List<string>> GetListListString(int rowCount, int colCount)
        {
            var res = new List<List<string>>(rowCount);
            for (int i = 0; i < rowCount; ++i)
            {
                res.Add(new List<string>(colCount));
                for (int j = 0; j < colCount; ++j)
                    res[i].Add(GetString());
            }
            return res;
        }
        public static List<List<string>> GetListListString(int rowCount)
        {
            return GetListListString(rowCount, rowCount);
        }
        public static List<List<string>> GetListListString()
        {
            return GetListListString(GetInt(), GetInt());
        }



        public static (bool, bool) GetBool2()
        {
            return (GetBool(), GetBool());
        }

        public static (bool, bool, bool) GetBool3()
        {
            return (GetBool(), GetBool(), GetBool());
        }

        public static (bool, bool, bool, bool) GetBool4()
        {
            return (GetBool(), GetBool(), GetBool(), GetBool());
        }

        public static (int, int) GetInt2()
        {
            return (GetInt(), GetInt());
        }

        public static (int, int, int) GetInt3()
        {
            return (GetInt(), GetInt(), GetInt());
        }

        public static (int, int, int, int) GetInt4()
        {
            return (GetInt(), GetInt(), GetInt(), GetInt());
        }

        public static (double, double) GetDouble2()
        {
            return (GetDouble(), GetDouble());
        }

        public static (double, double, double) GetDouble3()
        {
            return (GetDouble(), GetDouble(), GetDouble());
        }

        public static (double, double, double, double) GetDouble4()
        {
            return (GetDouble(), GetDouble(), GetDouble(), GetDouble());
        }


        public static (char, char) GetChar2()
        {
            return (GetChar(), GetChar());
        }

        public static (char, char, char) GetChar3()
        {
            return (GetChar(), GetChar(), GetChar());
        }

        public static (char, char, char, char) GetChar4()
        {
            return (GetChar(), GetChar(), GetChar(), GetChar());
        }


        public static (Node, Node) GetNode2()
        {
            return (GetNode(), GetNode());
        }

        public static (Node, Node, Node) GetNode3()
        {
            return (GetNode(), GetNode(), GetNode());
        }

        public static (Node, Node, Node, Node) GetNode4()
        {
            return (GetNode(), GetNode(), GetNode(), GetNode());
        }

        public static (string, string) GetString2()
        {
            return (GetString(), GetString());
        }

        public static (string, string, string) GetString3()
        {
            return (GetString(), GetString(), GetString());
        }

        public static (string, string, string, string) GetString4()
        {
            return (GetString(), GetString(), GetString(), GetString());
        }


        public static void Dummy()
        {}

    }


    public static class PT4Linq
    {
        
        public static void Put<T>(this IEnumerable<T> a)
        {
            var b = a.ToArray();
            PT.Put(b.Length);
            foreach (T e in b)
                PT.Put(e);
        }
        public static void Put<T>(this IEnumerable<T> a, int count)
        {
            var b = a.ToArray();
            if (count <= 0 || count > b.Length)
                count = b.Length;
            for (int i = 0; i < count; i++)
                PT.Put(b[i]);
        }
        public static IEnumerable<TSource> Show<TSource, TResult>(this IEnumerable<TSource> a,
            string cmt, Func<TSource, TResult> selector)
        {
            var b = a.Select(selector).ToArray();
            PT.Show(cmt);
            PT.Show((b.Length + ":").PadLeft(3));
            foreach (var e in b)
                PT.Show(e);
            PT.ShowLine();
            return a;
        }
        public static IEnumerable<TSource> Show<TSource, TResult>(this IEnumerable<TSource> a,
            Func<TSource, TResult> selector)
        {
            return a.Show("", selector);
        }
        public static IEnumerable<T> Show<T>(this IEnumerable<T> a, string cmt)
        {
            return a.Show(cmt, e => e);
        }
        public static IEnumerable<T> Show<T>(this IEnumerable<T> a)
        {
            return a.Show("", e => e);
        }
    }
    //2016.01<


}

