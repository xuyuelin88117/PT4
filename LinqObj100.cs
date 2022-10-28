// File: "LinqObj100"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj100");

            string f1 = GetString();
            string f2 = GetString();
            string f3 = GetString();
            string f4 = GetString();
            string f5 = GetString();

            var fr1 = File.ReadLines(f1);
            var fr2 = File.ReadLines(f2);
            var fr3 = File.ReadLines(f3);
            var fr4 = File.ReadLines(f4);
            var m1 = from i4 in fr4
                     let n4 = i4.Split(' ')
                     let p = n4[0]
                     let id = n4[1]
                     let shop = n4[2]
                     join i1 in fr1
                     on id equals i1.Split(' ')[1]
                     let birth = i1.Split(' ')[2]
                     select (p, id, shop, birth);

            var m2 = from t1 in m1
                     join i2 in fr2
                     on t1.p equals i2.Split(' ')[0]
                     let contry = i2.Split(' ')[1]
                     select (t1.p, t1.id, t1.shop, t1.birth, contry);

            var m3 = from t2 in m2
                     join i3 in fr3
                     on (t2.p, t2.shop) equals (i3.Split(' ')[2], i3.Split(' ')[0])
                     let value = i3.Split(' ')[1]
                     select (t2.contry, t2.shop, t2.birth, t2.id, value);

            var m4 = from i1 in m3
                     group (i1.contry, i1.shop, i1.birth, i1.id, i1.value)
                     by (i1.contry, i1.shop)
                         into t1
                     from mid in t1
                     where int.Parse(mid.birth) == t1.Max(x => int.Parse(x.birth))
                     select mid;

            var r = from mid in m4
                    group (mid.contry, mid.shop, mid.birth, mid.id, mid.value)
                    by (mid.contry, mid.shop, mid.birth, mid.id)
                         into t3
                    from middle in t3
                    orderby middle.contry, middle.shop, int.Parse(middle.birth), int.Parse(middle.id)
                    select $"{middle.contry} {middle.shop} {middle.birth} {middle.id} " +
                           $"{t3.Sum(x => int.Parse(x.value))}";

            r = r.Distinct();

            File.WriteAllLines(f5, r);
        }
    }
}
