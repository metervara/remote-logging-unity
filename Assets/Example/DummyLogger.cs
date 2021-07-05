using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLogger : MonoBehaviour
{
    void Start() {
      Debug.Log("Hello world");
      Debug.LogWarning("Hello Warning");
      Debug.LogError("Hello Error");
      Debug.LogAssertion("Hello Assertion");
      Debug.LogException(new Exception("Hello Exception"));
    }
}
