using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace SensorReader
{
    public class SensorReaderLibWrapper
    {
        private const string JcPath = "com.jaeguins.sensorreader.SensorReader",
            initialize = "Initialize",
            start = "Start",
            checkRunning = "CheckRunning",
            end = "End",
            get = "Get";
        static AndroidJavaClass jc = new AndroidJavaClass(JcPath);
        public static void Initialize()
        {
#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
            jc.CallStatic(initialize, context);
#else
            Debug.LogError("Non-Android is not supported");
#endif
        }

        public static string Start(SensorType type)
        {
#if UNITY_ANDROID
            return jc.CallStatic<string>(start, (int)type);
#else
            Debug.LogError("Non-Android is not supported");
            return "Non-Android is not supported";
#endif
        }

        public static bool IsRunning(SensorType type)
        {
#if UNITY_ANDROID
            return jc.CallStatic<bool>(checkRunning);
#else
            Debug.LogError("Non-Android is not supported");
            return false;
#endif
        }
        public static float Get(SensorType type, int index)
        {
#if UNITY_ANDROID
            return jc.CallStatic<float>(get, (int) type, index);
#else
            Debug.LogError("Non-Android is not supported");
            return -1f;
#endif
        }

        public static string End(SensorType type)
        {
#if UNITY_ANDROID
            return jc.CallStatic<string>(end, (int) type);
#else
            Debug.LogError("Non-Android is not supported");
            return "Non-Android is not supported";
#endif
        }
    }
    public enum SensorType
    {
        Accelerometer = 1,
        MagneticField = 2,
        Orientation = 3,
        Gyroscope = 4,
        Light = 5,
        Pressure = 6,
        Temperature = 7,
        Proximity = 8,
        Gravity = 9,
        LinearAcceleration = 10,
        RotationVector = 11,
        RelativeHumidity = 12,
        AmbientTemperature = 13,
        MagneticFieldUncalibrated = 14,
        GameRotationVector = 15,
        GyroscopeUncalibrated = 16,
        SignificantMotion = 17,
        StepDetector = 18,
        StepCounter = 19,
        GeomagneticRotationVector = 20,
        HeartRate = 21,
        AccelerometerUncalibrated = 35,
        DevicePrivateBase = 65536,
        LowLatencyOffBodyDetect = 34,
        MotionDetect = 30,
        Pose6Dof = 28,
        StationaryDetect = 29,
        HeartBeat = 31,
        All = -1,
    }
}
