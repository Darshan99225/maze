using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instace;

    private void Awake()
    {
        Instace = this;
    }

    public int width
    {
        get { return PlayerPrefs.GetInt("width", 10); }
        set { PlayerPrefs.SetInt("width", value); }
    }
    public int height
    {
        get { return PlayerPrefs.GetInt("height", 10); }
        set { PlayerPrefs.SetInt("height", value); }
    }

}
