using Avalonia.Controls.Shapes;
using Avalonia.Media;
using DynamicData;
using GraphicsEditor.ViewModels.Pages;
using GraphicsEditor.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace GraphicsEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int figureIndex;
        private int figureListIndex;
        private object[] figureViews;
        private object content;
        private ObservableCollection<Figures> figureList;
        private ObservableCollection<Shape> shapes;
        private Figures curFigure;
        private bool replace;

        public MainWindowViewModel()
        {
            replace = false;
            figureViews = new object[6];
            FigureListIndex = -1;
            FigureList = new ObservableCollection<Figures>();
            Shapes = new ObservableCollection<Shape>();
            figureViews[0] = new MenuLineViewModel();
            figureViews[1] = new MenuPolylineViewModel();
            figureViews[2] = new MenuPolygonViewModel();
            figureViews[3] = new MenuRectangleViewModel();
            figureViews[4] = new MenuEllipseViewModel();
            figureViews[5] = new MenuPathViewModel();
            FigureIndex = 0;
            ClearParam = ReactiveCommand.Create(() => {
                FigureListIndex = -1;
                if (Content is MenuLineViewModel newObject)
                {
                    newObject.Name = "";
                    newObject.StartPoint = "";
                    newObject.EndPoint = "";
                    newObject.StrokeNum = 0;
                    newObject.ThicknessLine = 1;
                }
                if (Content is MenuPolylineViewModel polyline)
                {
                    polyline.Name = "";
                    polyline.Points = "";
                    polyline.StrokeNum = 0;
                    polyline.ThicknessLine = 1;
                }
                if (Content is MenuPolygonViewModel polygon)
                {
                    polygon.Name = "";
                    polygon.Points = "";
                    polygon.StrokeNum = 0;
                    polygon.FillNum = 0;
                    polygon.ThicknessLine = 1;
                }
                if (Content is MenuRectangleViewModel rectangle)
                {
                    rectangle.Name = "";
                    rectangle.StartPoint = "";
                    rectangle.Width = "";
                    rectangle.Height = "";
                    rectangle.FillNum = 0;
                    rectangle.StrokeNum = 0;
                    rectangle.ThicknessLine = 1;
                }
                if (Content is MenuEllipseViewModel ellipse)
                {
                    ellipse.Name = "";
                    ellipse.StartPoint = "";
                    ellipse.Width = "";
                    ellipse.Height = "";
                    ellipse.FillNum = 0;
                    ellipse.StrokeNum = 0;
                    ellipse.ThicknessLine = 1;
                }
                if (Content is MenuPathViewModel path)
                {
                    path.Name = "";
                    path.Commands = "";
                    path.StrokeNum = 0;
                    path.FillNum = 0;
                    path.ThicknessLine = 1;
                }
                FigureIndex = figureIndex;
            });
            AddFigure = ReactiveCommand.Create(() =>
            {
                if (Content is MenuLineViewModel newObject)
                {
                    if (newObject.Name == "" || newObject.EndPoint == "" || newObject.StartPoint == "") return;
                    curFigure = new LineElement { Name = newObject.Name, EndPoint = newObject.EndPoint, StartPoint = newObject.StartPoint, StrokeColor = newObject.Colors[newObject.StrokeNum].ToString(), StrokeThickness = newObject.ThicknessLine };
                }
                if (Content is MenuPolylineViewModel newObjectPolyline)
                {
                    if (newObjectPolyline.Name == "" || newObjectPolyline.Points == "") return;
                    curFigure = new BrokenLine { Name = newObjectPolyline.Name, Points = newObjectPolyline.Points, StrokeColor = newObjectPolyline.Colors[newObjectPolyline.StrokeNum].ToString(), StrokeThickness = newObjectPolyline.ThicknessLine };
                }
                if (Content is MenuPolygonViewModel newObjectPolygon)
                {
                    if (newObjectPolygon.Name == "" || newObjectPolygon.Points == "") return;
                    curFigure = new PolygonElement { Name = newObjectPolygon.Name, Points = newObjectPolygon.Points, StrokeColor = newObjectPolygon.Colors[newObjectPolygon.StrokeNum].ToString(), FillColor = newObjectPolygon.Colors[newObjectPolygon.FillNum].ToString(), StrokeThickness = newObjectPolygon.ThicknessLine };
                }
                if (Content is MenuRectangleViewModel newObjectRectangle)
                {
                    if (newObjectRectangle.Name == "" || newObjectRectangle.StartPoint == "" || newObjectRectangle.Width == "" || newObjectRectangle.Height == "") return;
                    if (double.TryParse(newObjectRectangle.Width, out _) == false)
                    {
                        ClearParam.Execute().Subscribe();
                        return;
                    }
                    if (double.TryParse(newObjectRectangle.Height, out _) == false)
                    {
                        ClearParam.Execute().Subscribe();
                        return;
                    }
                    curFigure = new RectangleElement { Name = newObjectRectangle.Name, StartPoint = newObjectRectangle.StartPoint, Width = double.Parse(newObjectRectangle.Width), Height = double.Parse(newObjectRectangle.Height), StrokeColor = newObjectRectangle.Colors[newObjectRectangle.StrokeNum].ToString(), FillColor = newObjectRectangle.Colors[newObjectRectangle.FillNum].ToString(), StrokeThickness = newObjectRectangle.ThicknessLine };
                }
                if (Content is MenuEllipseViewModel newObjectEllipse)
                {
                    if (newObjectEllipse.Name == "" || newObjectEllipse.StartPoint == "" || newObjectEllipse.Width == "" || newObjectEllipse.Height == "") return;
                    if (double.TryParse(newObjectEllipse.Width, out _) == false)
                    {
                        ClearParam.Execute().Subscribe();
                        return;
                    }
                    if (double.TryParse(newObjectEllipse.Height, out _) == false)
                    {
                        ClearParam.Execute().Subscribe();
                        return;
                    }
                    curFigure = new EllipseElement { Name = newObjectEllipse.Name, StartPoint = newObjectEllipse.StartPoint, Width = double.Parse(newObjectEllipse.Width), Height = double.Parse(newObjectEllipse.Height), StrokeColor = newObjectEllipse.Colors[newObjectEllipse.StrokeNum].ToString(), FillColor = newObjectEllipse.Colors[newObjectEllipse.FillNum].ToString(), StrokeThickness = newObjectEllipse.ThicknessLine };
                }
                if (Content is MenuPathViewModel newObjectPath)
                {
                    if (newObjectPath.Name == "" || newObjectPath.Commands == "") return;
                    curFigure = new CompFig { Name = newObjectPath.Name, Commands = newObjectPath.Commands, StrokeColor = newObjectPath.Colors[newObjectPath.StrokeNum].ToString(), FillColor = newObjectPath.Colors[newObjectPath.FillNum].ToString(), StrokeThickness = newObjectPath.ThicknessLine };
                }
                AddShape(curFigure);
                ClearParam.Execute().Subscribe();
            });
        }

        public Shape ElementToShape(Figures obj)
        {
            if (obj is LineElement line)
            {
                return new Line
                {
                    Name = line.Name,
                    StartPoint = Converters.StringToPoint(line.StartPoint),
                    EndPoint = Converters.StringToPoint(line.EndPoint),
                    Stroke = Converters.StringToBrush(line.StrokeColor),
                    StrokeThickness = line.StrokeThickness
                };
            }
            if (obj is BrokenLine polyline)
            {
                return new Polyline
                {
                    Name = polyline.Name,
                    Points = Converters.StringToPoints(polyline.Points),
                    Stroke = Converters.StringToBrush(polyline.StrokeColor),
                    StrokeThickness = polyline.StrokeThickness
                };
            }
            if (obj is PolygonElement polygon)
            {
                return new Polygon
                {
                    Name = polygon.Name,
                    Points = Converters.StringToPoints(polygon.Points),
                    Fill = Converters.StringToBrush(polygon.FillColor),
                    Stroke = Converters.StringToBrush(polygon.StrokeColor),
                    StrokeThickness = polygon.StrokeThickness
                };
            }
            if (obj is RectangleElement rectangle)
            {
                return new Avalonia.Controls.Shapes.Rectangle
                {
                    Name = rectangle.Name,
                    Margin = Converters.StringToMargin(rectangle.StartPoint),
                    Width = rectangle.Width,
                    Height = rectangle.Height,
                    Fill = Converters.StringToBrush(rectangle.FillColor),
                    Stroke = Converters.StringToBrush(rectangle.StrokeColor),
                    StrokeThickness = rectangle.StrokeThickness
                };
            }
            if (obj is EllipseElement ellipse)
            {
                return new Avalonia.Controls.Shapes.Ellipse
                {
                    Name = ellipse.Name,
                    Margin = Converters.StringToMargin(ellipse.StartPoint),
                    Width = ellipse.Width,
                    Height = ellipse.Height,
                    Fill = Converters.StringToBrush(ellipse.FillColor),
                    Stroke = Converters.StringToBrush(ellipse.StrokeColor),
                    StrokeThickness = ellipse.StrokeThickness
                };
            }
            if (obj is CompFig path)
            {
                return new Avalonia.Controls.Shapes.Path
                {
                    Name = path.Name,
                    Data = Geometry.Parse(path.Commands),
                    Fill = Converters.StringToBrush(path.FillColor),
                    Stroke = Converters.StringToBrush(path.StrokeColor),
                    StrokeThickness = path.StrokeThickness
                };
            }
            return null;
        }

        private void AddShape(Figures obj)
        {
            if (obj is LineElement line)
            {
                if (line.Name == "") return;
                string[] st = line.StartPoint.Split(",");
                string[] en = line.EndPoint.Split(",");
                if (st.Count() != 2 || en.Count() != 2) return;
                foreach (string el in st)
                {
                    if (double.TryParse(el, out _) == false) return;
                }
                foreach (string el in en)
                {
                    if (double.TryParse(el, out _) == false) return;
                }
                if (!(FigureList.Any(n => n.Name == line.Name)))
                {
                    FigureList.Add(line);
                    Shapes.Add(ElementToShape(line));
                }
                else
                {
                    FigureList.Replace(FigureList.First(n => n.Name == line.Name), line);
                    Shapes.Replace(Shapes.First(n => n.Name == line.Name), ElementToShape(line));
                }
            }
            if (obj is BrokenLine polyline)
            {
                if (polyline.Name == "") return;
                string[] st = polyline.Points.Replace(",", " ").Split(" ");
                if (st.Count() % 2 == 1 || st.Count() < 4) return;
                foreach (string el in st)
                {
                    if (double.TryParse(el, out _) == false) return;
                }
                if (!(FigureList.Any(n => n.Name == polyline.Name)))
                {
                    FigureList.Add(polyline);
                    Shapes.Add(ElementToShape(polyline));
                }
                else
                {
                    FigureList.Replace(FigureList.First(n => n.Name == polyline.Name), polyline);
                    Shapes.Replace(Shapes.First(n => n.Name == polyline.Name), ElementToShape(polyline));
                }
            }
            if (obj is PolygonElement polygon)
            {
                if (polygon.Name == "") return;
                string[] st = polygon.Points.Replace(",", " ").Split(" ");
                if (st.Count() % 2 == 1 || st.Count() < 6) return;
                foreach (string el in st)
                {
                    if (double.TryParse(el, out _) == false) return;
                }
                if (!(FigureList.Any(n => n.Name == polygon.Name)))
                {
                    FigureList.Add(polygon);
                    Shapes.Add(ElementToShape(polygon));
                }
                else
                {
                    FigureList.Replace(FigureList.First(n => n.Name == polygon.Name), polygon);
                    Shapes.Replace(Shapes.First(n => n.Name == polygon.Name), ElementToShape(polygon));
                }
            }
            if (obj is RectangleElement rectangle)
            {
                if (rectangle.Name == "" || rectangle.Width < 1 || rectangle.Height < 1) return;
                string[] st = rectangle.StartPoint.Split(",");
                if (st.Count() != 2) return;
                foreach (string el in st)
                {
                    if (double.TryParse(el, out _) == false) return;
                }
                if (!(FigureList.Any(n => n.Name == rectangle.Name)))
                {
                    FigureList.Add(rectangle);
                    Shapes.Add(ElementToShape(rectangle));
                }
                else
                {
                    FigureList.Replace(FigureList.First(n => n.Name == rectangle.Name), rectangle);
                    Shapes.Replace(Shapes.First(n => n.Name == rectangle.Name), ElementToShape(rectangle));
                }
            }
            if (obj is EllipseElement ellipse)
            {
                if (ellipse.Name == "" || ellipse.Width < 1 || ellipse.Height < 1) return;
                string[] st = ellipse.StartPoint.Split(",");
                if (st.Count() != 2) return;
                foreach (string el in st)
                {
                    if (double.TryParse(el, out _) == false) return;
                }
                if (!(FigureList.Any(n => n.Name == ellipse.Name)))
                {
                    FigureList.Add(ellipse);
                    Shapes.Add(ElementToShape(ellipse));
                }
                else
                {
                    FigureList.Replace(FigureList.First(n => n.Name == ellipse.Name), ellipse);
                    Shapes.Replace(Shapes.First(n => n.Name == ellipse.Name), ElementToShape(ellipse));
                }
            }
            if (obj is CompFig path)
            {
                try
                {
                    var temp = Geometry.Parse(path.Commands);
                }
                catch
                {
                    return;
                }
                if (!(FigureList.Any(n => n.Name == path.Name)))
                {
                    FigureList.Add(path);
                    Shapes.Add(ElementToShape(path));
                }
                else
                {
                    FigureList.Replace(FigureList.First(n => n.Name == path.Name), path);
                    Shapes.Replace(Shapes.First(n => n.Name == path.Name), ElementToShape(path));
                }
            }
        }

        public object Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public int FigureIndex
        {
            get => figureIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref figureIndex, value);
                Content = figureViews[figureIndex];
                if (replace == true) FigureListIndex = -1;
            }
        }
        public int FigureListIndex
        {
            get => figureListIndex;
            set
            {
                replace = false;
                this.RaiseAndSetIfChanged(ref figureListIndex, value);
                if (figureListIndex != -1)
                {
                    if (FigureList[figureListIndex] is LineElement line)
                    {
                        FigureIndex = 0;
                        if (Content is MenuLineViewModel cont)
                        {
                            replace = true;
                            cont.Name = line.Name;
                            cont.StartPoint = line.StartPoint;
                            cont.EndPoint = line.EndPoint;
                            cont.SetIndexOfColor(Converters.StringToBrush(line.StrokeColor));
                            cont.ThicknessLine = line.StrokeThickness;
                        }
                    }
                    if (FigureList[figureListIndex] is BrokenLine polyline)
                    {
                        FigureIndex = 1;
                        if (Content is MenuPolylineViewModel cont)
                        {
                            replace = true;
                            cont.Name = polyline.Name;
                            cont.Points = polyline.Points;
                            cont.SetIndexOfColor(Converters.StringToBrush(polyline.StrokeColor));
                            cont.ThicknessLine = polyline.StrokeThickness;
                        }
                    }
                    if (FigureList[figureListIndex] is PolygonElement polygon)
                    {
                        FigureIndex = 2;
                        if (Content is MenuPolygonViewModel cont)
                        {
                            replace = true;
                            cont.Name = polygon.Name;
                            cont.Points = polygon.Points;
                            cont.SetIndexOfColor(Converters.StringToBrush(polygon.StrokeColor));
                            cont.SetIndexOfColorFill(Converters.StringToBrush(polygon.FillColor));
                            cont.ThicknessLine = polygon.StrokeThickness;
                        }
                    }
                    if (FigureList[figureListIndex] is RectangleElement rectangle)
                    {
                        FigureIndex = 3;
                        if (Content is MenuRectangleViewModel cont)
                        {
                            replace = true;
                            cont.Name = rectangle.Name;
                            cont.StartPoint = rectangle.StartPoint;
                            cont.Width = rectangle.Width.ToString();
                            cont.Height = rectangle.Height.ToString();
                            cont.SetIndexOfColor(Converters.StringToBrush(rectangle.StrokeColor));
                            cont.SetIndexOfColorFill(Converters.StringToBrush(rectangle.FillColor));
                            cont.ThicknessLine = rectangle.StrokeThickness;
                        }
                    }
                    if (FigureList[figureListIndex] is EllipseElement ellipse)
                    {
                        FigureIndex = 4;
                        if (Content is MenuEllipseViewModel cont)
                        {
                            replace = true;
                            cont.Name = ellipse.Name;
                            cont.StartPoint = ellipse.StartPoint;
                            cont.Width = ellipse.Width.ToString();
                            cont.Height = ellipse.Height.ToString();
                            cont.SetIndexOfColor(Converters.StringToBrush(ellipse.StrokeColor));
                            cont.SetIndexOfColorFill(Converters.StringToBrush(ellipse.FillColor));
                            cont.ThicknessLine = ellipse.StrokeThickness;
                        }
                    }
                    if (FigureList[figureListIndex] is CompFig path)
                    {
                        FigureIndex = 5;
                        if (Content is MenuPathViewModel cont)
                        {
                            replace = true;
                            cont.Name = path.Name;
                            cont.Commands = path.Commands;
                            cont.SetIndexOfColor(Converters.StringToBrush(path.StrokeColor));
                            cont.SetIndexOfColorFill(Converters.StringToBrush(path.FillColor));
                            cont.ThicknessLine = path.StrokeThickness;
                        }
                    }
                }
            }
        }

        public ObservableCollection<Figures> FigureList
        {
            get => figureList;
            set
            {
                this.RaiseAndSetIfChanged(ref figureList, value);
            }
        }

        public ObservableCollection<Shape> Shapes
        {
            get => shapes;
            set => this.RaiseAndSetIfChanged(ref shapes, value);
        }
        public Figures CurFigure
        {
            get => curFigure;
            set => this.RaiseAndSetIfChanged(ref curFigure, value);
        }
        public ReactiveCommand<Unit, Unit> AddFigure { get; }
        public ReactiveCommand<Unit, Unit> ClearParam { get; }
    }
}
