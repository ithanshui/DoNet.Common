using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace JMChart.Series
{
    /// <summary>
    /// 雷达图线
    /// </summary>
    public class RadarSeries:ISeries
    {
        const int AnimateDurtion = 1000;

        public RadarSeries(RadarCanvas canvas)
            :base(canvas)
        { 
        
        }

        /// <summary>
        /// 生成当前线条
        /// </summary>      
        /// <returns></returns>
        public override Shape CreatePath()
        {
            if (storyboard != null) storyboard.Stop();

            var canvas = Canvas as RadarCanvas;
            var index = 0;
            if (canvas.IsAnimate) this.storyboard = new Storyboard();

            var path = CurrentPath as Path;
            if (path == null) CurrentPath = path = new Path();

            if (canvas.IsFillShape) CurrentPath.Fill = this.Fill;

            CurrentPath.StrokeThickness = canvas.LineWidth;
            CurrentPath.Stroke = this.Stroke;

            var geo = path.Data == null ? new PathGeometry() : path.Data as PathGeometry;
            path.Data = geo;
            PathFigure fig = null;
            if (geo.Figures.Count == 0)
            {
                fig = new PathFigure();
                geo.Figures.Add(fig);
                fig.IsClosed = true;
            }
            else
            {
                fig = geo.Figures[0];
                fig.Segments.Clear();
            }

            if (canvas.IsAnimate) this.storyboard = new Storyboard();

            //起始点和线段点要分开处理
            foreach (var a in canvas.Axises)
            {
                if (a.AType != AxisType.YRadar) continue;
                var axis = a as RadarAxis;

                //获取当前点的值
                var v = this.GetNumberValue(a.BindName);
                var r = a.Step * (v - a.MinValue);

                //生成提示信息
                var tooltip = CreateTooltip(a.BindName, v.ToString());

                //目标点
                var targetPoint = new Point(canvas.Center.X + axis.RotateCos * r, canvas.Center.Y + axis.RotateSin * r);

                if (canvas.IsAnimate)
                {
                    var anima = new PointAnimation();
                    anima.To = targetPoint;
                    anima.Duration = TimeSpan.FromMilliseconds(AnimateDurtion);

                    if (index != 0)
                    {
                        var seg = new LineSegment();
                        seg.Point = canvas.Center;
                        fig.Segments.Add(seg);
                        Storyboard.SetTarget(anima, seg);
                        Storyboard.SetTargetProperty(anima, new PropertyPath("Point"));
                    }
                    else
                    {
                        Storyboard.SetTarget(anima, fig);
                        fig.StartPoint = canvas.Center;
                        Storyboard.SetTargetProperty(anima, new PropertyPath("StartPoint"));
                        index++;
                    }

                    //动画展示完后加入点
                    anima.Completed += new EventHandler((object sender, EventArgs e) =>
                    {
                        var panima = sender as PointAnimation;
                        AddPoint(panima.To.Value, 10, tooltip);
                    });

                    this.storyboard.Children.Add(anima);
                }
                else
                {
                    if (index != 0)
                    {
                        var seg = new LineSegment();
                        seg.Point = targetPoint;
                        fig.Segments.Add(seg);

                    }
                    else
                    {
                        fig.StartPoint = targetPoint;
                        index++;
                    }

                    AddPoint(targetPoint, 10, tooltip);
                }
            }
            return CurrentPath;
        }
    }
}
