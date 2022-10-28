// File: "OOP3Behav4"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public class Validator
        {
            public virtual string Validate(string s)
            {
                return "";
            }
        }

        // Implement the EmptyValidator, NumberValidator
        //   and RangeValidator descendant classes
        class EmptyValidator : Validator
        {
            public override string Validate(string s)
            {
                if (s != "")
                    return "";
                else
                    return "!Empty text";
            }
        };
        class NumberValidator : Validator
        {
            public override string Validate(string s)
            {
                if (s == String.Empty)
                    return "!'" + s + "': not a number";
                for (int i = 0; i < s.Length; i++)
                {
                    if (!char.IsDigit(s[i]) && (i != 0 || s[i] != '-'))
                    {
                        return "!'" + s + "': not a number";
                    }
                }
                return "";
            }
        };
        class RangeValidator : Validator
        {
            int min, max;
            public RangeValidator(int a, int b)
            {
                min = Math.Min(a, b);
                max = Math.Max(a, b);
            }
            public override string Validate(string s)
            {
                if (s == String.Empty) return "!'" + s + "': not in range " + min.ToString() + ".." + max.ToString();
                for (int i = 0; i < s.Length; i++)
                {
                    if (!char.IsDigit(s[i]) && (i != 0 || s[i] != '-'))
                    {
                        return "!'" + s + "': not in range " + min.ToString() + ".." + max.ToString();
                    }
                }
                if (int.Parse(s) >= min && int.Parse(s) <= max)
                    return "";
                return "!'" + s + "': not in range " + min.ToString() + ".." + max.ToString();
            }
        };
        // Implement the TextBox and TextForm classes
        class TextBox : Validator
        {
            string text;
            Validator v;
            public TextBox() { v = new Validator(); text = ""; }
            public void SetText(string text)
            {
                this.text = text;
            }
            public void SetValidator(Validator v)
            {
                this.v = v;
            }
            public string Validate()
            {
                return v.Validate(text);
            }

        };
        class TextForm
        {
            public TextBox[] tb;
            //List<TextBox> tb = new List<TextBox>();
            public string text;
            public int n;
            public TextForm(int n) { this.tb = new TextBox[n]; for (int i = 0; i < tb.Length; i++) { tb[i] = new TextBox(); } }
            public void SetText(int ind, string text)
            {
                tb[ind].SetText(text);
            }
            public void SetValidator(int ind, Validator v)
            {
                tb[ind].SetValidator(v);
            }
            public string Validate()
            {
                string s = "";
                for (int i = 0; i < tb.Length; i++)
                    s = s + tb[i].Validate();
                return s;
            }
        };
        public static void Solve()
        {
            Task("OOP3Behav4");
            List<Tuple<int, char>> list = new List<Tuple<int, char>>();
            int N = GetInt(), A = GetInt(), B = GetInt(), K = GetInt();
            for (int i = 0; i < K; i++)
            {

                int ind = GetInt();
                char val = GetChar();
                list.Add(new Tuple<int, char>(ind, val));
            }

            for (int i = 0; i < 5; i++)
            {
                TextForm tf = new TextForm(N);
                for (int j = 0; j < N; j++)
                {
                    string s = GetString();
                    tf.SetText(j, s);
                }
                foreach (Tuple<int, char> e in list)
                {
                    if (e.Item2 == 'E')
                        tf.SetValidator(e.Item1, new EmptyValidator());
                    else if (e.Item2 == 'N')
                        tf.SetValidator(e.Item1, new NumberValidator());
                    else
                        tf.SetValidator(e.Item1, new RangeValidator(A, B));
                }
                Put(tf.Validate());
            }

        }
    }
}
