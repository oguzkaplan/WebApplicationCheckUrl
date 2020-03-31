/*!
    * Start Bootstrap - SB Admin v6.0.0 (https://startbootstrap.com/templates/sb-admin)
    * Copyright 2013-2020 Start Bootstrap
    * Licensed under MIT (https://github.com/BlackrockDigital/startbootstrap-sb-admin/blob/master/LICENSE)
    */
(function ($) {
    "use strict";

    getDatePicker();

    // Add active state to sidbar nav links
    var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
    $("#layoutSidenav_nav .sb-sidenav a.nav-link").each(function () {
        if (this.href === path) {
            $(this).addClass("active");
        }
    });

    // Toggle the side navigation
    $("#sidebarToggle").on("click", function (e) {
        e.preventDefault();
        $("body").toggleClass("sb-sidenav-toggled");
    });


    $("body").on("click", ".modalPopupView", function () {
        //data-target dan url mizi al
        var url = $(this).data("target");
        var title = $(this).data("title");
        $("#modalLabel").html(title);

        var callbackMethod = $(this).data("callbackMethod");
        //bu urlimizi post et 
        
        $.get(url, function (data) { })
            //eğer işlemimiz başarılı bir şekilde gerçekleşirse 
            .done(function (data) {
                console.log(data);
                //gelen datayı.modal - body mizin içerise html olarak ekle
                $("#FormModalCenter .modal-body").html(data);
                //sonra da modalimizi göster 
                $("#FormModalCenter").modal("show");
            })
            //eğer işlem başarısız olursa 
            .fail(function () {
                //modalımızın bodysine Error! yaz 
                $("#FormModalCenter .modal-body").text("Error!!");
                //sonra da modalimizi göster
                $("#FormModalCenter").modal("show");
            });
    });


})(jQuery);


function getDatePicker() {
    $(".datepicker").datepicker();
}
