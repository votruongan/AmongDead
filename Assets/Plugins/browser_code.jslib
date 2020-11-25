mergeInto(LibraryManager.library, {

  GetCacheData1: function (h, key0){
    console.log("invoke get1");
    h = Pointer_stringify(h);
    key0 = Pointer_stringify(key0);
    console.log(h, key0);
    const obj = uPlugin.cacheData[h];
    if (!obj) return '';
    console.log("object found: ",obj);
    
    var res = obj[key0];
    console.log("object res: ",res);

    if (res == null) {res = '';}
    else{
     if (res instanceof Object) {res = JSON.stringify(res);}
        else {res = res.toString();      }
    }

    var bufferSize = lengthBytesUTF8(res) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(res, buffer, bufferSize);
    return buffer;
  },

  GetCacheData2: function (h, key0, key1){
    console.log("invoke get2");
    h = Pointer_stringify(h);
    key0 = Pointer_stringify(key0);
    key1 = Pointer_stringify(key1);
    console.log(h, key0, key1);
    const obj = uPlugin.cacheData[h];
    if (!obj) return '';
    console.log("object found: ",obj);
    
    var res = obj[key0][key1];
    console.log("object res: ",res);

    if (res == null) {res = '';}
    else{
     if (res instanceof Object) {res = JSON.stringify(res);}
        else {res = res.toString();      }
    }

    var bufferSize = lengthBytesUTF8(res) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(res, buffer, bufferSize);
    return buffer;
  },

  GetCacheData3: function (h, key0, key1, key2){
    console.log("invoke get3");
    h = Pointer_stringify(h);
    key0 = Pointer_stringify(key0);
    key1 = Pointer_stringify(key1);
    key2 = Pointer_stringify(key2);
    console.log(h, key0, key1, key2);
    const obj = uPlugin.cacheData[h];
    if (!obj) return '';
    console.log("object found: ",obj);

    var res = obj[key0][key1][key2];
    console.log("object res: ",obj[key0][key1][key2]);  

    if (res == null) {res = '';}
    else{
     if (res instanceof Object) {res = JSON.stringify(res);}
        else {res = res.toString();      }
    }

    var bufferSize = lengthBytesUTF8(res) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(res, buffer, bufferSize);
    return buffer;
  },

  GetCacheData4: function (h, key0, key1, key2, key3){
    console.log("invoke get4");
    h = Pointer_stringify(h);
    key0 = Pointer_stringify(key0);
    key1 = Pointer_stringify(key1);
    key2 = Pointer_stringify(key2);
    key3 = Pointer_stringify(key3);
    console.log(h, key0, key1, key2, key3);
    const obj = uPlugin.cacheData[h];
    if (!obj) return '';
    console.log("object found: ",obj);

    var res = obj[key0][key1][key2][key3];
    console.log("object res: ",res);

    if (res == null) {res = '';}
    else{
     if (res instanceof Object) {res = JSON.stringify(res);}
        else {res = res.toString();      }
    }

    var bufferSize = lengthBytesUTF8(res) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(res, buffer, bufferSize);
    return buffer;
  },

  ParseObjectValue: function (str){
    str = Pointer_stringify(str);
    const h = uPlugin.hashCode(str);
    uPlugin.cacheData[h] = JSON.parse(str);

    var bufferSize = lengthBytesUTF8(h) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(h, buffer, bufferSize);
    return buffer;
  },
  
  ReleaseCache: function(h){
    h = Pointer_stringify(h);
    uPlugin.cacheData[h] = null;
  }

});
