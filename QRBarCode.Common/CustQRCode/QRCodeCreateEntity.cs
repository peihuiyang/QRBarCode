using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace QRBarCode.Common.CustQRCode
{
    /// <summary>
    /// 功能描述    ：二维码创建实体
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-22 17:02:09 
    /// </summary>
    public class QRCodeCreateEntity
    {
        /// <summary>
        /// 内容实体
        /// </summary>
        public string ContextText { get; set; }
        /// <summary>
        /// 生成二维码图片的像素大小
        /// </summary>
        public int PixelsPerModule { get; set; } = 10;
        /// <summary>
        /// 暗色,格式为#FF000000
        /// </summary>
        public string DarkColor { get; set; } = "#FF000000";
        /// <summary>
        /// 亮色,格式为#FF000000
        /// </summary>
        public string LightColor { get; set; } = "#FFFFFFFF";
        /// <summary>
        /// 水印字节数组
        /// </summary>
        public byte[] IconSource { get; set; }
        /// <summary>
        /// 图标大小比例
        /// </summary>
        public int IconSizePercent { get; set; } = 15;
        /// <summary>
        /// 图标的边框
        /// </summary>
        public int IconBorderWidth { get; set; } = 6;
        /// <summary>
        /// 静止区，位于二维码某一边的空白边界,用来阻止读者获取与
        /// 正在浏览的二维码无关的信息 即是否绘画二维码的空白边框区域 默认为true
        /// </summary>
        public bool DrawQuietZones { get; set; } = true;
    }
}
