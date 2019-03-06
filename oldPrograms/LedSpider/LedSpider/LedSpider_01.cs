


using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace LedSpider
{
    public class Program
    {
        public OutputPort LedSpider = new OutputPort;
        public static void Main()
        {
            Debug.Print(Resources.GetString(Resources.StringResources.String1));
        }
    }
}
