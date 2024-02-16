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
