using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using DynamicData;
using GraphicsEditor.Models;
using GraphicsEditor.ViewModels;
using GraphicsEditor.Models;
using GraphicsEditor.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;

namespace GraphicsEditor.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void OpenFileDialogMenuXmlClick(object sender, RoutedEventArgs routedEventArgs)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            List<string> formates = new List<string>
            {
                "xml"
            };
            openFileDialog.Filters.Add(new FileDialogFilter { Extensions = formates });
            openFileDialog.AllowMultiple = false;
            string[]? result = await openFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    dataContext.FigureList = Serializer<ObservableCollection<Figures>>.Load(result[0]);
                    foreach (Figures f in dataContext.FigureList)
                    {
                        dataContext.Shapes.Add(dataContext.ElementToShape(f));
                    }
                }
            }

        }
        private async void OpenFileDialogMenuJsonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            List<string> formates = new List<string>
            {
                "json"
            };
            openFileDialog.Filters.Add(new FileDialogFilter { Extensions = formates, Name = "Json files" });
            openFileDialog.AllowMultiple = false;
            string[]? result = await openFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    dataContext.FigureList = JsonSerializer<ObservableCollection<Figures>>.Load(result[0]);
                    foreach (Figures f in dataContext.FigureList)
                    {
                        dataContext.Shapes.Add(dataContext.ElementToShape(f));
                    }
                }
            }

        }
        private async void SaveFileDialogMenuXmlClick(object sender, RoutedEventArgs routedEventArgs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            List<string> formates = new List<string>
            {
                "xml"
            };
            saveFileDialog.Filters.Add(new FileDialogFilter { Extensions = formates, Name = "Xml files" });
            string? result = await saveFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    Serializer<ObservableCollection<Figures>>.Save(result, dataContext.FigureList);
                }
            }
        }
        private async void SaveFileDialogMenuJsonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            List<string> formates = new List<string>
            {
                "json"
            };
            saveFileDialog.Filters.Add(new FileDialogFilter { Extensions = formates, Name = "Json files" });
            string? result = await saveFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    JsonSerializer<ObservableCollection<Figures>>.Save(result, dataContext.FigureList);
                }
            }
        }
        private async void SaveFileDialogMenuPngClick(object sender, RoutedEventArgs routedEventArgs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            List<string> formates = new List<string>
            {
                "png"
            };
            saveFileDialog.Filters.Add(new FileDialogFilter { Extensions = formates, Name = "Png files" });
            string? result = await saveFileDialog.ShowAsync(this);
            var canvas = this.GetVisualDescendants().OfType<Canvas>().Where(canvas => canvas.Name.Equals("canvas")).FirstOrDefault();
            if (canvas != null)
            {
                var pixelSize = new PixelSize((int)canvas.Bounds.Width, (int)canvas.Bounds.Height);
                var size = new Size(canvas.Bounds.Width, canvas.Bounds.Height);
                using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pixelSize, new Avalonia.Vector(96, 96)))
                {
                    canvas.Measure(size);
                    canvas.Arrange(new Rect(size));
                    bitmap.Render(canvas);
                    bitmap.Save(result);
                }
            }
        }
        private void DeleteShape(object sender, RoutedEventArgs routedEventArgs)
        {
            Grid grid = ((Button)sender).Parent as Grid;
            TextBlock textblock = grid.Children[0] as TextBlock;
            string name = textblock.Text;
            if (DataContext is MainWindowViewModel window)
            {
                window.FigureList.Remove(window.FigureList.First(x => x.Name == name));
                window.Shapes.Remove(window.Shapes.First(x => x.Name == name));
            }
        }

    }
}
