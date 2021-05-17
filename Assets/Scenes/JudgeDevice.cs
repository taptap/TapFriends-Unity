using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace JudgeDevice
{
    public class Judge
    {
        public static bool IsIphoneXDevice = false;
        public static void JudgeDeviceModel()
        {
            var modelStr = SystemInfo.deviceModel;
            if (modelStr.Equals("iPhone10,1") || modelStr.Equals("iPhone10,2") || modelStr.Equals("iPhone10,3")
                || modelStr.Equals("iPhone10,4") || modelStr.Equals("iPhone10,5") || modelStr.Equals("iPhone10,6")
                || modelStr.Equals("iPhone11,2") || modelStr.Equals("iPhone11,6") || modelStr.Equals("iPhone11,8")
                || modelStr.Equals("iPhone12,1") || modelStr.Equals("iPhone12,3") || modelStr.Equals("iPhone12,5")
                || modelStr.Equals("iPhone12,8") || modelStr.Equals("iPhone13,1") || modelStr.Equals("iPhone13,2")
                || modelStr.Equals("iPhone13,3") || modelStr.Equals("iPhone13,4") ) {
                IsIphoneXDevice = true;
            }
            else
            {
                IsIphoneXDevice = false;
            }
        }
    }
}