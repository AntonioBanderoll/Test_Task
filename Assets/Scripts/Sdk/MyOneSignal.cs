using OneSignalSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOneSignal : MonoBehaviour
{
    void Awake()
    {
        OneSignal.Initialize("635c5f12-bb1c-4e8a-92a5-65636c604328");
    }
}
