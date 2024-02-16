document.addEventListener("DOMContentLoaded", function () {
    // This function will be executed when a radio button is chosen
    var radioButtons = document.querySelectorAll('input[class="radio-button-style"]');

    radioButtons.forEach(function (radioButton) {
        radioButton.addEventListener("change", function () {
            var form = radioButton.closest('form');
            // Trigger the form submission
            form.submit();
        });
    });
});