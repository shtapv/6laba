using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public class PolygonElement : Figures
    {
        private string points;
        private string strokeColor;
        private double strokeThickness;
        private string fillColor;

        public string Points
        {
            get => points;
            set => points = value;
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
    }
}
