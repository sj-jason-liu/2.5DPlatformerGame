using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UIManager is MISSING!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private Text _coinText, _liveText;

    //update coin display
    public void UpdateCoinDisplay(int coinUpdate)
    {
        _coinText.text = "Coin: " + coinUpdate;
    }

    public void UpdateLifeDisplay(int lives)
    {
        _liveText.text = "Life: " + lives;
    }
}
