using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace metervara.logging
{
  public class LogglyLogger : LogInterceptionBase<LogglySettings>
  { 
    public override void Start()
    {
      base.Start();

      if(String.IsNullOrEmpty(settings.apiToken)) {
        enabled = false;
        return;
      }
      IsReady = true;
    }

    override protected void Log(LogItem msg)
    {
      WWWForm form = new WWWForm();

      form.AddField("LEVEL", msg.type.ToString());
      form.AddField("Message", msg.logString);
      form.AddField("StackTrace", msg.stackTrace);

      UnityWebRequest www = UnityWebRequest.Post($"https://logs-01.loggly.com/inputs/{settings.apiToken}/tag/{settings.TAG}", form);
      www.SendWebRequest(); // yield return www.SendWebRequest();;
    }
  }
}