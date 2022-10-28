// File: "OOP2Struc4"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public class TextView
        {
            // Do not change the implementation of the class
            int x, y;
            int width = 1, height = 1;
            public void GetOrigin(out int x, out int y)
            {
                x = this.x;
                y = this.y;
            }
            public void SetOrigin(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public void GetSize(out int width, out int height)
            {
                width = this.width;
                height = this.height;
            }
            public void SetSize(int width, int height)
            {
                this.width = width;
                this.height = height;
            }
        }

        public interface IShape
        {
            // Convert this abstract class into the IShape interface
            string GetInfo();
             void MoveBy(int x, int y);
        }

        public class RectShape: IShape {
            int x1, y1, x2, y2;
            public RectShape(int x1, int y1, int x2, int y2) {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }
            public void MoveBy(int x, int y) {
                this.x1 = this.x1 + x;
                this.y1 = this.y1 + y;
                this.x2 = this.x2 + x;
                this.y2 = this.y2 + y;
            }
            public string GetInfo() {
                return "R(" + x1 + "," + y1 + ")(" + x2 + "," + y2 + ")" ;
            }
        }

        public class TextShape: TextView, IShape {
            public TextShape(int x1, int y1, int x2, int y2) {
                this.SetOrigin(x1, y1);
                int width = x2 - x1;
                int hight = y2 - y1;
                this.SetSize(width, hight);
            }

            public string GetInfo() {
                int x1, y1, width, height;
                this.GetOrigin(out x1, out y1);
                this.GetSize(out width, out height);
                int x2 = width + x1;
                int y2 = height + y1;
                return "T(" + x1 + "," + y1 + ")(" + x2 + "," + y2 + ")";
            }

            public void MoveBy(int a, int b) {
                int x, y;
                this.GetOrigin(out x, out y);
                this.SetOrigin(x + a, y + b);
            }

        }

        // Implement the RectShape and TextShape classes
        // These classes must implement the IShape interface;
        //   the TextShape class must be a descendant of the TextView class

        public static void Solve()
        {
            Task("OOP2Struc4");

            int n = GetInt();

            IShape[] shape = new IShape[n]; 

            for (int i = 0; i < n; ++i) {
                char @char = GetChar();
                int x1 = GetInt();
                int y1 = GetInt();
                int x2 = GetInt();
                int y2 = GetInt();
                
                if (@char == 'R')
                    shape[i] = new RectShape(x1, y1 , x2, y2);
                else
                    shape[i] = new TextShape(x1, y1, x2, y2);
            }

            int x = GetInt();
            int y = GetInt();
            for (int i = 0; i < n; ++i) {
                shape[i].MoveBy(a, b);
                Put(shape[i].GetInfo());
            }

        }
    }
}
