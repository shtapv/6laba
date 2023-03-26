using Avalonia;
using Avalonia.Media;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public static class Converters
    {
        public static string BrushToString(SolidColorBrush brush)
        {
            return brush.ToString();
        }

        public static SolidColorBrush StringToBrush(string str)
        {
            return new SolidColorBrush(Avalonia.Media.Color.Parse(str));
        }

        public static string PointToString(Avalonia.Point p)
        {
            return p.ToString();
        }

        public static Avalonia.Point StringToPoint(string str)
        {
            string[] s = str.Split(",");
            return new Avalonia.Point(double.Parse(s[0]), double.Parse(s[1]));
        }
        public static List<Avalonia.Point> StringToPoints(string str)
        {
            List <Avalonia.Point> list = new List<Avalonia.Point>();
            string[] s = str.Replace(",", " ").Split(" ");
            for (int i = 0; i < s.Count(); i+=2)
            {
                list.Add(new Avalonia.Point(double.Parse(s[i]), double.Parse(s[i + 1])));
            }
            return list;
        }
        public static Thickness StringToMargin(string str)
        {
            string[] s = str.Split(",");
            return new Thickness(double.Parse(s[0]), double.Parse(s[1]));
        }
    }
}
