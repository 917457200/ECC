
//加载分页
; (function () {
    //初始化分页函数
    var pages = function (el, obj) {
        this.$element = el;
        //this.totalpage.totalpage


        //当前对象
        this.pagesObj = $(el);

        //分页样式
        if (obj.cssType != null && obj.cssType != "undefined" && obj.cssType.length > 0) {
            this.cssType = obj.cssType;
            this.viewpage = setViewpage(obj.cssType, this.viewpage);//通过样式设置页面：页码显示多少个
        }

        //绑定数据的datatable对象
        if (obj.table != null && obj.table != "undefined" && obj.table.length > 0) {
            this.table = obj.table;

            //滚动距离顶部的位置
            if (getInt(obj.scroll) > 0) {
                this.scroll = obj.scroll;
            }
        }

        //总页数
        if (getInt(obj.totalpage) > 0) {
            this.totalpage = obj.totalpage;
        }

        //页码显示多少个
        if (getInt(obj.viewpage) > 0) {
            this.viewpage = obj.viewpage;
        }

        //当前第几页
        if (getInt(obj.currentPage) > 0) {
            this.currentPage = obj.currentPage;
        }

        if (getInt(obj.recordCount) > 0) {
            this.recordCount = obj.recordCount;
        }

        //回调函数
        if (typeof obj.callback == 'function') {
            this.callback = eval(obj.callback);
        }

        //保护包默认参数
        this.options = $.extend({}, this.defaults);
    }

    //当前对象
    pages.prototype.pagesObj = null;

    //滚动距离顶部的位置
    pages.prototype.scroll = 0;

    //绑定数据的datatable对象
    pages.prototype.table = "";

    //分页样式
    pages.prototype.cssType = "default";

    //总页数
    pages.prototype.totalpage = 0;

    //页码显示多少个
    pages.prototype.viewpage = 10;

    //当前第几页
    pages.prototype.currentPage = 1;
    //总记录条数
    pages.prototype.recordCount = 0;

    //翻页 回调函数
    pages.prototype.callback = function (call) {
        call;
    }


    //执行分页函数
    pages.prototype.startPage = function () {

        var obj = this.pagesObj;//HTML分页控件对象
        var cssType = this.cssType;
        var callback = this.callback;//回调事件
        $(obj).html('');//清空分页数据，从新分页
        if (this.currentPage < 100000) {
            this.viewpage = 8;
        }
        else if (this.currentPage < 10000000000) {
            this.viewpage = 6;
        }

        pageCalc(this.totalpage, this.viewpage, this.currentPage, function (s, e, c, ee) {
            //如果只有1页，就不显示分页了
            if (e > 0) {
                $(obj).html(getListType(cssType, s, e, c, ee));
            }
            var width = 0;
            for (var i = s; i <= e; i++) {
                if (i.toString().length == 1) {
                    width += 30;
                }
                else if (i.toString().length == 2) {
                    width += 33;
                }
                else if (i.toString().length == 3) {
                    width += 42;
                }
                else if (i.toString().length == 4) {
                    width += 51;
                }
                else if (i.toString().length == 5) {
                    width += 60;
                }
                else if (i.toString().length == 6) {
                    width += 69;
                }
                else if (i.toString().length == 7) {
                    width += 78;
                }
                else if (i.toString().length == 8) {
                    width += 86;
                }
            }
            $(obj).css("width", 260 + width + "px");
            ////alert(s+"_"+e+"_"+c)
            ////设置页面居中
            //if (e < 6) {
            //    $(obj).css("width", 100 + 200 + e * 36 + "px");//36
            //}
            //else {
            //    $(obj).css("width", "600px");//36
            //    //$(obj).css("width", 200 + (e-9) * 48 + (8-(e-9))*40+"px");//36
            //}
            //alert($(obj).css("width"));
            $(obj).css("margin", "10px auto 48px auto");

        });
        if (this.totalpage > 0) {
            $(obj).append('<li>共 <span style="color:#03A3D2;">' + this.recordCount + '</span> 条</li>')
        }

        var $table = this.table;//绑定数据的datatable对象 
        var $scroll = this.scroll;//滚动距离顶部的位置

        //绑定点击事件
        $(obj).find('[data-pagecheck="yes"]').click(function () {

            ////滚动轴定位
            //if ($table.length > 0) {
            //    var y = $(document).scrollTop();
            //    var inputTop = $($table).offset().top;
            //    if ($scroll > 0 && y > inputTop) {
            //        $('html,body').animate({ scrollTop: 0 }, 600);
            //    } else if ($table.length > 0 && y > inputTop) {
            //        $('html,body').animate({ scrollTop: inputTop }, 600);
            //    }
            //}

            var index = $(this).attr("data-pag");//点击第几页
            if (parseInt(index) > 0) {
                callback(index);//回调函数
            }
        });
    }

    $.fn.pageList = function (o) {
        var obj = new pages(this, o);
        obj.startPage();
    }

    /*分页算法 
     totalpage：总页数,
     viewpage：页码显示多少个,
     currentPage：当前第几页 
     callback;(startIndex：开始页码, endIndex：结束页码, curIndex：当前第几页)回掉函数
   */
    pageCalc = function (totalpage, viewpage, currentPage, callback) {
        var startindex, endindex;
        if (viewpage >= totalpage) {
            startindex = 1;
            endindex = totalpage;
        } else {
            if (currentPage <= viewpage / 2) {
                startindex = 1;
                endindex = viewpage;
            } else if ((currentPage + viewpage / 2) > totalpage) {
                startindex = totalpage - viewpage + 1;
                endindex = totalpage;
            } else {
                startindex = currentPage - parseInt((viewpage - 1) / 2);
                endindex = currentPage + parseInt(viewpage / 2);
            }
        }
        var page = { startIndex: startindex, endIndex: endindex, curIndex: currentPage, totalPage: totalpage };
        callback(page.startIndex, page.endIndex, page.curIndex, page.totalPage);
    }

    //字符串处理
    function getInt(str) {
        if (str == null || str == undefined || isNaN(parseInt(str))) {
            return 0;
        }
        return str;
    }

    ///通过样式设置，页码显示多少个
    function setViewpage(cssType, viewpage) {
        var viewpages = viewpage;
        switch (cssType) {
            case "back-end":
                viewpages = 10;
                break;
        }
        return viewpages;
    }

    //分页样式
    function getListType(cssType, s, e, c, ee) {
        var str = "";
        switch (cssType.toLowerCase()) {
            case "back-end":
                var previousPage = c > 1 ? 'data-pag="' + (c - 1) + '"' : 'data-pag="1"';
                str += '<span data-pagecheck="yes" class="reduce" ' + previousPage + '>上一页</span>';
                for (var i = s; i <= e; i++) {
                    if (i == c) {
                        str += '<span data-pag="0" class="cpb" data-currentPage="currentPage">' + i + '</span>';
                    } else {
                        str += '<span data-pagecheck="yes" data-pag="' + i + '">' + i + '</span>';
                    }
                }
                var nextPage = c + 1 > e ? '0' : 'data-pag="' + (c + 1) + '"';
                str += '<span data-pagecheck="yes" ' + nextPage + ' class="add">下一页</span>';

                break;
            case "backweb":
                var previousPage = c > 1 ? 'data-pag="' + (c - 1) + '"' : 'data-pag="1"';
                str += '<li><a data-pagecheck="yes" ' + previousPage + '>上一页</a></li>';
                for (var i = s; i <= e; i++) {
                    if (i == c) {
                        str += '<li><a data-pag="0" class="cpb" data-currentPage="currentPage" class="clickPage">' + i + '</a></li>';
                    } else {
                        str += '<li><a data-pagecheck="yes" data-pag="' + i + '">' + i + '</a></li>';
                    }
                }
                var nextPage = c + 1 > e ? '0' : 'data-pag="' + (c + 1) + '"';
                str += '<li><a data-pagecheck="yes" ' + nextPage + '>下一页</a></li>';
                break;
            case "pagecss":
                var previousPage = c > 1 ? 'data-pag="' + (c - 1) + '"' : 'data-pag="1"';
                str += '<li><a data-pagecheck="yes" data-pag="1">首页</a></li><li><a data-pagecheck="yes" ' + previousPage + '>上一页</a></li>';
                for (var i = s; i <= e; i++) {
                    if (i == c) {
                        str += '<li><a data-pag="0" data-currentPage="currentPage" class="clickPage">' + i + '</a></li>';
                    } else {
                        str += '<li><a data-pagecheck="yes" data-pag="' + i + '">' + i + '</a></li>';
                    }
                }
                var nextPage = c + 1 > e ? '0' : 'data-pag="' + (c + 1) + '"';
                str += '<li><a data-pagecheck="yes" ' + nextPage + '>下一页</a></li><li><a data-pagecheck="yes" data-pag="' + ee + '">尾页</a></li>';
                break;
        }
        return str;
    }

})();

