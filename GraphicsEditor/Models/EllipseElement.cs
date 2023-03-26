using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public class EllipseElement : Figures
    {
        private string startPoint;
        private double width;
        private double height;
        private string strokeColor;
        private double strokeThickness;
        private string fillColor;

        public string StartPoint
        {
            get => startPoint;
            set => startPoint = value;
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
        public string FillColor
        {
            get => fillColor;
            set => fillColor = value;
        }
        public double Width
        {
            get => width;
            set => width = value;
        }
        public double Height
        {
            get => height;
            set => height = value;
        }
    }
}
