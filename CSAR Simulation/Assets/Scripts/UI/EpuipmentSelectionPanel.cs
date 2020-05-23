using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UIFramework;
using UnityEngine.UI;
using GlobalParameters;

public class EpuipmentSelectionPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    public Dropdown YDYHDropdown;
    public Dropdown SARDropdown;

    public Button YDYHButton;
    public Button SARButton;

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
        YDYHDropdown.onValueChanged.AddListener(ChangeYDYHTex);
        SARDropdown.onValueChanged.AddListener(ChangeSARTex);

        YDYHButton.onClick.AddListener(PushYDYHPanel);
        SARButton.onClick.AddListener(PushSARPanel);
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(0.8f, .5f);
    }

    public override void OnExit()
    {
        //canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        transform.DOScale(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
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
            EquipmentSelection.ydyh = GlobalParameters.YDYH.A_10;
        }
        else if (n == 1)
        {
            YDYH.GetComponent<RawImage>().texture = AC_130;
            EquipmentSelection.ydyh = GlobalParameters.YDYH.AC_130;
        }
    }

    public void PushYDYHPanel()
    {
        if (EquipmentSelection.ydyh == GlobalParameters.YDYH.A_10)
        {
            OnPushPanel("A_10Detail");
        }
        else if (EquipmentSelection.ydyh == GlobalParameters.YDYH.AC_130)
        {
            OnPushPanel("AC_130Detail");
        }
    }

    public void ChangeSARTex(int n)
    {
        if (n == 0)
        {
            SAR.GetComponent<RawImage>().texture = MH_53;
            EquipmentSelection.sar = GlobalParameters.SAR.MH_53;
        }
        else if (n == 1)
        {
            SAR.GetComponent<RawImage>().texture = MH_60;
            EquipmentSelection.sar = GlobalParameters.SAR.MH_60;
        }
    }

    public void PushSARPanel()
    {
        if (EquipmentSelection.sar == GlobalParameters.SAR.MH_53)
        {
            OnPushPanel("MH_53Detail");
        }
        else if (EquipmentSelection.sar == GlobalParameters.SAR.MH_60)
        {
            OnPushPanel("MH_60Detail");
        }
    }
}
