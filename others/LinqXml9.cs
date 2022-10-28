using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using PT4;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PT4Tasks
{
    public class MyTask: PT
    {
        // ��� ������� ����� ������ LinqXml �������� ���������
        // �������������� ������ ����������, ������������ � ���������:
        //
        //   Show() � Show(cmt) - ���������� ������ ������������������,
        //     cmt - ��������� �����������;
        //
        //   Show(e => r) � Show(cmt, e => r) - ���������� ������
        //     �������� r, ���������� �� ��������� e ������������������,
        //     cmt - ��������� �����������.

        public static void Solve()
        {
            Task("LinqXml9");
            var inFileName = GetString();
            var outFileName = GetString();

            var file = File.ReadLines(inFileName, Encoding.Default);

            var xmlDoc = new XDocument(
                new XDeclaration("1.0", "windows-1251", null), new XElement("root",
                    file.Select(line => line.StartsWith("comment:") 
                        ? new XComment(line.Remove(0, 8))
                        : (XNode) new XElement("line", line))
                ));

            xmlDoc.Save(outFileName);
        }
    }
}
