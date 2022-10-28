// File: "LinqObj58"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    //��ʼ���а����й���ѧ������ͼ������ѧѧ��ͨ�� USE �Ľ������Ϣ����ָ��˳�򣩡�ÿ������Ԫ�ذ��������ֶΣ�
    //<����> <��������ĸ> <ѧУ���> <USE����>
    //USE ������ 0 �� 100 ��Χ�ڵ���������������֮����һ���ո������
    //����ÿ��ѧУ���ҳ�����һ�ſ�Ŀ�÷ֵ��� 50 �ֵ�ǰ����ѧ��������ĸ˳�򣩣���������ǵ����ϡ���������ĸ��ѧУ��š�
    //�ڵ�����������ʾÿ��ѧ������Ϣ���������Ϻ�����ĸ����ĸ˳���������ƥ�䣬������ѧУ��š�
    //���ĳ��ѧУ����ָ��������ѧ����������������ʾ���д���ѧ������Ϣ��
    //�����ʼ����û��һ��ѧ������ָ�����������ı���Students not found��д�����ļ���
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj58");
            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        name = s[0],
                        nm = s[1],
                        school = int.Parse(s[2]),
                        score1 = int.Parse(s[3]),
                        score2 = int.Parse(s[4]),
                        score3 = int.Parse(s[5]),
                    };
                }).Where(e => e.score1 < 50 || e.score2 < 50 || e.score3 < 50)
                  .OrderBy(e => e.name).ThenBy(e => e.nm)
                  .GroupBy(e => e.school).Show()
                  .SelectMany(x => x.OrderBy(y => y.name).Take(3))
                  .OrderBy(e => e.name).ThenBy(e => e.nm).ThenBy(e => e.school)
                  .Select(e => e.name + " " + e.nm + " " + e.school);


            if (r.Count() != 0)
                File.WriteAllLines(GetString(), r.ToArray(), Encoding.Default);
            else
            {
                string S = "Required students not found";
                File.WriteAllText(GetString(), S, Encoding.Default);
            }
        }
    }
}
