function createItem()
{
    var newItem = $("#ItemDesc").val();
    var actionUrl = $("#CreateActionUrl").val();
    $.post(actionUrl, { itemDesc: newItem }, function (response) {
        $("#ItemListDiv").html(response);
        console.log(response);
    });
}