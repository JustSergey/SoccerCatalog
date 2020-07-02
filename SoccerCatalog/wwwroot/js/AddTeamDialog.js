$(function () {
    $.ajaxSetup({ cache: false });
    $(".addTeam").click(function (e) {
        e.preventDefault();
        $("<div></div>")
            .addClass("dialog")
            .appendTo("body")
            .dialog({
                title: $(this).attr("title"),
                close: function () { $(this).remove() },
                modal: true
            })
            .load(this.href);
    });
});