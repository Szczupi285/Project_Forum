/*
increment / decrement the respond - upvote - count after respond - upvote - checkbox state change
It also sends respondId to ManageRespondUpvote action in Controller
*/
$(document).ready(function () {
    $('.respond-upvote-checkbox').change(function () {

        var respondid = $(this).data('respondid');
        var icon = document.getElementById('respond-upvote-favorite');

        var countNumElement = $(this).closest('.single-respond-display').find('.respond-upvote-count');

        var isChecked = $(this).prop('checked');

        // Increment or decrement the count based on the checkbox state
        var newCount = parseInt(countNumElement.text()) + (isChecked ? 1 : -1);


        // Update the displayed count
        countNumElement.text(newCount);

        // send respondId to ManageUpvote action
        $.ajax({
            url: '/Forum/ManageRespondUpvote?respondId=${respondId}',
            type: 'POST',
            data: { respondId: respondid },
            success: function (response) {
                console.log(response);

            },
            error: function (error) {
                console.error(error);
            }
        });
    });
});