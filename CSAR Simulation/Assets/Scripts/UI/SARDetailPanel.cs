using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using DG.Tweening;
using UnityEngine.UI;
using GlobalParameters;

public class SARDetailPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    public Text height; //飞行高度
    public Text fuelWeight; //燃油重量
    public Dropdown sarWeapon; //携带武器
    public Text weaponNum; //武器数量

    // Start is called before the first frame update
    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        sarWeapon.onValueChanged.AddListener(OnSarWeaponEndEdit);
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

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }

    public void OnHeightEndEdit(string s)
    {
        string content = height.text;
        if (int.TryParse(content, out int data))
        {
            if (EquipmentSelection.sar == SAR.MH_53)
            {
                MH_53.height = data;
            }
            if (EquipmentSelection.sar == SAR.MH_60)
            {
                MH_60.height = data;
            }
        }
    }

    public void OnFuelWeightEndEdit(string s)
    {
        string content = fuelWeight.text;
        if (int.TryParse(content, out int data))
        {
            if (EquipmentSelection.sar == SAR.MH_53)
            {
                MH_53.fuelWeight = data;
            }
            if (EquipmentSelection.sar == SAR.MH_60)
            {
                MH_60.fuelWeight = data;
            }
        }
    }

    public void OnSarWeaponEndEdit(int n)
    {
        if (EquipmentSelection.sar == SAR.MH_53)
        {
            switch (n)
            {
                case 0:
                    MH_53.sarWeapon = SARWeapon.antiTank;
                    break;
                case 1:
                    MH_53.sarWeapon = SARWeapon.gatlin;
                    break;
                case 2:
                    MH_53.sarWeapon = SARWeapon.gun;
                    break;
            }
        }
        if (EquipmentSelection.sar == SAR.MH_60)
        {
            switch (n)
            {
                case 0:
                    MH_60.sarWeapon = SARWeapon.antiTank;
                    break;
                case 1:
                    MH_60.sarWeapon = SARWeapon.gatlin;
                    break;
                case 2:
                    MH_60.sarWeapon = SARWeapon.gun;
                    break;
            }
        }
    }

    public void OnWeaponNumEndEdit(string s)
    {
        string content = weaponNum.text;
        if (int.TryParse(content, out int data))
        {
            if (EquipmentSelection.sar == SAR.MH_53)
            {
                MH_53.weaponNum = data;
            }
            if (EquipmentSelection.sar == SAR.MH_60)
            {
                MH_60.weaponNum = data;
            }
        }
    }
}
