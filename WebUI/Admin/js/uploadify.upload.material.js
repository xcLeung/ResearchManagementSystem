function UploadInit() {
    $("#upload_div").uploadify(
        {   
        'debug': false,
        'width': '120',   //设置按钮宽度
        'height': '20',   //设置按钮高度
        'swf': '../../uploadify/uploadify.swf',
        'uploader': 'AjaxUpload_Material.aspx',
        //'auto': false,
        'successTimeout': 99999,
        'fileTypeDesc': 'Allow Type：',
        'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png;*.swf',    //上传格式
        'multi': true,   //上传多个文件
        'fileSizeLimit': '50MB',
        //选择上传文件后调用
        'onSelect': function (file) {
        
        },
        //返回一个错误，选择文件的时候触发
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("the number of files must less than " + $('#upload_div').uploadify('settings', 'queueSizeLimit') + "!");
                    break;
                case -110:
                    alert("file [" + file.name + "] not in the range of" + $('#upload_div').uploadify('settings', 'fileSizeLimit') + "!");
                    break;
                case -120:
                    alert("file [" + file.name + "] size exception!");
                    break;
                case -130:
                    alert("file [" + file.name + "] type not allow!");
                    break;
            }
        },
        //检测FLASH失败调用
        'onFallback': function () {
            alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
        },
        //上传到服务器，服务器返回相应信息到data里
        'onUploadSuccess': function (file, data, response) {
            alert("成功上传文件！");
            $("#Material").val(data);
            $("#MaterialImg").prop("src",data);
            $("#_Url").val(data);
            $("#_Size").val(parseInt(file.size / 1024) + "KB");
        }
    });
}