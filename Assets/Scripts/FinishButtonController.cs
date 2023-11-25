using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<PressFinish> buttonsList;
    [SerializeField] private GameObject nextLevelUI;

    

    // Update is called once per frame
    void Update()
    {
        if (buttonsList.TrueForAll(finish => finish.State))
        {
            nextLevelUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
