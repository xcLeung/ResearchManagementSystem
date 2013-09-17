var RUIKit = {
    RPicSlider: {
        init: function (slideId) {
            var n = 0;
            var list = $("#" + slideId + " .picSlider_list a");
            var info = $("#" + slideId + " .picSlider_info").eq(0);
            var count = list.length;

            //填充控制的那个ul
            var liStrings = [];
            for(var i=0;i<list.length;i++){
                liStrings.push("<li>"+(i+1)+"</li>");
            }
            liStrings = "<ul>"+liStrings.join("")+"</ul>";
            $("#"+slideId).append(liStrings);

            list.filter(":not(:first-child)").hide();
            info.html(list.filter(":first-child").find("img").attr("alt"));
            info.bind("click", function (e) {
                e.preventDefault();
                e.stopPropagation();
                window.open(list.filter(":first-child").attr("href"), "_blank");
            });



            $("#" + slideId + " li").bind("click", function (e) {
                e.preventDefault();
                e.stopPropagation();
                var i = $(this).text() - 1;
                n = i;
                if (i >= count) return;
                info.html(list.find("img").attr("alt"));
                info.unbind().click(function () {
                    window.open(list.eq(i).attr('href'), "_blank")
                });
                list.filter(":visible").fadeOut(500).parent().children().eq(i).fadeIn(1000);
                $(this).css({"background": "#bb2222", 'color': '#000'}).siblings().css({"background": "#555", 'color': '#fff'});
            });
            var showAuto = function () {
                n = n >= (count - 1) ? 0 : ++n;
                $("#"+slideId+" li").eq(n).trigger('click');
            };
            var t = setInterval(showAuto, 4000);
            $("#"+slideId).hover(function () {
                clearInterval(t)
            }, function () {
                t = setInterval(showAuto, 4000);
            });
        }
    }
};
