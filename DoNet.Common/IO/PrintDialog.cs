//////////////////////////////////////////////////
// Author   : jiamao
// Date     : 2010/09/15
// Usage    : 打印机操作类
//////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace DoNet.Common.IO
{
    public class PrinterDialog
    {
         PrintDocument _printdoc = new PrintDocument();
        PrintDialog _printdialog = new PrintDialog();
        PrintPreviewDialog _printpredialog = new PrintPreviewDialog();
        List<print_POJO> _printlist = new List<print_POJO>();

        public PrinterDialog()
        {
            _printdoc.PrintPage +=  new PrintPageEventHandler(print_print);
            _printdialog.Document = _printdoc;
            _printpredialog.Document = _printdoc;
            print_Width = 350;
        }

        
        /// <summary>
        /// 所有要打印的对象
        /// </summary>
        public List<print_POJO> All_Prints
        {
            get { return _printlist; }
            set { _printlist = value; }
        }

        int p_w = 0;
        /// <summary>
        /// 打印的宽度
        /// </summary>
        public int print_Width
        {
            get { return p_w; }
            set { p_w = value; }
        }
        public struct print_POJO
        {
            public Font f;
            public string value;
            public string textalgin;
            public Brush brush;
            public Point location;
        }
        /// <summary>
        /// rs 打印
        /// </summary>
        public void print()
        {
            _printdialog.ShowDialog();
        }
        /// <summary>
        /// 打印预览
        /// </summary>
        public void print_Preview()
        {
            _printpredialog.ShowDialog();
        }
        private void print_print(object sender,PrintPageEventArgs pe)
        {
            int print_Y = 3;//打印当前行的Y坐标
            int print_X = 8;//打印当前行的Y坐标
            foreach (print_POJO pp in _printlist)
            {
                SizeF sf = pe.Graphics.MeasureString(pp.value, pp.f);
                if (pp.textalgin == "left")
                {
                    pe.Graphics.DrawString(pp.value, pp.f, pp.brush, new Point(print_X,print_Y));//左对齐
                    
                }
                else if (pp.textalgin == "center")
                {
                    int w = (int)sf.Width;
                    pe.Graphics.DrawString(pp.value, pp.f, pp.brush, new Point((print_Width - w) / 2, print_Y));//左对齐
                }
                else
                {
                    int w = (int)sf.Width;
                    pe.Graphics.DrawString(pp.value, pp.f, pp.brush, new Point(print_Width - w, print_Y));//右对齐
                }
                print_Y += (int)sf.Height + 3;//移至下一行
            }
        }
    }
}
