using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace metervara.logging
{
  public class FileLogger : LogInterceptionBase<FileLoggerSettings>
  { 
    protected string LogPath
    {
      get
      {
        return Path.Combine(Application.persistentDataPath, settings.logPath);
      }
    }
    public override void Start()
    {
      base.Start();
      try
      {
        System.IO.Directory.CreateDirectory(LogPath);
        IsReady = true;
      }
      catch
      {
        enabled = false;
      }
    }

    override protected void Log(LogItem msg)
    {
      string day = System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd");
      string fileName = Path.Combine(LogPath, $"{day}.log");
      string timestamp = System.DateTime.Now.ToLocalTime().ToString(settings.timestampFormat); // TODO: Fix timestamp will not be correct for queued messages

      string message = $"{timestamp} {settings.itemDelimiter} {msg.type.ToString()} {settings.itemDelimiter} {msg.logString}";

      try
      {
        System.IO.File.AppendAllText(fileName, message + "\n");
      }
      catch { }
    }

    private void RemoveOldLogs()
    {
      if (settings.logFileRetentionDays <= 0)
      {
        return;
      }
      string path = LogPath;
      if (String.IsNullOrEmpty(path))
      {
        return;
      }

      try
      {
        string[] files = Directory.GetFiles(path);

        foreach (string file in files)
        {
          FileInfo fi = new FileInfo(file);
          if (fi.CreationTime < DateTime.Now.AddDays(-settings.logFileRetentionDays))
          {
            fi.Delete();
          }
        }
      }
      catch {}
    }
  }
}