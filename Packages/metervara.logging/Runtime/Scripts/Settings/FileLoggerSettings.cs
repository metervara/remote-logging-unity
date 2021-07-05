using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metervara.logging
{
  [System.Serializable]
  public class FileLoggerSettings
  {
    public string itemDelimiter = "|";
    public string timestampFormat = "yyyy-MM-dd HH:mm:ss.fff \"GMT\"zzz";

    [Tooltip("Relative to Application.dataPath")]
    public string logPath = "Logs";

    [Tooltip("How long logs are saved. <=0 keeps logs forever")]
    public int logFileRetentionDays = -1;
  }
}