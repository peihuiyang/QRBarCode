using Peihui.Core.Response;
using QRBarCode.EntityDto.CustQRCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRBarCode.IDomainServer.CustQRCode
{
    /// <summary>
    /// 功能描述    ：二维码业务接口
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-22 15:58:01 
    /// 最后修改者  ：sh
    /// 最后修改日期：2021-02-22 15:58:01 
    /// </summary>
    public interface IQRCodeDomain
    {
        /// <summary>
        /// 生成二维码字节数组
        /// </summary>
        /// <param name="qRCodeCreateInput"></param>
        /// <returns></returns>
        ResponseResult<byte[]> CreateToBytes(QRCodeCreateInput qRCodeCreateInput);
        ResponseResult Test(QRCodeCreateInput qRCodeCreateInput);
        ResponseResult<string> CreateToBase64(QRCodeCreateInput qRCodeCreateInput);
    }
}
