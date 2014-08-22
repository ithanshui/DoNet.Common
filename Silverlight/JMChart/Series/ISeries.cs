using System;
using System.Net;
using System.Windows;
using System.Windows.Data;
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
    /// 图表线或图接口
    /// </summary>
    public abstract class ISeries : Common.IBaseControl
    {
        public ISeries(ChartCanvas canvas)
        {
            Canvas = canvas;
        }

        /// <summary>
        /// 画布
        /// </summary>
        public ChartCanvas Canvas { get; set; }

        /// <summary>
        /// 当前颜色
        /// </summary>
        public Brush Stroke
        {
            get;
            set;
        }

        /// <summary>
        /// 填充色
        /// </summary>
        public Brush Fill
        {
            get;
            set;
        }

        /// <summary>
        /// 当前线的label格式
        /// /// #Y=当前值，#YName=当前Y轴名称,#C{列名}=表示绑定当前数据对象的指定列值
        /// </summary>
        public string ItemTooltipFormat { get; set; }

        /// <summary>
        /// 当前绑定的对象
        /// </summary>
        public object DataContext { get; set; }

        /// <summary>
        /// 获取对象的属性的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            var obj = Common.Helper.GetPropertyName(DataContext, name);
            return obj;
        }

        /// <summary>
        /// 获取对象的值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns></returns>
        public double GetNumberValue(string name)
        {
            double value = 0;
            var obj = GetValue(name);
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out value);
            }
            return value;
        }

        /// <summary>
        /// 当前动画
        /// </summary>
        protected Storyboard storyboard;

        /// <summary>
        /// 展现
        /// </summary>
        public virtual void Draw()
        {
            var p = CreatePath();
            Canvas.AddChild(p);

            if (storyboard != null && Canvas.IsAnimate)
            {
                storyboard.Begin();
            }
        }

        Shape shap;
        /// <summary>
        /// 当前线条
        /// </summary>
        public Shape CurrentPath { 
            get { return shap; } 
            protected set { shap = value; }
        }

        /// <summary>
        /// 生成图形
        /// </summary>
        /// <returns></returns>
        public virtual Shape CreatePath()
        {
            return CurrentPath;
        }

        /// <summary>
        /// 生成图例
        /// </summary>
        /// <returns></returns>
        internal virtual StackPanel CreateLegend()
        {
            if (!string.IsNullOrWhiteSpace(Canvas.LegendLabel))
            {
                var panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                var colorarea = new Rectangle();
                colorarea.Width = 20;
                colorarea.Height = 10;
                colorarea.Fill = this.Fill;
                colorarea.Stroke = this.Stroke;
                panel.Margin = new Thickness(2);
                panel.Children.Add(colorarea);

                var text = new TextBlock();
                text.Margin = new Thickness(2);
                text.Text = Common.Helper.DserLabelName(Canvas.LegendLabel, null,
                        (string name) =>
                        {
                            return GetValue(name);
                        });
                text.Foreground = new SolidColorBrush(Colors.Black);
                panel.Children.Add(text);

                return panel;
            }
            return null;
        }

        /// <summary>
        /// 添加点的小圆圈，方便鼠标点中。并加提示
        /// </summary>
        /// <param name="center"></param>
        /// <param name="rotate"></param>
        protected void AddPoint(Point center, double rotate,string tooltip)
        {
            var circle = Common.Helper.CreateEllipse(center, rotate);
            circle.Stroke = this.Stroke;
            circle.Fill = this.Fill;
            ToolTipService.SetToolTip(circle, tooltip);
            Canvas.AddChild(circle);
        }

        /// <summary>
        /// 生成提示信息
        /// #Y=当前值，#YName=当前Y轴名称,#C{列名}=表示绑定当前数据对象的指定列值
        /// </summary>
        /// <returns></returns>
        protected string CreateTooltip(string yName,string yValue)
        {
            if (!string.IsNullOrWhiteSpace(this.ItemTooltipFormat))
            {
                var tmp = Common.Helper.DserLabelName(this.ItemTooltipFormat,
                    new System.Collections.Generic.Dictionary<string, string>() { { "YName", yName }, { "Y", yValue } }, 
                    (string name) =>
                    {
                        return GetValue(name);
                    });
                return tmp;
            }
            return this.ItemTooltipFormat;
        }

        
    }
}
