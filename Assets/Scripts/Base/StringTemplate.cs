public struct StringTemplate{
    public static string LoneWolf_Task = "Kill everyone on this ship without triggering the alarm. (${1}/10)";
    public static string LoneWolf_Alarm = "Kill the witness.";
}


public class StringUtils {
    public static string ExecTemplate(string template, string value){
        string res = template;
        res = res.Replace("${1}",value);
        return res;
    }
    public static string ExecTemplate(string template, string[] data = null){
        string res = template;
        if (data == null) return res;
        for(int i = 0; i < data.Length; i++){
            res = res.Replace("${"+i.ToString()+"}",data[i]);
        }
        return res;
    }
}