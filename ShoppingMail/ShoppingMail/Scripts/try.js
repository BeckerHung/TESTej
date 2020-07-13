//$(document).ready(function () {
//    $('#removeitem').on('show.bs.modal', function (event) {
//        var button = $(event.relatedTarget)
//        var title = button.data('title')
//        var modal = $(this)
//        modal.find('.modal-title').text('確認' + title)
//    });
//});

//var root = '@Url.Content("~/")';




//功能:按讚計數
function like(pid) {
    $.ajax({
        url: '/Home/like',
        type: 'GET',
        data: { pid: pid },
        success: function (likenum) {
            var id = pid-1
            var temp = "按讚數:" + likenum;
            $('p[name="showlike"]')[id].innerHTML = temp;  
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
            alert(msg + 'Ajax request 按讚功能發生錯誤');
        }
    });
}
  

$(function () {
    //功能:隱藏大手圖片
    $('.pop').hide();
    //功能:切換樣式功能，選擇3或4個一列
    $("input[type='radio'][name='Option_1']").change(function () {

        switch ($(this).attr('id')) {
            case 'Option_1':
                $('.row .col-lg-3').addClass('col-lg-4').removeClass('col-lg-3')
                break;
            case 'Option_2':
                $('.row .col-lg-4').addClass('col-lg-3').removeClass('col-lg-4')
                break;
        }
    });
    $('#removeitem').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)
        var title = button.data('title')
        var modal = $(this)
        modal.find('.modal-title').text('確認' + title)
    });
   

})
//功能:點圖兩下出現大手>縮小>消失
var showpicture = function (e) {
    $('.pop').animate({ height: '200px', width: '200px' }, 500);
    $('.pop').fadeIn(2000);
    $('.pop').animate({ height: '50px', width: '50px' }, 500);
    $('.pop').hide(50);
}
//功能:加入購物車
function addCar(fPId) {
    $.ajax({
        url: '/Home/AddCar',
        type: 'GET',
        data: { fPId: fPId },
        async: false,
        success: function (result) {
           // console.log(result);
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









////功能:修改樣式功能:(3或4個一列)
//function setClass(e) {
//    switch (e) {
//        case 4:
//            $('.row .col-lg-3').addClass('col-lg-4').removeClass('col-lg-3')
//            break;
//        case 3:
//            $('.row .col-lg-4').addClass('col-lg-3').removeClass('col-lg-4')
//            break;
//    }
//}

