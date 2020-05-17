using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    [Serializable]
    public class UIPanelInfo : ISerializationCallbackReceiver
    {
        [NonSerialized]
        public UIPanelType panelType;

        public string panelTypeString;
        //{
        //    get
        //    {
        //        return panelType.ToString();
        //    }
        //    set
        //    {
        //        UIPanelType type = (UIPanelType)Enum.Parse(typeof(UIPanelInfo), value);
        //        panelType = type;
        //    }
        //}

        public string path;

        //反序列化：从文本信息到对象
        public void OnAfterDeserialize()
        {
            UIPanelType type = (UIPanelType)Enum.Parse(typeof(UIPanelType), panelTypeString);
            panelType = type;
        }

        public void OnBeforeSerialize()
        {

        }
    }
}