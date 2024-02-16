/*
    stores state of respond - upvote - checkbox in local storage
    Local storage is used because of the ammount of records that will be stored in PostUpvotes
    and efficency of getting records for certain(user, post) pair from such a big dataset

Note: consider method to clear data storage and get values from db in some scenarios.
    For example store information about all checkboxes user loaded and expire them after a day.
    Then while loading page check if user store information about loaded posts and for those post
    that he doesn't store information for, get value of PostUpvotes and change the state of checkbox based
    on that information'
*/
$(document).ready(function () {
    $('.respond-upvote-checkbox').change(function () {
        var respondid = $(this).data('respondid');
        var isChecked = $(this).prop('checked');

        // Store the checkbox state in local storage
        localStorage.setItem('RC' + respondid, isChecked);

        var icon = document.getElementById('respond-upvote-favorite');
        var countNumElement = $(this).closest('.single-respond-display').find('.respond-upvote-count');
    });

// Load the state of checkboxes from local storage on page load
$('.respond-upvote-checkbox').each(function () {
        var respondid = $(this).data('respondid');
var isChecked = localStorage.getItem('RC' + respondid) === 'true';
$(this).prop('checked', isChecked);
    });
});

