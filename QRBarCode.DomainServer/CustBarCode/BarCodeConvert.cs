using QRBarCode.Common.CustBarCode;
using QRBarCode.EntityDto.CustBarCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRBarCode.DomainServer.CustBarCode
{
    /// <summary>
    /// 功能描述    ：
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-23 10:40:47 
    /// </summary>
    public class BarCodeConvert
    {
        public static BarCodeCreateEntity ChangeEntity(BarCodeCreateInput item)
        {
            BarCodeCreateEntity barCodeCreateEntity = new BarCodeCreateEntity
            {
                ContextText = item.ContextText
            };
            if (item.Width != 0)
            {
                barCodeCreateEntity.Width = item.Width;
            }
            if (item.Height != 0)
            {
                barCodeCreateEntity.Height = item.Height;
            }
            if (!string.IsNullOrWhiteSpace(item.BackColor))
            {
                barCodeCreateEntity.BackColor = item.BackColor;
            }
            if (!string.IsNullOrWhiteSpace(item.ForeColor))
            {
                barCodeCreateEntity.ForeColor = item.ForeColor;
            }
         
            return barCodeCreateEntity;
        }
    }
}
