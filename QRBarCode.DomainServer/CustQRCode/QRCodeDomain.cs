using Peihui.Core.Response;
using QRBarCode.Common.CustQRCode;
using QRBarCode.EntityDto.CustQRCode;
using QRBarCode.IDomainServer.CustQRCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRBarCode.DomainServer.CustQRCode
{
    /// <summary>
    /// 功能描述    ：
    /// 创 建 者    ：Yang Peihui
    /// 创建日期    ：2021-02-22 16:11:18 
    /// </summary>
    public class QRCodeDomain : IQRCodeDomain
    {
        public ResponseResult<string> CreateToBase64(QRCodeCreateInput qRCodeCreateInput)
        {
            QRCodeCreateEntity qRCodeCreateEntity = QRCodeConvert.ChangeEntity(qRCodeCreateInput);
            string result = QRCoderHelper.CreateQRCodeToBase64(qRCodeCreateEntity);
            return ResponseResult<string>.Success(result);
        }

        public ResponseResult<byte[]> CreateToBytes(QRCodeCreateInput qRCodeCreateInput)
        {
            QRCodeCreateEntity qRCodeCreateEntity = QRCodeConvert.ChangeEntity(qRCodeCreateInput);
            byte[] result = QRCoderHelper.CreateQRCodeToBytes(qRCodeCreateEntity);
            return ResponseResult<byte[]>.Success(result);
        }

        public ResponseResult Test(QRCodeCreateInput qRCodeCreateInput)
        {
            QRCodeCreateEntity qRCodeCreateEntity = QRCodeConvert.ChangeEntity(qRCodeCreateInput);
            string fileName = QRCoderHelper.CreateQRCodeToFile(qRCodeCreateEntity);
            return ResponseResult.Success(fileName);
        }
    }
}
