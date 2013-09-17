var RUIKit = {
    RTabs : {
        init:function(tabId,height){
            $("#"+tabId+" .tabs .tab:first").addClass("tabActived");
            var tabs =$("#"+tabId+" .tabs .tab");
            var blocks = $("#"+tabId+" .blocks .block");
            blocks.hide();
            blocks.eq(0).show();
            blocks.css("height",height-33);
            $("#"+tabId).css("height",height+20);//+20是下面block的padding-top和padding-bottom各10px 加上这句话以保证tab高度
            tabs.bind("click",function(e){
                e.preventDefault();
                e.stopPropagation();
                for(var i=0;i<tabs.length;i++){
                    tabs.eq(i).removeClass("tabActived");
                    blocks.hide();
                }
                $(this).addClass("tabActived");
                blocks.eq($(this).index()).fadeIn();
            })
        }
    }
};
