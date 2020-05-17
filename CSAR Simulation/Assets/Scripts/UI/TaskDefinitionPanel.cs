using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDefinitionPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
