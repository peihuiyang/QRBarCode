using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peihui.Core.Response;
using QRBarCode.Api.Configuration.Filters;
using QRBarCode.EntityDto.CustBarCode;
using QRBarCode.IDomainServer.CustBarCode;

namespace QRBarCode.Api.Controllers.v1
{
    [Route("api/qrbarcode/v1/barcode")]
    [ApiController]
    [SimpleAuthorizationFilter]
    public class BarCodeController : ControllerBase
    {
        private readonly IBarCodeDomain _barCodeDomain;

        public BarCodeController(IBarCodeDomain barCodeDomain)
        {
            _barCodeDomain = barCodeDomain;
        }

        /// <summary>
        /// 本地测试--将二维码生成图片
        /// </summary>
        /// <returns></returns>
        [HttpPost("test")]
        public ResponseResult Test([FromBody] BarCodeCreateInput barCodeCreateInput)
        {
            return _barCodeDomain.Test(barCodeCreateInput);
        }
        /// <summary>
        /// 生成条形码为字节数组
        /// </summary>
        /// <param name="barCodeCreateInput"></param>
        /// <returns></returns>
        [HttpPost("savetobytes")]
        public ResponseResult<byte[]> SaveToBytes([FromBody] BarCodeCreateInput barCodeCreateInput)
        {
            return _barCodeDomain.SaveToBytes(barCodeCreateInput);
        }
        /// <summary>
        /// 生成条形码为base64
        /// </summary>
        /// <param name="barCodeCreateInput"></param>
        /// <returns></returns>
        [HttpPost("savetobase64")]
        public ResponseResult<string> SaveToBase64([FromBody] BarCodeCreateInput barCodeCreateInput)
        {
            return _barCodeDomain.SaveToBase64(barCodeCreateInput);
        }
    }
}
