using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalInfoHolder : MonoBehaviour
{
    public static ImmortalInfoHolder instance;
    public static Dictionary<string,bool> boolData;
    public static Dictionary<string,int> intData;
    public static Dictionary<string,string> stringData;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // DontDestroyOnLoad(this.gameObject);
    }
    
    public static void AddString(string key, string value){
        if (stringData == null) stringData = new Dictionary<string,string>();
        if (stringData.ContainsKey(key)){
            stringData[key] = value;
            return;
        }
        stringData.Add(key,value);
    }
    public static void AddBool(string key, bool value){
        if (boolData == null) boolData = new Dictionary<string,bool>();
        if (boolData.ContainsKey(key)){
            boolData[key] = value;
            return;
        }
        boolData.Add(key,value);
    }
    public static void AddInt(string key, int value){
        if (intData == null) intData = new Dictionary<string,int>();
        if (intData.ContainsKey(key)){
            intData[key] = value;
            return;
        }
        intData.Add(key,value);
    }


    public static string GetString(string key, string defaultValue = ""){
        if (stringData == null || !stringData.ContainsKey(key)) return defaultValue;
        return stringData[key];
    }
    public static bool GetBool(string key, bool defaultValue = false){
        if (boolData == null || !boolData.ContainsKey(key)) return defaultValue;
        return boolData[key];
    }
    public static int GetInt(string key, int defaultValue = 0){
        if (intData == null || !intData.ContainsKey(key)) return defaultValue;
        return intData[key];
    }
    
    public static bool DeleteKey(string key){
        if (stringData != null && stringData.ContainsKey(key)){
            stringData.Remove(key);
            return true;
        }
        if (boolData != null && boolData.ContainsKey(key)){
            boolData.Remove(key);
            return true;
        }
        if (intData != null && intData.ContainsKey(key)){
            intData.Remove(key);
            return true;
        }
        return false;

    }
}