//设置tr样式
$(function () {
    //绑定全选按钮事件
    $("#id_checkAll").click(tableCheckAll);
    $('#id_checkAll').css("cursor", "pointer");
    $("#id_table_list").delegate('[type="checkbox"]', "click", function () {
        var count = $("#id_table_list").find('[type="checkbox"]').length;
        var checkCount = $('#id_table_list [type="checkbox"]:checked').length;
        $("#id_checkAll").prop("checked", count == checkCount);
    });

    //悬浮到tr的样式
    $("#id_table_list").delegate('td', "mouseover", function () {
        $('#id_table_list [type="checkbox"]').css("cursor", "pointer");
        $(this).prop("data-colorBackStr", $(this).parent().find("td").css("background-color"));
        $(this).prop("data-colorStr", $(this).parent().find("td").css("color"));
        $(this).parent().find("td").css("background-color", "#8cb0f7");//悬浮
        //$(this).parent().find("td").css("color", "#ffffff");
    });
    //移开tr的样式
    $("#id_table_list").delegate('td', "mouseout", function () {
        $(this).parent().find("td").css("background-color", $(this).prop("data-colorBackStr"));
        $(this).parent().find("td").css("color", $(this).prop("data-colorStr"));
    });

    //向上排序
    $("#id_table_list").delegate('[data-setsort="pre"]', "click", function () {
        var id = $(this).parents("tr").find("td:eq(0)").find('[type="checkbox"]').prop("id");
        var previd = $(this).parents("tr").prev().find("td:eq(0)").find('[type="checkbox"]').prop("id");
        prev = function (t) {
            var thistr = $(t).parents("tr:eq(0)");
            var prevtr = thistr.prev();
            if (prevtr.length > 0) {
                prevtr.insertAfter(thistr);
            }
            //当前tr 排序箭头隐藏显示
            if (thistr.prev().length > 0) {
                thistr.find('[data-setsort="pre"]').show();
            } else {
                thistr.find('[data-setsort="pre"]').hide();
            }
            if (thistr.next().length > 0) {
                thistr.find('[data-setsort="next"]').show();
            } else {
                thistr.find('[data-setsort="next"]').hide();
            }
            //当前tr的上一个tr 箭头隐藏显示
            if (prevtr.prev().length > 0) {
                prevtr.find('[data-setsort="pre"]').show();
            } else {
                prevtr.find('[data-setsort="pre"]').hide();
            }
            if (prevtr.next().length > 0) {
                prevtr.find('[data-setsort="next"]').show();
            } else {
                prevtr.find('[data-setsort="next"]').hide();
            }
        }
        try {
            //Ajax 调用排序
            sort(id, previd, prev(this));
        } catch (e) {
            $.msg("请重写：sort(id1,id2,collback) 排序方法");
        }
    });
    //向下排序
    $("#id_table_list").delegate('[data-setsort="next"]', "click", function () {
        var id = $(this).parents("tr").find("td:eq(0)").find('[type="checkbox"]').prop("id");
        var nextid = $(this).parents("tr").next().find("td:eq(0)").find('[type="checkbox"]').prop("id");
        if ($.toInt(nextid) <= 0)
            return false;

        next = function (t) {
            var thistr = $(t).parents("tr:eq(0)");
            var nexttr = thistr.next();
            if (nexttr.length > 0) {
                nexttr.insertBefore(thistr);
            }
            //当前 排序箭头隐藏显示
            if (thistr.prev().length > 0) {
                thistr.find('[data-setsort="pre"]').show();
            } else {
                thistr.find('[data-setsort="pre"]').hide();
            }
            if (thistr.next().length > 0) {
                thistr.find('[data-setsort="next"]').show();
            } else {
                thistr.find('[data-setsort="next"]').hide();
            }
            //当前tr的下一个tr排序箭头 隐藏显示
            if (nexttr.prev().length > 0) {
                nexttr.find('[data-setsort="pre"]').show();
            } else {
                nexttr.find('[data-setsort="pre"]').hide();
            }
            if (nexttr.next().length > 0) {
                nexttr.find('[data-setsort="next"]').show();
            } else {
                nexttr.find('[data-setsort="next"]').hide();
            }
        }
        try {
            //Ajax 调用排序
            sort(id, nextid, next(this));
        } catch (e) {
            $.msg("请重写：sort(id1,id2,collback) 排序方法");
        }
    });

});

