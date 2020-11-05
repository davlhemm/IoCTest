using System;

namespace IoCWebAppAspCore.Models
{
    public class TestViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
