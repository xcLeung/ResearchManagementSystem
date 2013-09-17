var RUIKit = {
    RPagination : {
        init: function (idSelector) {
            if ($(idSelector + ".RPagination").length > 1) {
                console.log("More than one Pagination existed in this page! Please Just leave one!");
            }
            var i = 0;
            var nPageNum = 1;
            var nPageConditionLoation = -1;
            var aSearchConditions = [location.href];
            if (location.href.match(/\?/g) != null) {
                aSearchConditions = location.href.split('?')[1].split('&');
                for (i = 0; i < aSearchConditions.length; i++) {
                    if (aSearchConditions[i].toLowerCase().match(/page=.*/g) != null) {
                        nPageConditionLoation = i;
                        nPageNum = parseInt(aSearchConditions[i].split('=')[1]);
                    }
                }
            }
            var oPagination = $(idSelector + ".RPagination a");
            oPagination.bind("click", function (event) {
                event.stopPropagation();
                event.preventDefault();
                if (nPageConditionLoation != -1) {
                    switch ($(this).text()) {
                        case "<":
                            nPageNum--;
                            break;
                        case ">":
                            nPageNum++;
                            break;
                        default:
                            nPageNum = parseInt($(this).text());
                            break;
                    }
                    aSearchConditions[nPageConditionLoation] = "page=" + nPageNum;
                    location.href = location.href.split("?")[0] + "?" + aSearchConditions.join("&");
                } else {
                    if (location.href.match(/\?/g) != null && (location.href.match(/&/g) == null)) {
                        location.href += "&page=" + $(this).text();
                    }
                    else {
                        location.href += "?page=" + $(this).text();
                    }
                }
            });
            var nPageMaxNum = parseInt(oPagination.eq(oPagination.length - 2).text());
            if (nPageNum == nPageMaxNum) {
                oPagination.eq(oPagination.length - 1).addClass("disabled");
                oPagination.eq(oPagination.length - 1).unbind("click");
                oPagination.eq(oPagination.length - 1).bind("click", function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                });
            } else if (nPageNum == 1) {
                oPagination.eq(0).addClass("disabled");
                oPagination.eq(0).unbind("click");
                oPagination.eq(0).bind("click", function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                });
            }
            for (i = 0; i < oPagination.length; i++) {
                if (oPagination.eq(i).text() == nPageNum) {
                    oPagination.eq(i).addClass("actived");
                }
            }
            //以下是判断裁剪的部分，逻辑混乱，有问题，虽然不影响正常使用，希望日后能重写一下
            //Jation   2013-7-8 17:19:18
            if (nPageMaxNum > 9) {
                if (nPageNum > 4 && nPageNum + 5 < nPageMaxNum) {
                    for (i = nPageNum - 3; i > 1; i--) {
                        oPagination.eq(i).parent().hide();
                    }
                    for (i = nPageNum + 3; i < nPageMaxNum - 1; i++) {
                        oPagination.eq(i).parent().hide();
                    }
                    oPagination.eq(1).after("<span>...</span>");
                    oPagination.eq(nPageNum + 2).after("<span>...</span>");
                } else if (nPageNum <= 4 && nPageNum + 5 < nPageMaxNum) {
                    for (i = nPageNum + 5; i < nPageMaxNum - 1; i++) {
                        oPagination.eq(i).parent().hide();
                    }
                    oPagination.eq(nPageNum + 4).after("<span>...</span>");
                } else if (nPageNum >= nPageMaxNum - 5 && nPageNum + 3 <= nPageMaxNum) {
                    for (i = nPageNum - 3; i > 1; i--) {
                        oPagination.eq(i).parent().hide();
                    }
                    for (i = nPageNum + 3; i < nPageMaxNum - 1; i++) {
                        oPagination.eq(i).parent().hide();
                    }
                    oPagination.eq(1).after("<span>...</span>");
                    oPagination.eq(nPageMaxNum - 1).before("<span>...</span>");
                } else if (nPageNum >= nPageMaxNum - 5 && nPageNum + 3 > nPageMaxNum) {
                    for (i = nPageNum - 3; i > 1; i--) {
                        oPagination.eq(i).parent().hide();
                    }
                    oPagination.eq(1).after("<span>...</span>");
                }
            }
        }

    }
};
