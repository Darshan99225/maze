using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    [SerializeField] private InputField width;
    [SerializeField] private InputField height;
    [SerializeField] private Text worring;
    
    void Start()
    {
        width.text = DataManager.Instace.width.ToString();
        height.text = DataManager.Instace.height.ToString();
    }

   public void GenerateMaze()
    {
        if((int.Parse(width.text)<10||int.Parse(width.text)>255)|| (int.Parse(height.text) < 10 || int.Parse(height.text) > 255))
        {
            worring.text = "Enter between 10 to 255";
        }
        else
        {
            DataManager.Instace.width = int.Parse(width.text);
            DataManager.Instace.height = int.Parse(height.text);
            SceneManager.LoadScene(1);
        }
    }
}
