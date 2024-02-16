$(document).ready(function () {
// This function will be executed when a radio button is chosen
$('input[class="radio-button-style"]').change(function () {

    var form = $(this).closest('form');
    // Trigger the form submission
    form.submit();
});
