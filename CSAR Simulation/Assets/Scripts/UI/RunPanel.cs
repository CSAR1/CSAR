using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UIFramework;

public class RunPanel : BasePanel
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

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0f);
    }

    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        transform.DOScale(0, 0f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
