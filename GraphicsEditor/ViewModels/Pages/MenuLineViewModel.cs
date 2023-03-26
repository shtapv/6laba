using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsEditor.Models;
using Avalonia.Controls.Shapes;
using DynamicData;

namespace GraphicsEditor.ViewModels.Pages
{
    public class MenuLineViewModel : ViewModelBase
    {
        private string name;
        private string startPoint;
        private string endPoint;
        private int strokeNum;
        private ObservableCollection<SolidColorBrush> colors;
        private double thicknessLine;

        public MenuLineViewModel()
        {
            Name = "";
            StartPoint = "";
            EndPoint = "";
            StrokeNum = 0;
            ThicknessLine = 1;
            Colors = new ObservableCollection<SolidColorBrush>();
            var brushes = typeof(Brushes).GetProperties().Select(brush => brush.GetValue(brush));
            foreach (object? el in brushes)
            {
                Colors.Add(Converters.StringToBrush(el.ToString()));
            }

        }

        public void SetIndexOfColor(SolidColorBrush color)
        {
            for(int i = 0; i < Colors.Count; i++)
            {
                if (Colors[i].Color == color.Color) { StrokeNum = i; break; }
            }
        }
        public string StartPoint
        {
            get => startPoint;
            set {
                this.RaiseAndSetIfChanged(ref startPoint, value);
            }
        }

        public string EndPoint
        {
            get => endPoint;
            set => this.RaiseAndSetIfChanged(ref endPoint, value);
        }
        public double ThicknessLine
        {
            get => thicknessLine;
            set => this.RaiseAndSetIfChanged(ref thicknessLine, value);
        }
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }
        public int StrokeNum
        {
            get => strokeNum;
            set => this.RaiseAndSetIfChanged(ref strokeNum, value);
        }

        public ObservableCollection<SolidColorBrush> Colors
        {
            get => colors;
            set => this.RaiseAndSetIfChanged(ref colors, value);
        }
    }
}
