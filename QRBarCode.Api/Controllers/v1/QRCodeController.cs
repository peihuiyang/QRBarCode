using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peihui.Core.Response;
using QRBarCode.Api.Configuration.Filters;
using QRBarCode.EntityDto.CustQRCode;
using QRBarCode.IDomainServer.CustQRCode;

namespace QRBarCode.Api.Controllers.v1
{
    [Route("api/qrbarcode/v1/qrcode")]
    [ApiController]
    [SimpleAuthorizationFilter]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRCodeDomain _qRCodeDomain;

        public QRCodeController(IQRCodeDomain qRCodeDomain)
        {
            _qRCodeDomain = qRCodeDomain;
        }

        /// <summary>
        /// 本地测试--将二维码生成图片
        /// </summary>
        /// <returns></returns>
        [HttpPost("test")]
        public ResponseResult Test([FromBody] QRCodeCreateInput qRCodeCreateInput)
        {
            return _qRCodeDomain.Test(qRCodeCreateInput);
        }
        /// <summary>
        /// 生成二维码字节数组
        /// </summary>
        /// <param name="qRCodeCreateInput"></param>
        /// <returns></returns>
        [HttpPost("createtobyte")]
        public ResponseResult<byte[]> CreateToBytes([FromBody] QRCodeCreateInput qRCodeCreateInput)
        {
            return _qRCodeDomain.CreateToBytes(qRCodeCreateInput);
        }
        /// <summary>
        /// 生成二维码Base64
        /// </summary>
        /// <param name="qRCodeCreateInput"></param>
        /// <returns></returns>
        [HttpPost("createtobase64")]
        public ResponseResult<string> CreateToBase64([FromBody] QRCodeCreateInput qRCodeCreateInput)
        {
            return _qRCodeDomain.CreateToBase64(qRCodeCreateInput);
        }
    }
}
