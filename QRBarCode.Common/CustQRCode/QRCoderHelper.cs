using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace QRBarCode.Common.CustQRCode
{
    /// <summary>
    /// 功能描述    ：二维码公共帮助类
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-22 15:05:37 
    /// </summary>
    public static class QRCoderHelper
    {
        /// <summary>
        /// 创建二维码返回文件路径名称
        /// </summary>
        /// <param name="qRCodeCreateEntity">二维码内容</param>
        public static string CreateQRCodeToFile(QRCodeCreateEntity qRCodeCreateEntity)
        {
            try
            {
                // => 设置生成文件相关信息
                string fileName = "";
                //二维码文件目录
                string filePath = @"C:/Images/QR/";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //创建二维码文件路径名称
                fileName = filePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";

                // => 创建二维码
                Bitmap qrCodeImage = CreateCodeToBitmap(qRCodeCreateEntity);
                qrCodeImage.Save(fileName, ImageFormat.Jpeg);
                return fileName;
            }
            catch (Exception ex)
            {
                throw new Exception("创建二维码返回文件路径名称方法异常", ex);
            }
        }

        /// <summary>
        /// 创建二维码返回byte数组
        /// </summary>
        /// <param name="qRCodeCreateEntity">二维码内容</param>
        public static byte[] CreateQRCodeToBytes(QRCodeCreateEntity qRCodeCreateEntity)
        {
            try
            {
                MemoryStream ms = ChangeToStream(qRCodeCreateEntity);

                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();

                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception("创建二维码返回byte数组方法异常", ex);
            }
        }

        /// <summary>
        /// 创建二维码返回Base64字符串
        /// </summary>
        /// <param name="plainText">二维码内容</param>
        public static string CreateQRCodeToBase64(QRCodeCreateEntity qRCodeCreateEntity)
        {
            try
            {
                string result = "";
                MemoryStream ms = ChangeToStream(qRCodeCreateEntity);

                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                if (qRCodeCreateEntity.DrawQuietZones)
                {
                    result = "data:image/jpeg;base64," + Convert.ToBase64String(arr);
                }
                else
                {
                    result = Convert.ToBase64String(arr);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("创建二维码返回Base64字符串方法异常", ex);
            }
        }

        /// <summary>
        /// 二维码将Bitmap转化为Stream
        /// </summary>
        /// <param name="qRCodeCreateEntity"></param>
        /// <returns></returns>
        public static MemoryStream ChangeToStream(QRCodeCreateEntity qRCodeCreateEntity)
        {
       
            Bitmap qrCodeImage = CreateCodeToBitmap(qRCodeCreateEntity);
            MemoryStream memoryStream = new MemoryStream();
            qrCodeImage.Save(memoryStream, ImageFormat.Jpeg);
            return memoryStream;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="qRCodeCreateEntity"></param>
        /// <returns></returns>
        public static Bitmap CreateCodeToBitmap(QRCodeCreateEntity qRCodeCreateEntity)
        {
            Bitmap icon = null;
            if (string.IsNullOrEmpty(qRCodeCreateEntity.ContextText))
            {
                return null;
            }
            // 如果有Icon则添加
            if (qRCodeCreateEntity.IconSource != null && qRCodeCreateEntity.IconSource.Length > 0)
            {
                Stream stream = new MemoryStream(qRCodeCreateEntity.IconSource);
                icon = new Bitmap(stream);
            }
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeGenerator.ECCLevel:纠错能力,Q级：约可纠错25%的数据码字
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qRCodeCreateEntity.ContextText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrCodeData);

            // 将十六进制颜色码转化为Color对象
            Color darkColor = ColorTranslator.FromHtml(qRCodeCreateEntity.DarkColor);
            Color lightColor = ColorTranslator.FromHtml(qRCodeCreateEntity.LightColor);

            // 生成二维码
            Bitmap qrCodeImage = qrcode.GetGraphic(qRCodeCreateEntity.PixelsPerModule, darkColor, lightColor, 
                icon, qRCodeCreateEntity.IconSizePercent,qRCodeCreateEntity.IconBorderWidth, qRCodeCreateEntity.DrawQuietZones);

            return qrCodeImage;
        }
    }
}
