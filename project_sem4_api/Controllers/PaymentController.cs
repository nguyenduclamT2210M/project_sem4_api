using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using project_sem4_api.Payment;
using System.Security.Cryptography;
using System.Text;

namespace project_sem4_api.Controllers
{
    public class PaymentController : Controller
    {
        private readonly string _vnpTmCode;
        private readonly string _vnpHashSecret;
        private readonly string _vnpUrl;
        private readonly string _vnpReturnUrl;

        public PaymentController(IOptions<VNPaySettings> vnPaySettings)
        {
            _vnpTmCode = vnPaySettings.Value.VnpTmCode;
            _vnpHashSecret = vnPaySettings.Value.VnpHashSecret;
            _vnpUrl = vnPaySettings.Value.VnpUrl;
            _vnpReturnUrl = vnPaySettings.Value.VnpReturnUrl;
        }

        [HttpPost("create-payment")]
        public IActionResult CreatePayment([FromBody] PaymentRequest paymentRequest)
        {
            var vnpParams = new Dictionary<string, string>
            {
                { "vnp_Version", "2.0.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", _vnpTmCode },
                { "vnp_Amount", (paymentRequest.Amount * 25000).ToString() }, // Amount in VND (cent)
                { "vnp_CurrCode", "VND" },
                { "vnp_TxnRef", paymentRequest.TransactionId.ToString() },
                { "vnp_OrderInfo", paymentRequest.OrderInfo },
                { "vnp_Locale", "vn" },
                { "vnp_ReturnUrl", _vnpReturnUrl },
                { "vnp_IpAddr", GetIpAddress() },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
            };

            // Generate Secure Hash
            string queryString = string.Join("&", vnpParams.Select(kv => kv.Key + "=" + kv.Value));
            string hashData = queryString + "&vnp_SecureHashType=SHA256";
            string secureHash = GetSecureHash(hashData, _vnpHashSecret);
            vnpParams.Add("vnp_SecureHash", secureHash);

            var paymentUrl = _vnpUrl + "?" + string.Join("&", vnpParams.Select(kv => kv.Key + "=" + kv.Value));
            return Ok(new { paymentUrl });
        }

        private string GetIpAddress()
        {
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
        }

        private string GetSecureHash(string data, string secret)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(secret + data);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        // Return URL after payment (called by VNPay)
        [HttpGet("vnpay-return")]
        public IActionResult VnPayReturn([FromQuery] Dictionary<string, string> vnpParams)
        {
            var vnpSecureHash = vnpParams["vnp_SecureHash"];
            vnpParams.Remove("vnp_SecureHash");

            // Check the Secure Hash
            string hashData = string.Join("&", vnpParams.Select(kv => kv.Key + "=" + kv.Value));
            string secureHash = GetSecureHash(hashData, _vnpHashSecret);

            if (vnpSecureHash == secureHash)
            {
                // If hash is valid, process payment
                var transactionStatus = vnpParams["vnp_ResponseCode"];
                if (transactionStatus == "00")
                {
                    // Payment successful
                    return Ok("Thanh toán thành công");
                }
                else
                {
                    // Payment failed
                    return BadRequest("Thanh toán thất bại");
                }
            }
            else
            {
                // Invalid secure hash
                return BadRequest("Mã xác thực không hợp lệ");
            }
        }
    }
}
