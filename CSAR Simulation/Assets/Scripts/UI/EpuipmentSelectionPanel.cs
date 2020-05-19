using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UIFramework;
using UnityEngine.UI;

public class EpuipmentSelectionPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    public Texture A_10;
    public Texture MH_53;
    public Texture EC_130;
    public Texture AC_130;
    public Texture MH_60;

    public GameObject YDYH;
    public GameObject SAR;
    public GameObject YJ;

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
        transform.DOScale(1, .5f);
    }

    public override void OnExit()
    {
        //canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        transform.DOScale(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }

    public void ChangeYDYHTex(int n)
    {
        if (n == 0)
        {
            YDYH.GetComponent<RawImage>().texture = A_10;
            Debug.Log("1111");
        }
        else if (n == 1)
        {
            YDYH.GetComponent<RawImage>().texture = AC_130;
            Debug.Log("2222");
        }
    }

    public void ChangeSARTex(int n)
    {
        if (n == 0)
        {
            SAR.GetComponent<RawImage>().texture = MH_53;
        }
        else if (n == 1)
        {
            SAR.GetComponent<RawImage>().texture = MH_60;
        }
    }
}
