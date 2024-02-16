/*
    Open and closes modal, pass the value contentid and contenttype to textarea attributes inside of modal. 
       Upon clicking retrives vvalues and text from textarea and sends it to Report method in Forum controller
*/

const openModalButton = document.querySelectorAll('.post-report-button');
const openModalButton1 = document.querySelectorAll('.respond-report-button');
const modal = document.getElementById('modal');
const reportDetailsTextarea = modal.querySelector('textarea');

       

openModalButton.forEach(button => {
    button.addEventListener('click', () => {

        const postId = button.getAttribute('data-contentid');

        reportDetailsTextarea.setAttribute('data-contentid', postId);
        reportDetailsTextarea.setAttribute('data-contenttype', "Post");

        modal.showModal();
    });
});

openModalButton1.forEach(button => {
    button.addEventListener('click', () => {

        const respondId = button.getAttribute('data-contentid');

        reportDetailsTextarea.setAttribute('data-contentid', respondId);
        reportDetailsTextarea.setAttribute('data-contenttype', "Respond");

        modal.showModal();
    });
});


// Close the modal when the button inside the modal is clicked
const sendReportDetailsButton = document.getElementById('send-report-details');
sendReportDetailsButton.addEventListener('click', () => {
    const contentId = reportDetailsTextarea.getAttribute('data-contentid');
const contentType = reportDetailsTextarea.getAttribute('data-contenttype');
const reportReason = reportDetailsTextarea.value;



$.ajax({
    url: '/Forum/Report?contentId=${contentId}&reportReason=${reportReason}&contentType=${contentType}',
type: 'POST',
data: {contentId: contentId, reportReason: reportReason, contentType: contentType },
success: function (response) {
    console.log(response);
        },
error: function (error) {
    console.error(error);
        }
    });


modal.close();
});
