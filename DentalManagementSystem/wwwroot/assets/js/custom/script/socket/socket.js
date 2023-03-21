WebSocket ws = new WebSocket("ws://" + document.location.hostname + "/ws");

ws.onopen(){
    ws.send("Setup:" + document.getElementById('UserId').value);
}

ws.onmessage((e) => {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toastr-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "1000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr.info(e.Data, "Thông báo");
})