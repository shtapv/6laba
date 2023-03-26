using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicsEditor.Models
{
    public class LineElement : Figures
    {
        private string startPoint;
        private string endPoint;
        private string strokeColor;
        private double strokeThickness;


        public string StartPoint
        {
            get => startPoint;
            set => startPoint = value;
        }
        public string EndPoint
        {
            get => endPoint;
            set => endPoint = value;
        }
        public string StrokeColor
        {
            get => strokeColor;
            set => strokeColor = value;
        }
        public double StrokeThickness
        {
            get => strokeThickness;
            set => strokeThickness = value;
        }
    }
}
