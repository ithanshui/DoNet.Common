using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace slExample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var data = new List<Silverlight.Common.Reflection.DataCollection>();
            data.Add(new Silverlight.Common.Reflection.DataCollection());
            for (var i = 0; i < 10; i++)
            {
                
                var item = new Silverlight.Common.Reflection.DataItem() { 
                 Key="key" + i,
                  Value=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                   DataType= typeof(DateTime)
                };
                data[0].Add(item);
            }

            var list = Silverlight.Common.Reflection.DynamicHelper.ConvertToList(data);

            var dic = new System.Collections.Generic.Dictionary<string, string>();
            dic.Add("id", "123");
            dic.Add("name", "jm");


            var json = Silverlight.Common.Serialization.Json.Parse(dic);
            var item2= new Silverlight.Common.Reflection.DataItem();
            item2.Value = json;
            item2.Key = "key";
            json = Silverlight.Common.Serialization.Json.Parse(item2);

            var c = new JMChart.RadarCanvas();
            chart1.CurrentCanvas = c;

            button1_Click(null, null);//首次画出默认的

            //textcmb.Items.Add("test1");
            //textcmb.Items.Add("test2");


            //Telerik.Windows.Controls.RadHtmlPlaceholder p = new Telerik.Windows.Controls.RadHtmlPlaceholder();
           //this.htmlweb.HtmlSource = "<a href=\"http://www.jm47.com\">test</a><b>tafasfsaf</b>";
            //p.Width = 200;
            //p.Height = 100;
            //this.stackPanel1.Children.Add(p);

            datetimepicker.IsDropDownOpen = true;
          
            datetimepicker.SelectedValue = DateTime.Now;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var data = new List<DataClass>();
            var rnd = new Random();
            for (var i = 0; i < 3; i++)
            {
                var d = new DataClass();
                d.我是测试的属性0 = i + rnd.Next(400);
                d.我是测试的属性 = i + rnd.Next(400);
                d.我是测试的属性2 = i + rnd.Next(400);
                d.c4 = i + rnd.Next(400);
                d.c5 = i + rnd.Next(400);

                data.Add(d);

                //var item = new TabItem();
                //item.DataContext = d;
                //tsTab.Items.Add(item);
            }

            chart1.CurrentCanvas.YValueNames = new string[] { "我是测试的属性0", "我是测试的属性", "我是测试的属性2", "c4", "c5" };
            chart1.CurrentCanvas.YLabels = new string[] {"测试：#YName","随机:#YName" };

            //数据点的tooltip,如果只有一个就所有的都按这个格式。否则为对应索引的
            chart1.CurrentCanvas.ItemLabels = new string[] { txttooltip.Text };
            chart1.CurrentCanvas.LegendLabel = txtlegend.Text;
            chart1.CurrentCanvas.LegendLabelPosition = (JMChart.EnumLegendLabelPosition)cmbLegendPosition.SelectedIndex;

            chart1.CurrentCanvas.IsAnimate = chkAni.IsChecked.Value;
            chart1.CurrentCanvas.IsFillShape = chkFill.IsChecked.Value;

            chart1.CurrentCanvas.LineWidth = cmbLineW.SelectedIndex + 1;

            //当线条少于5条时。无法画多边形。因为四条就是一个正方形。所以会默认用圆形。
            (chart1.CurrentCanvas as JMChart.RadarCanvas).IsCircle = chkCircle.IsChecked.Value;

            chart1.DataContext = data;

            chart1.Draw();

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           //textcmb.Text += ",a";
        }
    }

    public class DataClass
    {
        public double 我是测试的属性0 { get; set; }

        public double 我是测试的属性 { get; set; }

        public double 我是测试的属性2 { get; set; }

        public double c4 { get; set; }
        public double c5 { get; set; }
    }
}
