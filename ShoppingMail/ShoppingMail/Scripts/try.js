var root = '@Url.Content("~/")';

$(function () {
    setSidebar();    
    setProductCards(0);
    $("#load").hide();
    var searchdata = searchkeyword();
    autocomplete(document.getElementById("myInput"), searchdata);   
})
var NowPage = 0;
//說明:卷軸滑到底觸發載入畫面事件
$(window).scroll(function () {
    if ($(window).height() + $(window).scrollTop() == $(document).height()) {
        this.NowPage += 1;
        console.log('這是第' + this.NowPage + '頁');
        setProductCards(this.NowPage);
    }
});










//功能:加入購物車
function addCar(fId) {
    var URL = "Home/AddCar";
    $.ajax({
        url: URL,
        type: 'GET',
        data: { fId: fId},
        async: false,
        success: function (result) {
        },
        error: function (jqXHR) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status === 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status === 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.statusText;
            }
            alert(msg + 'Ajax request 加入購物車功能發生錯誤');
        }
    });
}

//功能:商品卡片區
function setProductCards() {
    var URL = "Home/GetProductcards_2";
    var result_html = '';
    $.ajax({
        //url: '@Url.Action("GetProductcards_2", "Home")',
        url: URL,
        type: "GET",
        //cache: false,
        async: false,
        dataType: "html",
        data: { NowPage: NowPage },
        success: function (result) {
            result_html = result;
            //說明:更新卡片內容
            $("#owl-demo1").html(result);
            //說明:綁定事件
            $('button[name = "addShoppingcar"]').on('click', function (e) {
                var fId = $(this).data('addcar');
                addCar(fId);
                var num = parseInt(document.getElementById("showShoppingcarnum").innerText);
                num += 1;
                document.getElementById("showShoppingcarnum").innerText = num;
            });
        }
    });
    return result_html;
}


//功能:展開選單
html = "";
function createList(array) {
    
    //說明:讀出每一個tTree的name
    $.each(array, function () {
        html += '<div><a href="#" name="sidebar" data-group="' + this.name + '">' + this.name + '</a></div>';
        //說明:如果為最後節點就跳出當前迴圈，進入下一個迴圈(continue)
        if (this.subdir.length == 0) {
            return true;
        }
        //說明:還有子階層就繼續展開選單
        else {
            createList(this.subdir);
        }
    })
    return html;
}

//功能:從後端取得資料長出樹狀結構
function setSidebar() {
  var URL = "Home/GetCategory";     
  $.ajax({
        //url: '@Url.Action("GetCategory", "Home")',
        url: URL,
        type: "GET",
        cache: false,
        async: false,
        success: function (result2) {
            var html = createList(result2, 0);
            $("#sidebar").html(html);

            //功能:點節點長出對應的cardsview
            //說明:點擊事件綁定:從_Sidebar樹狀結構<a>標籤中的data-group屬性值判斷點擊到哪個節點          
            $('a[name = "sidebar"]').on('click', function (e) {
                $("#productcard").empty();                
                var nodeId = $(this).data('group');            
                search(nodeId);                
            });
        }
    });
}

//功能:搜尋(Sidebar節點)
function search(search) {
    var URL = "Home/Search";
    $.ajax({
        url: URL,
        type: 'GET',
        data: { search: search },
        async: false,
        success: function (result) {
            $("#owl-demo1").html(result);
        }
    });
}

//功能:搜尋(關鍵字比對)
function searchkeyword() {
    var URL = "Home/SearchAccountContact";
    var test = []
    var new_arr = [];
    $.ajax({
        url: URL,
        dataType: "Json",
        method: "Get",
        async: false,
        success: function (data) {
            $.each(data, function (i) {
                test[i] = data[i].label;
            });
            $.each(test, function (i) {
                var items = test[i];
                //判斷元素是否存在於new_arr中，如果不存在則插入到new_arr的最後
                if ($.inArray(items, new_arr) == -1) {
                    new_arr.push(items);
                }
            });
        }
    });
    return new_arr;
}

//功能:搜尋比對(自動比對)
function autocomplete(inp, arr) {
    /*the autocomplete function takes two arguments, the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed, decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document, except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}