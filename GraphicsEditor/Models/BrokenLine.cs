using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public class BrokenLine : Figures
    {
        private string points;
        private string strokeColor;
        private double strokeThickness;


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
    }
}
