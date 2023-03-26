using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public class CompFig : Figures
    {
        private string commands;
        private string strokeColor;
        private double strokeThickness;
        private string fillColor;

        public string Commands
        {
            get => commands;
            set => commands = value;
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
