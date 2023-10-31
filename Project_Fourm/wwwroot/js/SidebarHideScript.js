// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    const divElement = document.querySelector(".sidebar_hide");
    const iconElement = divElement.querySelector(".material-icons");

    const sidebarCollapsed = "sidebar--collapsed";
    const sidebar = document.querySelector(".sidebar")
    const sidebarHide = sidebar.querySelector(".sidebar_hide");



    sidebarHide.addEventListener("click", () => {
    sidebar.classList.toggle(sidebarCollapsed);

    if (iconElement.textContent === "chevron_left")
    iconElement.textContent = "chevron_right";
    else
    iconElement.textContent = "chevron_left";
            });
