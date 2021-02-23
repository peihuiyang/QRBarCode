using Peihui.Core.Response;
using QRBarCode.Common.CustBarCode;
using QRBarCode.EntityDto.CustBarCode;
using QRBarCode.IDomainServer.CustBarCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRBarCode.DomainServer.CustBarCode
{
    /// <summary>
    /// 功能描述    ：
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-23 10:37:43 
    /// </summary>
    public class BarCodeDomain : IBarCodeDomain
    {
        public ResponseResult<string> SaveToBase64(BarCodeCreateInput barCodeCreateInput)
        {
            BarCodeCreateEntity barCodeCreateEntity = BarCodeConvert.ChangeEntity(barCodeCreateInput);
            string result = BarCodeHelper.SaveCodeToBase64(barCodeCreateEntity);
            return ResponseResult<string>.Success(result);
        }

        public ResponseResult<byte[]> SaveToBytes(BarCodeCreateInput barCodeCreateInput)
        {
            BarCodeCreateEntity barCodeCreateEntity = BarCodeConvert.ChangeEntity(barCodeCreateInput);
            byte[] result = BarCodeHelper.SaveCodeToBytes(barCodeCreateEntity);
            return ResponseResult<byte[]>.Success(result);
        }

        public ResponseResult Test(BarCodeCreateInput barCodeCreateInput)
        {
            BarCodeCreateEntity barCodeCreateEntity = BarCodeConvert.ChangeEntity(barCodeCreateInput);
            string filename = BarCodeHelper.SaveCode(barCodeCreateEntity);
            return ResponseResult.Success($"条形码存储成功：{filename}");
        }
    }
}
