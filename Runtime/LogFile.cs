using System;
using System.IO;
using UnityEngine;

namespace SerialPackage.Runtime
{
    class LogFile
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Awake()
        {
            if (PlayerPrefs.GetInt("CreateLogFile", 0) == 0)
            {
                return;
            }

            if (File.Exists(Application.temporaryCachePath + "/Log.txt"))
            {
                File.Move(Application.temporaryCachePath + "/Log.txt", Application.temporaryCachePath + "/Log_archive" + UnityEngine.Random.Range(0, 999999) + ".txt");
            }

            File.Create(Application.temporaryCachePath + "/Log.txt");

            Application.logMessageReceived += HandleLog;

            Debug.Log("(LogFile) CreateLogFile enabled.");
        }

        static void HandleLog(string logString, string stackTrace, LogType type)
        {
            File.AppendAllText(Application.temporaryCachePath + "/Log.txt", type.ToString() + Environment.NewLine + logString + Environment.NewLine + stackTrace);
        }
    }
}