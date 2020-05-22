using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;

public class Root : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.PushPanel(UIPanelType.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }
}