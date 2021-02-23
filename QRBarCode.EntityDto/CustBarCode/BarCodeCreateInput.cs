using System;
using System.Collections.Generic;
using System.Text;

namespace QRBarCode.EntityDto.CustBarCode
{
    /// <summary>
    /// 功能描述    ：
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-23 10:22:27 
    /// </summary>
    public class BarCodeCreateInput
    {
        /// <summary>
        /// 内容实体
        /// </summary>
        public string ContextText { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 背景色,格式为#FF000000
        /// </summary>
        public string BackColor { get; set; }
        /// <summary>
        /// 条形色,格式为#FF000000
        /// </summary>
        public string ForeColor { get; set; }
    }
}
