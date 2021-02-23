using QRBarCode.Common.CustQRCode;
using QRBarCode.EntityDto.CustQRCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRBarCode.DomainServer.CustQRCode
{
    /// <summary>
    /// 功能描述    ：
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-22 17:11:38 
    /// </summary>
    public class QRCodeConvert
    {
        public static QRCodeCreateEntity ChangeEntity(QRCodeCreateInput item)
        {
            QRCodeCreateEntity qRCodeCreateEntity = new QRCodeCreateEntity
            {
                ContextText = item.ContextText,
                IconSource = item.IconSource
            };
            if (item.PixelsPerModule != 0)
            {
                qRCodeCreateEntity.PixelsPerModule = item.PixelsPerModule;
            }
            if (item.IconSizePercent != 0)
            {
                qRCodeCreateEntity.IconSizePercent = item.IconSizePercent;
            }
            if (item.IconBorderWidth != 0)
            {
                qRCodeCreateEntity.IconBorderWidth = item.IconBorderWidth;
            }
            if (!string.IsNullOrWhiteSpace(item.DarkColor))
            {
                qRCodeCreateEntity.DarkColor = item.DarkColor;
            }
            if (!string.IsNullOrWhiteSpace(item.LightColor))
            {
                qRCodeCreateEntity.LightColor = item.LightColor;
            }
            if (!item.DrawQuietZones)
            {
                qRCodeCreateEntity.DrawQuietZones = item.DrawQuietZones;
            }
            return qRCodeCreateEntity;
        }
    }
}
