public struct StringTemplate{
    public static string LoneWolf_Description = "Kill everyone on this ship without triggering the alarm.\n\nIf any one detect you near a corpse, they would be alarmed. Kill them before they reach emergency button.";
    public static string LoneWolf_Task = "Kill everyone on this ship without triggering the alarm. (${1}/10)";
    public static string LoneWolf_Alarm = "YOU ARE DETECTED. KILL THE WITNESS.";

    public static string CTS_Description = "Capture booth the Navigation Room and Reactor Room.\n\nBe careful as enemy could kill you, and you cannot be spawned.";
    public static string CTS_Task0 = "Capture Navigation Room.";
    public static string CTS_Task1 = "Capture Reactor Room.";
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