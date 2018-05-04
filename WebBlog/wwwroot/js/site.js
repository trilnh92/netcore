// Write your JavaScript code.
$(document).ready(function () {
    $(".rightMenu").click(function () {
        $(this).children("ul .ulRightMenu").toggle(500);
    });
    $(".icon-pencil").click(function () {
        $(this).children("tr .OneThis").addClass("abc");
    });
    $(".Add").click(function () {
        $(".newTag").toggle(200);
    })
    $(".Add2").click(function () {
        $(".newTag").toggle(200);
    })
    $(".btn-cancel").click(function () {
        $(".newTag").toggle(200);
    })
});
