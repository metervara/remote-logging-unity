using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace metervara.logging
{
  public abstract class LogInterceptionBase<T> : MonoBehaviour where T : new()
  {
    [Tooltip("Filename of external config file if used (in Application.dataPath)")]
    public string externalConfigFile = "";
    public T settings;

    protected bool isReady = false;
    public UnityEvent readyEvent;
    protected bool IsReady {
      get {
        return isReady;
      }
      set
      {
        isReady = value;
        if (isReady)
        {
          if (readyEvent != null)
          {
            readyEvent.Invoke();
          }
          while (logQueue.Count > 0)
          {
            Log(logQueue.Dequeue());
          }
        }
      }
    }
    protected Queue<LogItem> logQueue = new Queue<LogItem>();
    public struct LogItem {
      public string logString;
      public string stackTrace;
      public LogType type;

      public LogItem(string logString, string stackTrace, LogType type)
      {
          this.logString = logString;
          this.stackTrace = stackTrace;
          this.type = type;
      }
    }

    void OnEnable()
    {
      Application.logMessageReceived += InterceptLog;
    }
    void OnDisable() {
      Application.logMessageReceived -= InterceptLog;
    }

    public virtual void Start() {
      if(!String.IsNullOrEmpty(externalConfigFile)) {
        settings = LoadSettings(externalConfigFile);
      }
    }

    protected abstract void Log(LogItem msg);

    private void InterceptLog(string logString, string stackTrace, LogType type) {
      if(!isReady) {
        logQueue.Enqueue(new LogItem(logString, stackTrace, type));
        return;
      }

      Log(new LogItem(logString, stackTrace, type));
    }

    private T LoadSettings(string fileName) {
      try
      {
        string filePath = System.IO.Path.Combine(Application.dataPath, fileName);
        string data = System.IO.File.ReadAllText(filePath);
        T externalSettings = JsonUtility.FromJson<T>(data);
        return externalSettings;
      }
      catch (System.Exception)
      {
        return new T(); // fallback with default settings
      }
    }
  }
}
