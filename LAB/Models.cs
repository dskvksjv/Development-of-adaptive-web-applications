using System.Collections.Generic;

namespace CryptoCompareAPI
{
    public class CoinInfo
    {
        public string Symbol { get; set; }
        public double TotalVolume24H { get; set; }
    }

    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public List<T> Data { get; set; }
    }
}
