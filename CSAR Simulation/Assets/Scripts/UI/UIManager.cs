using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    public class UIManager
    {
        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIManager();
                }
                return _instance;
            }
        }

        private Transform canvasTransform;
        private Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                {
                    canvasTransform = GameObject.Find("Canvas").transform;
                }
                return canvasTransform;
            }
        }
        private Dictionary<UIPanelType, string> panelPathDict; //存储所有面板Prefab路径
        private Dictionary<UIPanelType, BasePanel> panelDic; //保存所有实例化面板的游戏物体身上的Panel组件
        private Stack<BasePanel> panelStack;

        class UIPanelTypeJson
        {
            public List<UIPanelInfo> infoList;
        }

        private UIManager()
        {
            ParseUIPanelTypeJson();
        }

        /// <summary>
        /// 把某个页面入栈：显示在界面上
        /// </summary>
        public void PushPanel(UIPanelType panelType)
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }

            //判断栈里是否有页面
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }
            BasePanel panel = GetPanel(panelType);
            panel.OnEnter();
            panelStack.Push(panel);
        }

        public void PushInfo(string content)
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }

            //判断栈里是否有页面
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }
            InfoPanel panel = GetPanel(UIPanelType.Info) as InfoPanel;
            panel.OnEnter();
            panel.SetContent(content);
            panelStack.Push(panel);
        }

        /// <summary>
        /// 出栈：从界面移除
        /// </summary>
        public void PopPanel()
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }

            //判断栈里是否有页面
            if (panelStack.Count <= 0) return;

            BasePanel topPanel = panelStack.Pop();
            topPanel.OnExit();

            if (panelStack.Count <= 0) return;
            BasePanel topPanel2 = panelStack.Peek();
            topPanel2.OnResume();
        }

        public BasePanel GetPanel(UIPanelType panelType)
        {
            if (panelDic == null)
            {
                panelDic = new Dictionary<UIPanelType, BasePanel>();
            }

            //BasePanel panel;
            //panelDic.TryGetValue(panelType, out panel); //TODO

            //BasePanel panel = panelDic.TryGet(panelType);
            panelDic.TryGetValue(panelType, out BasePanel panel);

            if (panel == null)
            {
                //如果找不到，那么就找这个面板的路径，然后根据Prefab去实例化面板
                //string path;
                //panelPathDict.TryGetValue(panelType, out path);
                string path = panelPathDict.TryGet(panelType);
                Debug.Log(path);
                GameObject instPanel = Object.Instantiate(Resources.Load(path)) as GameObject;
                instPanel.transform.SetParent(CanvasTransform, false); //TODO
                panelDic.Add(panelType, instPanel.GetComponent<BasePanel>());
                return instPanel.GetComponent<BasePanel>();
            }
            else
            {
                return panel;
            }
        }

        private void ParseUIPanelTypeJson()
        {
            panelPathDict = new Dictionary<UIPanelType, string>();

            TextAsset ta = Resources.Load<TextAsset>("UI/UIPanelType");

            UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

            foreach (UIPanelInfo info in jsonObject.infoList)
            {
                panelPathDict.Add(info.panelType, info.path);
            }
        }

        /// <summary>
        /// For test
        /// </summary>
        public void Test()
        {
            string path;
            for (int i = 0; i < 5; i++)
            {
                panelPathDict.TryGetValue((UIPanelType)i, out path);
                //Debug.Log(path);
            }

        }
    }
}