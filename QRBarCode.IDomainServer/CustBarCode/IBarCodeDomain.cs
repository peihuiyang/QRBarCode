using Peihui.Core.Response;
using QRBarCode.EntityDto.CustBarCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRBarCode.IDomainServer.CustBarCode
{
    /// <summary>
    /// 功能描述    ：一维码生成接口
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-23 10:36:18 
    /// 最后修改者  ：sh
    /// 最后修改日期：2021-02-23 10:36:18 
    /// </summary>
    public interface IBarCodeDomain
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="barCodeCreateInput"></param>
        /// <returns></returns>
        ResponseResult Test(BarCodeCreateInput barCodeCreateInput);
        /// <summary>
        /// 生成条形码为字节数组
        /// </summary>
        /// <param name="barCodeCreateInput"></param>
        /// <returns></returns>
        ResponseResult<byte[]> SaveToBytes(BarCodeCreateInput barCodeCreateInput);
        /// <summary>
        /// 生成条形码为Base64
        /// </summary>
        /// <param name="barCodeCreateInput"></param>
        /// <returns></returns>
        ResponseResult<string> SaveToBase64(BarCodeCreateInput barCodeCreateInput);
    }
}