//全选按钮
function tableCheckAll() {
    if ($("#id_checkAll").is(":checked")) {
        $("#id_table_list").find("input").prop("checked", true);
    } else {
        $("#id_table_list").find("input").prop("checked", false);
    }
}

//排序箭头
function sortImg(index, count) {
    var str = "";
    if (index == 0) {
        str += '<img style="cursor:pointer;display:none;" src="/img/sort_pre.png" title="&#8593;" data-setsort="pre"/>';
        str += '<img style="cursor:pointer;"src="/img/sort_next.png" title="&#8595;"data-setsort="next"/>';
    } else if ((count - 1) == index) {
        str += '<img style="cursor:pointer;"src="/img/sort_pre.png" title="&#8593;" data-setsort="pre"/>';
        str += '<img style="cursor:pointer;display:none;" src="/img/sort_next.png" title="&#8595;"data-setsort="next"/>';
    } else {
        str += '<img style="cursor:pointer;"src="/img/sort_pre.png" title="&#8593;" data-setsort="pre"/>';
        str += '<img style="cursor:pointer;"src="/img/sort_next.png" title="&#8595;"data-setsort="next"/>';
    }
    return str;
}

////排序Ajax
//function sort(idone, idtow, callback) {
//    var par = getModelPar(0, { id1: idone, id2: idtow }, "updatesort");
//    //提交数据
//    GpAjax(par, function () {
//    }, function (rs) {
//        if ($.toInt(rs.status) != 0) {
//            $.msgError(rs.mes);
//            return false;
//        }
//        callback;
//    }, "/ajax/PublicCourse/OpenClassHourInfoHandler.ashx", "JSON", false);
//}