/*
    stores state of post - upvote - checkbox in local storage
    Local storage is used because of the ammount of records that will be stored in PostUpvotes
    and efficency of getting records for certain(user, post) pair from such a big dataset

Note: consider method to clear data storage and get values from db in some scenarios.
    For example store information about all checkboxes user loaded and expire them after a day.
    Then while loading page check if user store information about loaded posts and for those post 
*/
  
$(document).ready(function () {
    $('.post-upvote-checkbox').change(function () {
        var postid = $(this).data('postid');
        var isChecked = $(this).prop('checked');

        // Store the checkbox state in local storage
        localStorage.setItem('PC' + postid, isChecked);

        var icon = document.getElementById('post-upvote-favorite');
        var countNumElement = $(this).closest('.single-post-display').find('.post-upvote-count');
    });

// Load the state of checkboxes from local storage on page load
$('.post-upvote-checkbox').each(function () {
        var postid = $(this).data('postid');
var isChecked = localStorage.getItem('PC' + postid) === 'true';
$(this).prop('checked', isChecked);
    });
});
