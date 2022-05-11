&(function (){
    var connection = new signalR.HubConnectionBuilder().withUrl("/myHub").build();

    connection.start();

    $('textarea').keyup(()=>{
        console.log('send: ' + $('textarea').val());
        connection.invoke("Changed", $('textarea').val());
    });
    connection.on("ChangeReceive", function (v){
        console.log('recived ' + v);
        $('textarea').val(v);
    });
});