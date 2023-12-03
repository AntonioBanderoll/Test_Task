using AppsFlyerSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAppsFlyer : MonoBehaviour
{
    void Start()
    {
        AppsFlyer.OnInAppResponse += AppsFlyerOnRequestResponse;

        AppsFlyer.initSDK("devkey", "appID");
        AppsFlyer.startSDK();
        AppsFlyer.setCustomerUserId("someId");
    }
    void AppsFlyerOnRequestResponse(object sender, EventArgs e)
    {
        var args = e as AppsFlyerRequestEventArgs;
        AppsFlyer.AFLog("AppsFlyerOnRequestResponse", " status code " + args.statusCode);
    }
}
