

  

$(function () {
    setProductCategoryMenu();
})



//功能:加入購物車
function addCar(fId) {
    $.ajax({
        url: '/Home/AddCar',
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


//功能:展開選單
    html = "";
    function createList(array)
    {
        //說明:讀出每一個tTree的name
        $.each(array, function ()
        {
            
            html += '<div><a href="#" name="sidebar" data-group="' + this.Id + '">' + this.name + '</a></div>';
            
             //說明:如果為最後節點就跳出當前迴圈，進入下一個迴圈(continue)
             if (this.subdir.length == 0)
             {
                return true; 
             }
             //說明:還有子階層就繼續展開選單
             else
             {
                createList(this.subdir);
             }
         })      
        return html;
    }
    //功能:取得分類節點model
    //注意:取得分類畫面後事件綁定失效!!!
    function getCategorynode(nodeId) {
        $.ajax({
            url: '/Home/Getnodemodel',
            type: 'GET',
            data: { nodeId: nodeId },
            async: false,
            success: function (nodeModel) {
                var html_node = createCard(nodeModel);                
                $("#productcard").html(html_node);             
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
                alert(msg + 'Ajax request 取得節點卡片發生錯誤');
            }
        });
    }