using System.ComponentModel;

namespace CybageTest.Models
{
    public class ResponseModel
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        [DefaultValue(false)]
        public bool IsSuccess { get; set; }
    }
}
