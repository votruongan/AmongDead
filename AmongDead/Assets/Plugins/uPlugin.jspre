const uPlugin = {

  cacheData: {
    a:123
  },

  hashCode: function(str) {
    console.log("invoke uPlugin.hashCode. str:",str);
    var hash = 0;
    if (str.length == 0) {
        return hash;
    }
    for (var i = 0; i < str.length; i++) {
        var char = str.charCodeAt(i);
        hash = ((hash<<5)-hash)+char;
        hash = hash & hash; // Convert to 32bit integer
    }
    return hash.toString();
  },
  
  returnString: function (data){
    if (!data) return '';
    console.log("invoke uPlugin.returnString data:", data);
    console.log("invoke uPlugin.returnString datatype:", typeof(data));
    var rStr = data.toString();
    console.log("invoke uPlugin.returnString rStr0:", rStr);
    if (data instanceof Object) rStr = JSON.stringify(data);
    console.log("invoke uPlugin.returnString rStr1:", rStr);
    return rStr;
    var bufferSize = unityInstance.Module.lengthBytesUTF8(rStr) + 1;
    var buffer = unityInstance.Module._malloc(bufferSize);
    unityInstance.Module.stringToUTF8(rStr, buffer, bufferSize);
    console.log("invoke uPlugin.returnString buffer:", buffer);
    return buffer;
  },

}