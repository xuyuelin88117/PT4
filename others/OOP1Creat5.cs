// File: "OOP1Creat5"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class AbstractButton
        {
            public abstract string GetControl();
        }

        // Implement the Button1 and Button2 descendant classes
        public class Button1 : AbstractButton
        {
            string info;
            public Button1(string s)
            {
                info = s.ToUpper();
            }
            public override string GetControl()
            {
                string x="[" + info +"]";
                return x;
            }
        }
        public class Button2 : AbstractButton
        {
            string info;
            public Button2(string s)
            {
                info = s.ToLower ();
                
            }
            public override string GetControl()
            {
                string x="<" + info +">";
                return x;
            }
        }

        public abstract class AbstractLabel
        {
            public abstract string GetControl();
        }

        // Implement the Label1 and Label2 descendant classes
         public class Label1 : AbstractLabel
        {
            string info;
            public Label1(string s)
            {
                info = s.ToUpper ();
            }
            public override string GetControl()
            {
                string x="=" + info +"=";
                return x;
            }
        }
        public class Label2 : AbstractLabel
        {
            string info;
            public Label2(string s)
            {
                info = s.ToLower ();
            }
            public override string GetControl()
            {
                string x= "\""+ info + "\"";
                return x;
            }
        }
        public abstract class ControlFactory
        {
            public abstract AbstractButton CreateButton(string text);
            public abstract AbstractLabel CreateLabel(string text);
        }

        // Implement the Factory1 and Factory2 descendant classes
        class Factory1 : ControlFactory
        {
             public override AbstractButton CreateButton(string text)
            {
                return new Button1(text);
            }
            public override AbstractLabel CreateLabel(string text)
            {
                return new Label1(text);
            }
        }
        class Factory2 : ControlFactory
        {
             public override AbstractButton CreateButton(string text)
            {
                return new Button2(text);
            }
            public override AbstractLabel CreateLabel(string text)
            {
                return new Label2(text);
            }
        }

        public class Client
        {
            ControlFactory f;
            string info;
            // Add required fields
            public Client(ControlFactory f)
            {
                // Implement the constructor
                this.f=f;
            }
            public void AddButton(string text)
            {
                // Implement the method
                info=info+f.CreateButton (text).GetControl ()+" ";

            }
            public void AddLabel(string text)
            {
                // Implement the method
                info=info+f.CreateLabel(text).GetControl ()+" ";
            }
            public string GetControls()
            {
                //return "";
                info=info.Remove (info.Length -1);
                return info;
                // Remove the previous statement and implement the method
            }
        }
    
        public static void Solve()
        {
            Task("OOP1Creat5");
            int N = GetInt();
            Factory1 f1=new Factory1 ();
            Factory2 f2=new Factory2 ();
            Client c1=new Client (f1);
            Client c2=new Client (f2);
            for (int i=0;i<N;i++){
                string text = GetString();
                if(text[0]=='B'){
                    c1.AddButton (text.Remove (0,2));
                    c2.AddButton (text.Remove (0,2));
                }
                if(text[0]=='L'){
                    c1.AddLabel (text.Remove (0,2));
                    c2.AddLabel (text.Remove (0,2));
                }
            }
            Put(c1.GetControls ());
            Put(c2.GetControls ());


        }
    }
}
