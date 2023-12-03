using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("RoundCoin", PlayerPrefs.GetInt("RoundCoin",0)+1);
            UIManager.Instance.UpdateRoundCoinInfo();
            Destroy(gameObject);
        }
    }
}
