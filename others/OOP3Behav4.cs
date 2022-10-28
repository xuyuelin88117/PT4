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
        public class EmptyValidator : Validator
        {
            public override string Validate(string s)
            {
                if (s.Length == 0) {
                    return "!Empty text";
                }
                return "";
            }
        }
        public class NumberValidator : Validator
        {

            public override string Validate(string s)
            {
                int num;
                bool isNum = int.TryParse(s, out num);
                if (isNum)
                {
                    return "";
                }
                return "!'" + s + "': not a number";
            }
        }
        public class RangeValidator : Validator
        {
            int min, max;
            public RangeValidator(int a, int b)
            {
                if (a < b)
                {
                    min = a; max = b;
                }
                else
                {
                    min = b; max = a;
                }
            }
            public override string Validate(string s)
            {
                int num;
                bool isNum = int.TryParse(s, out num);
                if (isNum && num >= min && num <= max)
                {
                    return "";
                }
                return "!'" + s + "': not in range " + min + ".." + max;
            }
        }
        // Implement the TextBox and TextForm classes

        public class TextBox
        {
            string text;
            Validator v;
            public TextBox()
            {
                v = new Validator();
                text = "";
            }
            public void SetText(string text) {
                this.text = text;
            }
            public void SetValidator(Validator v) {
                this.v = v;
            }
            public string Validate() {
                return v.Validate(text);
            }
        }

        public class TextForm 
        {
            TextBox[] tb;

            public TextForm(int N) {
                tb = new TextBox[N];
                for (int i = 0; i < N; i++)
                    tb[i] = new TextBox();
            }
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
                string str = "";
                for (int i = 0; i < tb.Length; i++)
                    str += tb[i].Validate();
                return str;
            }
        }

        public static void Solve()
        {
            Task("OOP3Behav4");
            int N = GetInt();
            int A = GetInt();
            int B = GetInt();

            int K = GetInt();
            int[] par1 = new int[K];
            char[] par2 = new char[K];

            for (int i = 0; i < K; i++) 
            {
                par1[i] = GetInt();
                par2[i] = GetChar();
            }

            string[] masStr1 = new string[N];
            string[] masStr2 = new string[N];
            string[] masStr3 = new string[N];
            string[] masStr4 = new string[N];
            string[] masStr5 = new string[N];

            for (int i = 0; i < N; i++)
            {
                masStr1[i] = GetString();
            }
            for (int i = 0; i < N; i++)
            {
                masStr2[i] = GetString();
            }
            for (int i = 0; i < N; i++)
            {
                masStr3[i] = GetString();
            }
            for (int i = 0; i < N; i++)
            {
                masStr4[i] = GetString();
            }
            for (int i = 0; i < N; i++)
            {
                masStr5[i] = GetString();
            }

            TextForm tf = new TextForm(N);

            for (int i = 0; i < K; i++)
            {
                if (par2[i] == 'E')
                    tf.SetValidator(par1[i], new EmptyValidator());
                else if (par2[i] == 'R')
                    tf.SetValidator(par1[i], new RangeValidator(A, B));
                else
                    tf.SetValidator(par1[i], new NumberValidator());
            }

            for (int i = 0; i < N; i++)
            {
                tf.SetText(i, masStr1[i]);
            }
            Put(tf.Validate());
            for (int i = 0; i < N; i++)
            {
                tf.SetText(i, masStr2[i]);
            }
            Put(tf.Validate());
            for (int i = 0; i < N; i++)
            {
                tf.SetText(i, masStr3[i]);
            }
            Put(tf.Validate());
            for (int i = 0; i < N; i++)
            {
                tf.SetText(i, masStr4[i]);
            }
            Put(tf.Validate());
            for (int i = 0; i < N; i++)
            {
                tf.SetText(i, masStr5[i]);
            }
            Put(tf.Validate());
        }
    }
}
