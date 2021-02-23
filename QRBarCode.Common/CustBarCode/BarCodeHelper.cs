using BarcodeLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace QRBarCode.Common.CustBarCode
{
    /// <summary>
    /// 功能描述    ：条形码生成帮助类
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-23 09:43:37 
    /// </summary>
    public class BarCodeHelper
    {
        /// <summary>
        /// 保存条形码为Image
        /// </summary>
        /// <param name="barCodeCreateEntity"></param>
        /// <returns></returns>
        public static Image CreateToImage(BarCodeCreateEntity barCodeCreateEntity)
        {

            var barcode = new Barcode()
            {
                IncludeLabel = true,
                Alignment = AlignmentPositions.CENTER,
                Width = barCodeCreateEntity.Width,
                Height = barCodeCreateEntity.Height,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = ColorTranslator.FromHtml(barCodeCreateEntity.BackColor),
                ForeColor = ColorTranslator.FromHtml(barCodeCreateEntity.ForeColor)
            };
            return barcode.Encode(TYPE.CODE39, barCodeCreateEntity.ContextText);
        }
        /// <summary>
        /// 保存为文件
        /// </summary>
        /// <param name="barCodeCreateEntity"></param>
        /// <returns></returns>
        public static string SaveCode(BarCodeCreateEntity barCodeCreateEntity)
        {
            Image image = CreateToImage(barCodeCreateEntity);
            //二维码文件目录
            string filePath = @"C:/Images/Bar/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // => 设置生成文件相关信息
            //创建二维码文件路径名称
            string fileName = filePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            image.Save(fileName, ImageFormat.Jpeg);
            return fileName;
        }
        /// <summary>
        /// 保存为字节数组
        /// </summary>
        /// <param name="barCodeCreateEntity"></param>
        /// <returns></returns>
        public static byte[] SaveCodeToBytes(BarCodeCreateEntity barCodeCreateEntity)
        {
            Image image = CreateToImage(barCodeCreateEntity);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            byte[] result = memoryStream.GetBuffer();
            return result;
        }
        /// <summary>
        /// 保存为base64
        /// </summary>
        /// <param name="barCodeCreateEntity"></param>
        /// <returns></returns>
        public static string SaveCodeToBase64(BarCodeCreateEntity barCodeCreateEntity)
        {
            byte[] bytes = SaveCodeToBytes(barCodeCreateEntity);
            string result = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
            return result;
        }
    }
}
