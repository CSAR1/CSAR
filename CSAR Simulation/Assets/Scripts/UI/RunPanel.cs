using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UIFramework;
using UnityEngine.UI;

public class RunPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    public Text timeText;
    public Text lifeText;

    public GameObject content;
    private Pilot pilot;
    List<GameObject> panelList = new List<GameObject>();

    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        /*
        GameObject panel = GameObject.Instantiate(Resources.Load<GameObject>("UI/GridPanel"));
        panel.transform.SetParent(this.content.transform);
        panel.GetComponentInChildren<Text>().text = "暂无状态更新。";
        panelList.Add(panel);*/
        pilot = GameObject.Find("Pilot").GetComponent<Pilot>();
    }

    private void FixedUpdate()
    {
        timeText.text = "距搜救出动已过去：" + pilot.timePassed.ToString("0.00") + "小时";
        lifeText.text = "待救目标剩余生命：" + (pilot.lifeLeft / pilot.life * 100f).ToString("0.00") + "%";
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

    public void ShowInformation(string content)
    {
        foreach (GameObject thePanel in panelList)
        {
            Destroy(thePanel);
        }
        panelList.Clear();
        GameObject panel = GameObject.Instantiate(Resources.Load<GameObject>("UI/GridPanel"));
        panel.transform.SetParent(this.content.transform);
        panel.GetComponentInChildren<Text>().text = content;
        panel.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        panelList.Add(panel);
    }

    public void AddInformation(string content)
    {
        GameObject panel = GameObject.Instantiate(Resources.Load<GameObject>("UI/GridPanel"));
        panel.transform.SetParent(this.content.transform);
        panel.GetComponentInChildren<Text>().text = content;
        panel.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        panelList.Add(panel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
