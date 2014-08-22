using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace JMChart
{
    /// <summary>
    /// 雷达图基线
    /// </summary>
    public class RadarCanvas : ChartCanvas
    {
        public RadarCanvas() {
            IsCircle = false;
        }

        /// <summary>
        /// 雷达图外形
        /// </summary>
        public enum RadarType
        { 
            /// <summary>
            /// 线开型
            /// </summary>
            Line=0,
            /// <summary>
            /// 圆型
            /// </summary>
            Circle=1
        }

        double _angle = 0;
        /// <summary>
        /// 纵线之间的角度
        /// </summary>
        public double Angle
        {
            get {
                _angle = 360 / VerticalCount;
                return _angle;
            }
        }

        /// <summary>
        /// 中心点
        /// </summary>
        public Point Center
        {
            get;
            internal set;
        }

        /// <summary>
        /// 是否为圆型图
        /// </summary>
        public bool IsCircle { get; set; }

        /// <summary>
        /// 半径
        /// </summary>
        public double Radius
        {
            get;
            internal set;
        }

        /// <summary>
        /// 展现
        /// </summary>
        public override void Draw() 
        {
            Reset();

            //画基线
            DrawLine();
            
            base.Draw();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="data"></param>
        internal override void InitSeries(object data)
        {
            base.InitSeries(data);

            if (data == null) return;

            //获取集合接口
            var enumerator = data as System.Collections.ICollection;

            if (enumerator != null)
            {
                var modelindex = 0;
                foreach (var d in enumerator)
                {
                    var item = new Series.RadarSeries(this);
                    item.DataContext = d;
                    var color = modelindex < SeriesColors.Length ? SeriesColors[modelindex] : SeriesColors[modelindex % SeriesColors.Length];
                    
                    item.Stroke = new SolidColorBrush(color);
                    if (this.ItemLabels != null && this.ItemLabels.Length > 0)
                    {
                        item.ItemTooltipFormat = this.ItemLabels.Length > modelindex ? this.ItemLabels[modelindex] : this.ItemLabels[this.ItemLabels.Length - 1];
                    }
                    else
                    {
                        //默认采用Y名称加Y值的格式
                        item.ItemTooltipFormat = "#YName:#Y";
                    }

                    color.A = 60;
                    item.Fill = new SolidColorBrush(color);

                    modelindex++;
                    this.Serieses.Add(item);
                }
            }
        }

        /// <summary>
        /// 计算画布位置
        /// </summary>
        protected override void SetCanvasLayout()
        {
            //base.SetCanvasLayout();

            Radius = Math.Min(Width, Height) / 2;

            //中心点
            Center = new Point(Radius, Radius);           
        }

        /// <summary>
        /// 以中间为点画线
        /// </summary>
        /// <param name="center"></param>
        private void DrawLine()
        {
            var angle = Angle;

            var center = Center;
            //横轴个数
            var hcount = this.HorizontalCount;
           
            //x轴每一小格的长度
            var xstep = Radius / hcount;
            var yaxises = new RadarAxis[VerticalCount];

            var dataDic = Common.Helper.GetMaxandNinValue(DataContext as System.Collections.ICollection, this.YValueNames);
            
            //从90度起开始画线条
            //画星状线
            for (var i = 0; i < yaxises.Length; i++)
            {
                var axis =yaxises[i]= new RadarAxis();
                var path = new Path();
                axis.AxisShap = path;
                axis.BindName = YValueNames[i];
                var dic = dataDic[axis.BindName];
                axis.MaxValue = dic[1].Value;
                axis.MinValue = Common.Helper.CheckMinValue(dic[0].Value);

                axis.Length = Radius;
                axis.Rotate = i * angle ;

                ///设定Y标签
                if (this.YLabels != null && i < this.YLabels.Length)
                {
                    axis.Label = this.YLabels[i];
                }

                //计算纵轴离中心的偏移量
                var offsety = axis.RotateSin * Radius;
                var offsetx = axis.RotateCos * Radius;
                
                axis.StartPoint = center;
                axis.EndPoint = new Point(center.X + offsetx, center.Y + offsety);
                axis.AType = AxisType.YRadar;
                axis.Stroke = ForeColor;

                var geo = new PathGeometry();
                path.Data = geo;
                var fig = new PathFigure();
                geo.Figures.Add(fig);
                fig.StartPoint = axis.StartPoint;
                fig.Segments.Add(new LineSegment() { Point=axis.EndPoint });

                AddChild(axis.AxisShap);

                //生成Y轴标签
                var label = CreateYLabel(axis);               

                AddChild(label);

                this.Axises.Add(axis);                
            }

            //当线条少于5条时。无法画多边形。因为四条就是一个正方形。所以会默认用圆形。
            if (IsCircle == false && VerticalCount > 4)
            {
                for (var i = 0; i < hcount; i++)
                {
                    var xaxis = new RadarAxis() { AType = AxisType.XRadar, Stroke = ForeColor};
                    var shap = new Polygon();
                    shap.Stroke = xaxis.Stroke;
                    
                    xaxis.AxisShap = shap;                    
                    
                    this.Axises.Add(xaxis);                   
                    var step = xstep * (i + 1);

                    foreach (var yaxis in yaxises)
                    {
                        //纵坐标上的点偏移量
                        var xoffsetx = yaxis.RotateCos * step;
                        var xoffsety = yaxis.RotateSin * step;

                        var p = new Point(Center.X + xoffsetx, Center.Y + xoffsety);
                        shap.Points.Add(p);
                    }
                    
                    AddChild(shap);
                }
            }
            else
            {
                for (var i = 0; i < hcount; i++)
                {
                    var xaxis = new RadarAxis() { AType = AxisType.XRadar, Stroke = ForeColor};
                    var path = new Path();
                    xaxis.AxisShap = path;
                    
                    this.Axises.Add(xaxis);

                    var step = xstep * (i + 1);
                    var cirl = new EllipseGeometry();
                    path.Data = cirl;
                    cirl.Center = Center;
                    cirl.RadiusX =cirl.RadiusY = step;
                    
                    AddChild(xaxis.AxisShap);
                }
            }
        }

        /// <summary>
        /// 生成Y轴标签
        /// </summary>
        /// <param name="label">标签值</param>
        /// <param name="rotate">旋转角度</param>
        /// <returns></returns>
        protected TextBlock CreateYLabel(RadarAxis axis)
        {
            var textBlock = new TextBlock();
           
            textBlock.Text = axis.Label;
            textBlock.TextAlignment = TextAlignment.Center;
            var transform = new CompositeTransform();
            transform.Rotation = axis.Rotate < 180 && axis.Rotate > 0 ? axis.Rotate - 90 : axis.Rotate + 90;            
            textBlock.RenderTransform = transform;

            var x = axis.EndPoint.X + axis.RotateCos * textBlock.ActualHeight;// -axis.RotateSin * textBlock.ActualWidth / 2;
            var y = axis.EndPoint.Y + axis.RotateSin * textBlock.ActualHeight;// +axis.RotateCos * textBlock.ActualWidth / 2;

            textBlock.SetValue(Canvas.LeftProperty, x);
            textBlock.SetValue(Canvas.TopProperty, y);

            return textBlock;
        }
    }
}
