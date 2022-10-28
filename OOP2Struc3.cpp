#include "pt4.h"
using namespace std;

class TextView
{
	// §¯§¦ §ª§©§®§¦§¯§Á§«§´§¦ §²§¦§¡§­§ª§©§¡§¸§ª§À §¥§¡§¯§¯§°§¤§° §¬§­§¡§³§³§¡
    int x = 0, y = 0;
    int width = 1, height = 1;
public:
    void GetOrigin(int& x, int& y);
    void SetOrigin(int x, int y);
    void GetSize(int& width, int& height);
    void SetSize(int width, int height);
};

void TextView::GetOrigin(int& x, int& y)
{
    x = this->x;
    y = this->y;
}
void TextView::SetOrigin(int x, int y)
{
    this->x = x;
    this->y = y;
}
void TextView::GetSize(int& width, int& height)
{
    width = this->width;
    height = this->height;
}
void TextView::SetSize(int width, int height)
{
    this->width = width;
    this->height = height;
}

class Shape
{
public:
    virtual string GetInfo() = 0;
    virtual void MoveBy(int a, int b) = 0;
};

// Implement the RectShape and TextShape descendant classes
class RectShape : public Shape
{
public:
    RectShape(int x1, int y1, int x2, int y2) : x1(x1), y1(y1), x2(x2), y2(y2) { }
    string GetInfo() override { return "R(" + to_string(x1) + "," + to_string(y1) + ")(" + to_string(x2) + "," + to_string(y2) + ")"; }
    void MoveBy(int a, int b) override { x1 += a; x2 += a; y1 += b; y2 += b; }
private:
    int x1, y1, x2, y2;
};

class TextShape : public Shape
{
public:
    TextShape(TextView& tview, int x1, int y1, int x2, int y2);
    string GetInfo() override;
    void MoveBy(int a, int b) override;
private:
    TextView& tview;
};

TextShape::TextShape(TextView& tview, int x1, int y1, int x2, int y2) : tview(tview)
{
    this->tview.SetOrigin(x1, y1);
    this->tview.SetSize(x2 - x1, y2 - y1);
}
string TextShape::GetInfo()
{
    int x0, y0, width, height;
    tview.GetOrigin(x0, y0);
    tview.GetSize(width, height);
    string x1, y1, x2, y2;
    x1 = to_string(x0);
    y1 = to_string(y0);
    x2 = to_string(x0 + width);
    y2 = to_string(y0 + height);
    return "T(" + x1 + "," + y1 + ")(" + x2 + "," + y2 + ")";
}
void TextShape::MoveBy(int a, int b)
{
    int x0, y0;
    tview.GetOrigin(x0, y0);
    tview.SetOrigin(x0 + a, y0 + b);
}

void Solve()
{
    Task("OOP2Struc3");
    int n, a, b;
    pt >> n;
    vector<Shape*> shape(n);
    vector<TextView> text(n, TextView());
    for (int i = 0; i < n; i++) {
        char c;
        pt >> c;
        int x_1, y_1, x_2, y_2;
        pt >> x_1 >> y_1 >> x_2 >> y_2;
        if (c == 'R')
            shape[i] = new RectShape(x_1, y_1, x_2, y_2);
        else
            shape[i] = new TextShape(text[i], x_1, y_1, x_2, y_2);
    }
    pt >> a >> b;
     for (auto i : shape) {
        i->MoveBy(a, b);
        pt << i->GetInfo();
    }
}
