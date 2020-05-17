using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对Dictionary扩展
/// </summary>
public static class DictionaryExtension
{
    /// <summary>
    /// 尝试根据Key得到value，得到了的话返回value，否则返回空
    /// this Dictionary<Tkey, Tvalue> dict这个字典表示我们要获取值的字典
    /// </summary>
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        dict.TryGetValue(key, out Tvalue value);
        return value;
    }
}