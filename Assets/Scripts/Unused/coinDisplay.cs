using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinDisplay : MonoBehaviour
{
    public int currentcoins;
    
        public void Update()
        {
        currentcoins = Coins.coinCount;

            GetComponent<TMPro.TextMeshProUGUI>().text = currentcoins.ToString() + "/2 coins";

        }
    
}
