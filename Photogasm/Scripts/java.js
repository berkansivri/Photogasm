//function btnComment(commentRow) {
//    var crow = commentRow.getAttribute("data-cmnt");
//    var x = document.getElementsByClassName("imgPanel");
//    var y = document.getElementsByClassName("closePopUpComment");
//    var z = document.getElementsByClassName("imgPanel-comment");
//    x[crow - 1].style.transition = "2s";
//    x[crow - 1].style.background = "rgba(0,0,0,0.8)";
//    x[crow - 1].style.color = "#FFF";
//    x[crow - 1].style.height = "950px";
//    x[crow - 1].style.zIndex = "999";

//    y[crow - 1].style.transition = "2s";
//    y[crow - 1].style.opacity = "1";

//    z[crow - 1].style.transition = "2s";
//    z[crow - 1].style.opacity = "1";
//}
//function btnClosePopUpComment(closeRow)
//{
//    var crow = closeRow.getAttribute("data-close");
//    var x = document.getElementsByClassName("imgPanel");
//    var y = document.getElementsByClassName("closePopUpComment");
//    var z = document.getElementsByClassName("imgPanel-comment");
//    x[crow - 1].style.transition = "2s";
//    x[crow - 1].style.position = "";
//    x[crow - 1].style.background = "";
//    x[crow - 1].style.height = "";
//    x[crow - 1].style.zIndex = "";
//    x[crow - 1].style.color = "#000";
//    x[crow - 1].style.marginright = "";
//    x[crow - 1].style.transform = "";
//    x[crow - 1].style.margin = "";
//    x[crow - 1].style.top = "";
//    x[crow - 1].style.left = "";

//    y[crow - 1].style.transition = "2s";
//    y[crow - 1].style.opacity = "";

//    z[crow - 1].style.transition = "2s";
//    z[crow - 1].style.opacity = "";
//}

//$(function{
//    $("form").submit(function () {
//        var $lg_username = $('#txtUsername').val();
//        var $lg_password = $('#txtPassword').val();
//        if ($lg_username == "ERROR") {
//            msgChange($('#div-login-msg'), $('#icon-login-msg'), $('#text-login-msg'), "error", "glyphicon-remove", "Login failed.");
//        } else {
//            msgChange($('#div-login-msg'), $('#icon-login-msg'), $('#text-login-msg'), "success", "glyphicon-ok", "Login success.");
//        }
//    })
//});

$(document).ready(function () {
    var $btnSets = $('#responsive'),
        $btnLinks = $btnSets.find('a');

    $btnLinks.click(function (e) {
        e.preventDefault();
        $(this).siblings('a.active').removeClass("active");
        $(this).addClass("active");
        var index = $(this).index();
        $("div.user-menu>div.user-menu-content").removeClass("active");
        $("div.user-menu>div.user-menu-content").eq(index).addClass("active");
    });
});

$(document).ready(function () {
    $("[rel='tooltip']").tooltip();

    $('.view').hover(
        function () {
            $(this).find('.caption').slideDown(250); //.fadeIn(250)
        },
        function () {
            $(this).find('.caption').slideUp(250); //.fadeOut(205)
        }
    );
});

function goTop() {
    $("html,body").animate({ scrollTop: 0 }, 1000);
}

        //-----------FOR MYPAGE MODAL-------------
//$(document).ready(function () {
//    var $lightbox = $('#imgModalBody');
//    $('[data-target="#imgModal"]').on('click', function (event) {
//        var $img = $(this).find('input'),
//            alt = $img.attr('alt'),
//            src = $img.attr('src'),
//            css = {
//                'maxWidth': $(window).width() - 100,
//                'maxHeight': $(window).height() - 100
//            };
//        $lightbox.find('img').attr('src', src);
//        $lightbox.find('img').attr('alt', alt);
//        $lightbox.find('img').css(css);
//    });
//    $lightbox.on('shown.bs.modal', function (e) {
//        var $img = $lightbox.find('img');

//        $lightbox.find('.modal-body').css({ 'width': $img.width() });
//        $lightbox.find('.close').removeClass('hidden');
//    });
//});
//-----------FOR MYPAGE MODAL END-------------

