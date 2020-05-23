using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using DG.Tweening;
using UnityEngine.UI;
using GlobalParameters;

public class YDYHDetailPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    public Text fuelWeight; //燃油重量
    public Text aircraftGunNum; //航炮数量
    public Text rocketNum; //火箭弹数量
    public Text bombNum; //航弹数量
    public Text missileNum; //对地导弹数量

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

    public void OnFuelWeightEndEdit(string s)
    {
        string content = fuelWeight.text;
        if (int.TryParse(content, out int data))
        {
            //Debug.Log("Before : A-10 " + A_10.fuelWeight);
            //Debug.Log("Before : AC - 130 " + AC_130.fuelWeight);
            if (EquipmentSelection.ydyh == YDYH.A_10)
            {
                A_10.fuelWeight = data;
                //Debug.Log("After : A-10 " + A_10.fuelWeight);
            }
            if (EquipmentSelection.ydyh == YDYH.AC_130)
            {
                AC_130.fuelWeight = data;
                //Debug.Log("After : AC-130 " + AC_130.fuelWeight);
            }
        }
    }

    public void OnAircraftGunNumEndEdit(string s)
    {
        string content = aircraftGunNum.text;
        if (int.TryParse(content, out int data))
        {
            if (EquipmentSelection.ydyh == YDYH.A_10)
            {
                A_10.aircraftGun.num = data;
            }
            if (EquipmentSelection.ydyh == YDYH.AC_130)
            {
                AC_130.aircraftGun.num = data;
            }
        }
    }

    public void OnRocketNumEndEdit(string s)
    {
        string content = rocketNum.text;
        if (int.TryParse(content, out int data))
        {
            if (EquipmentSelection.ydyh == YDYH.A_10)
            {
                A_10.rocket.num = data;
            }
            if (EquipmentSelection.ydyh == YDYH.AC_130)
            {
                AC_130.rocket.num = data;
            }
        }
    }

    public void OnBombNumEndEdit(string s)
    {
        string content = bombNum.text;
        if (int.TryParse(content, out int data))
        {
            if (EquipmentSelection.ydyh == YDYH.A_10)
            {
                A_10.bomb.num = data;
            }
            if (EquipmentSelection.ydyh == YDYH.AC_130)
            {
                AC_130.bomb.num = data;
            }
        }
    }

    public void OnMissileNumEndEdit(string s)
    {
        string content = missileNum.text;
        if (int.TryParse(content, out int data))
        {
            if (EquipmentSelection.ydyh == YDYH.A_10)
            {
                A_10.missile.num = data;
            }
            if (EquipmentSelection.ydyh == YDYH.AC_130)
            {
                AC_130.missile.num = data;
            }
        }
    }
}
