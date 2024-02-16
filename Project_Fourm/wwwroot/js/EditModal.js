/*
    Open and closes modal, pass the value contentid and contenttype to textarea attributes inside of modal.
        Upon clicking retrives values and text from textarea and sends it to Edit method in Forum controller
*/

// Wait for the document to be ready
document.addEventListener("DOMContentLoaded", function () {
    // Get all elements with class "edit-button"
    var editButtons = document.querySelectorAll(".edit-button");
// Get the modal element by ID
var modal = document.getElementById("edit-modal");
const editTextarea = modal.querySelector('textarea');

// Loop through each edit button
editButtons.forEach(function (button) {
    // Add a click event listener to each edit button
    button.addEventListener("click", function () {
        const Id = button.getAttribute('data-contentid');
        const contentType = button.getAttribute('data-contenttype')

        const nearestContent = contentType === 'Post' ?
            button.closest('.single-post-display').querySelector('.post-content') :
            button.closest('.single-respond-display').querySelector('.respond-content');
        editTextarea.value = nearestContent ? nearestContent.textContent : '';

        editTextarea.setAttribute('data-contentid', Id);
        editTextarea.setAttribute('data-contenttype', contentType);

        // Open the modal
        modal.showModal();
    });
    });

// Close the modal when the button inside the modal is clicked
const sendReportDetailsButton = document.getElementById('edit-button');
    sendReportDetailsButton.addEventListener('click', () => {
        const contentId = editTextarea.getAttribute('data-contentid');
const contentType = editTextarea.getAttribute('data-contenttype');
const newContent = editTextarea.value;

$.ajax({
    url: '/Forum/Edit?contentId=${contentId}&newContent=${newContent}&contentType=${contentType}',
type: 'PUT',
data: {contentId: contentId, newContent: newContent, contentType: contentType },
success: function (response) {
    console.log(response);
// reload page after edit
location.reload(true);
            },
error: function (error) {
    console.error(error);
            }
        });

modal.close();
    });
});
