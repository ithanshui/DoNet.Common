using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Linq;
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
    /// 图例位置
    /// </summary>
    public enum EnumLegendLabelPosition
    {         
       Left,
        Top,
        Right,
        Bottom
    }
    /// <summary>
    /// 基础画布
    /// </summary>
    public abstract class ChartCanvas
    {
        public ChartCanvas() {
            init();
        }

        public ChartCanvas(Panel parent)
        {
            init();
            Parent = parent;
        }

        /// <summary>
        /// 初始化默认值
        /// </summary>
        private void init() {           
            HorizontalCount = 3;
            VerticalCount = 5;
            LineWidth = 1;
            ForeColor = new SolidColorBrush(Colors.Black);
            Axises = new ObservableCollection<IAxis>();
            Serieses = new ObservableCollection<Series.ISeries>();

            IsAnimate = true;
            IsFillShape = false;

            LegendPanel = new StackPanel();
            LegendPanel.Margin = new Thickness(10);

            LegendSize = new Size(100,100);
        }

        /// <summary>
        /// 当前线条颜色集合
        /// </summary>
        protected internal Color[] SeriesColors = new Color[] 
        { 
            Colors.Green,  
            Colors.Orange,
            Colors.Blue, 
            Colors.Red,  
             Color.FromArgb(255,116,192,211),
             Colors.Yellow,
            Colors.Purple,
            Colors.Magenta,
            Colors.Brown
        };       

        /// <summary>
        /// 基本画布
        /// </summary>
        public Panel Parent { get; set; }

        /// <summary>
        /// 图例容器
        /// </summary>
        public StackPanel LegendPanel
        {
            get;
            internal set;
        }

        /// <summary>
        /// 图例区域大小
        /// </summary>
        public Size LegendSize { get; set; }

        /// <summary>
        /// 基线颜色
        /// </summary>
        public Brush ForeColor { get; set; }

        /// <summary>
        /// 是否为封闭的图形
        /// </summary>
        public bool IsFillShape { get; set; }

        /// <summary>
        /// 所有轴
        /// </summary>
        public ObservableCollection<IAxis> Axises { get; internal set; }

        /// <summary>
        /// 线或图信息
        /// </summary>
        public ObservableCollection<Series.ISeries> Serieses { get; set; }

        object dataContext;
        /// <summary>
        /// 数据源
        /// </summary>
        public object DataContext {
            get {
                return dataContext;
            }
            internal set {
                dataContext = value;
                InitSeries(dataContext);
            }
        }

        /// <summary>
        /// 初始化图形
        /// </summary>
        /// <param name="data"></param>
        internal virtual void InitSeries(object data)
        {
            if (Serieses == null) Serieses = new ObservableCollection<Series.ISeries>();
            else Serieses.Clear();            
        }

        /// <summary>
        /// 绑定到Y轴的属性名
        /// </summary>
        public string[] YValueNames { get; set; }

        /// <summary>
        /// 点的提示说明格式
        /// #Y=当前值，#YName=当前Y轴名称,#C{列名}=表示绑定当前数据对象的指定列值
        /// </summary>
        public string[] ItemLabels { get; set; }

        /// <summary>
        /// 图例格式，
        /// 默认为字段名
        /// </summary>
        public string LegendLabel { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public double Width
        {
            get;
            set;
        }

        /// <summary>
        /// 高
        /// </summary>
        public double Height
        {
            get;
            set;
        }

        /// <summary>
        /// 图表区域大小
        /// </summary>
        public Size GraphSize { get; set; }

        /// <summary>
        /// 线宽
        /// </summary>
        public double LineWidth { get; set; }

        /// <summary>
        /// 横线条数
        /// </summary>
        public int HorizontalCount
        {
            get;
            set;
        }

        int vcount = 0;
        /// <summary>
        /// 纵线条数
        /// </summary>
        public int VerticalCount { get {
            if (YValueNames != null)
            {
                return YValueNames.Length;
            }
            return vcount;
        }
            set { vcount = value; }
        }

        /// <summary>
        /// 是否使用动画效果
        /// </summary>
        public bool IsAnimate { get; set; }

        /// <summary>
        /// 图例位置
        /// </summary>
        public EnumLegendLabelPosition LegendLabelPosition
        {
            get;
            set;
        }

        ObservableCollection<FrameworkElement> children = new System.Collections.ObjectModel.ObservableCollection<FrameworkElement>();
        /// <summary>
        /// 子控件
        /// </summary>
        public ObservableCollection<FrameworkElement> Children
        {
            get
            {
                return children;
            }
            internal set
            {
                children = value;
            }
        }

        /// <summary>
        /// 重新清除前面已生成的对象
        /// </summary>
        public void Reset()
        {
            foreach (var c in Children)
            {
                if (c is FrameworkElement)
                {
                    var fe = c as FrameworkElement;
                    if (fe.Parent != null)
                    {
                        this.Parent.Children.Remove(fe);
                    }
                }
            }

            Children.Clear();
            Axises.Clear();
            //计算布局
            SetCanvasLayout();
        }

        /// <summary>
        /// 展现
        /// </summary>
        public virtual void Draw()
        {
            foreach (var c in Children)
            {
                if (c is FrameworkElement)
                {
                    var fe = c as FrameworkElement;
                    if (fe.Parent == null && 
                        this.Parent != null && 
                        !this.Parent.Children.Contains(fe))
                    {
                        this.Parent.Children.Add(fe);
                    }
                }

                if (c is Common.IBaseControl)
                {
                    ((Common.IBaseControl)c).Draw();
                }
            }

            this.LegendPanel.Children.Clear();
            ///处理图表
            foreach (var item in this.Serieses)
            {
                item.Draw();

                var legend=item.CreateLegend();
                if (legend != null)
                {
                    this.LegendPanel.Children.Add(legend);
                }
            }
        }

        /// <summary>
        /// 添加 子控件
        /// </summary>
        /// <param name="el"></param>
        public void AddChild(FrameworkElement fe)
        {
            if (!Children.Contains(fe))
            {
                Children.Add(fe);

                if (fe.Parent == null && this.Parent != null && !this.Parent.Children.Contains(fe))
                {
                    this.Parent.Children.Add(fe);
                }
            }
        }

        /// <summary>
        /// 计算画布位置
        /// </summary>
        protected virtual void SetCanvasLayout()
        {
        }
    }
}
