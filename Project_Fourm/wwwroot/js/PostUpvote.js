/*
increment / decrement the post - upvote - count after post - upvote - checkbox state change
It also sends postId to ManageUpvote action in Controller
*/

$(document).ready(function () {
    $('.post-upvote-checkbox').change(function () {

        var postid = $(this).data('postid');
        var icon = document.getElementById('post-upvote-favorite');

        var countNumElement = $(this).closest('.single-post-display').find('.post-upvote-count');

        var isChecked = $(this).prop('checked');

        // Increment or decrement the count based on the checkbox state
        var newCount = parseInt(countNumElement.text()) + (isChecked ? 1 : -1);


        // Update the displayed count
        countNumElement.text(newCount);

        // send postId to ManageUpvote action 
        $.ajax({
            url: '/Forum/ManageUpvote?postId=${postid}',
            type: 'POST',
            data: { postId: postid },
            success: function (response) {
                console.log(response);

            },
            error: function (error) {
                console.error(error);
            }
        });
    });
});
