using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GeoLocation : MonoBehaviour
{
    [SerializeField] private String URL;
    [SerializeField] private GameObject menu;
    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        StartCoroutine(CheckLocation());
    }

    private IEnumerator CheckLocation()
    {
        while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return null;
        }
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }
        LocationInfo locationInfo = Input.location.lastData;

        if (locationInfo.latitude != 0 || locationInfo.longitude != 0)
        {
            if (IsInUkraine(locationInfo.latitude, locationInfo.longitude))
            {
                menu.SetActive(true);
            }
            else
            {
                Application.OpenURL(URL);
            }
        }
    }

    private bool IsInUkraine(float latitude, float longitude)
    {
        float ula = 49.0f; //ukraineCenterLatitude
        float ulo = 32.0f; //ukraineCenterLongitude

        float ur = 5.0f; //ukraineRadius

        if (latitude > (ula - ur) && latitude < (ula + ur)
    && longitude > (ulo - ur) && longitude < (ulo + ur))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

