$(function () {
    // Declare a proxy to reference the hub. 
    var chat = $.connection.tableAppendHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (name, message) {
        // Html encode display name and message. 
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        $('#broadcastsection').append('<p>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };
     
    // Declare a proxy to reference the hub. 
    var sitewidechat = $.connection.siteWideAppendHub;
    // Create a function that the hub can call to broadcast messages.
    sitewidechat.client.broadcastMessage = function (name, message) {
        // Html encode display name and message. 
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        $('#sitewidebroadcastsection').append('<p>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };
     
    var specificObjectHub = $.connection.specificObjectHub;
    // Create a function that the hub can call to broadcast messages.
    specificObjectHub.client.broadcastMessage = function (id, name, message) {
        // Html encode display name and message. 
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        $('#specific' + id).append('<p>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };
    
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // Call the Send method on the hub. 
            chat.server.send("halo", "message");
        });

        $('#sitewidemessage').click(function () {
            // Call the Send method on the hub. 
            sitewidechat.server.send("sitewide", "sitewide message");
        });

        $('#specificmessage').click(function () {
            var id = $("#specificText").val();
            // Call the Send method on the hub. 
            specificObjectHub.server.send(id, "specific", "specific message");
        });

        if (objectId !== 0) {
            specificObjectHub.server.addGroup(objectId);
        }
    });
});