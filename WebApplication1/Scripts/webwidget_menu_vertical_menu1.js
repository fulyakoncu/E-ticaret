(function (a) {
    a.fn.webwidget_menu_vertical_menu1 = function (p) {
        var p = p || {};

        var g = p && p.style_color ? p.style_color : "red";
        var h = p && p.font_color ? p.font_color : "#666";
        var i = p && p.font_decoration ? p.font_decoration : "none";
        var m = p && p.directory ? p.directory : "Styles/images";
        var n = p && p.animation_speed ? p.animation_speed : "fast";
        var c = p && p.cikisyonu ? p.cikisyonu : "left";
        var o = a(this);
        if (o.children("ul").length == 0 || o.children("ul").children("li").length == 0) {
            o.append("Require menu content");
            return null
        }
        init();
        init2();
        function init() {

            o.children("ul").children("li").children("a").css("color", h).css("text-decoration", i);
            o.children("ul").children("li:has(a)").hover(
                function () {
                    mouseover($(this));
                },
                function () {
                    mouseout($(this));
                }
            );

            o.children("ul").children("li").children("ul").css(c, "172px");
            o.children("ul").children("li").children("ul").children("li").children("a").css("color", h).css("text-decoration", i);
            o.children("ul").children("li").children("ul").children("li:has(a)").hover(
                function () {

                },
                function () {

                }
            );
        }
        function init2() {
            var t = o.children("ul").children("li");
            t.children("ul").children("li").children("a").css("color", h).css("text-decoration", i);
            t.children("ul").children("li:has(a)").hover(
                function () {
                    //alert(o.children("ul").children("li").index($(this)));
                    mouseover($(this));
                },
                function () {
                    mouseout($(this));
                }
            );

            t.children("ul").children("li").children("ul").css(c, "172px");
            t.children("ul").children("li").children("ul").children("li").children("a").css("color", h).css("text-decoration", i);
            t.children("ul").children("li").children("ul").children("li:has(a)").hover(
                function () {

                },
                function () {

                }
            );
        }
        function mouseover(dom) {
            dom.children("a").css("color", "#000");
            dom.children("ul").fadeIn(n);
        }
        function mouseout(dom) {
            dom.children("a").css("color", h);
            dom.children("ul").fadeOut(n);
        }
    }
})(jQuery);