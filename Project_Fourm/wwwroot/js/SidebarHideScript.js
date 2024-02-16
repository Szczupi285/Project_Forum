const divElement = document.querySelector(".sidebar_hide");
const iconElement = divElement.querySelector(".material-icons");

const sidebarCollapsed = "sidebar--collapsed";
const sidebar = document.querySelector(".sidebar")
const sidebarHide = sidebar.querySelector(".sidebar_hide");

if (localStorage.getItem('SidebarState') === "chevron_right") {
    sidebar.classList.add(sidebarCollapsed);
    iconElement.textContent = "chevron_right";
} else {
    sidebar.classList.remove(sidebarCollapsed);
    iconElement.textContent = "chevron_left";
}



sidebarHide.addEventListener("click", () => {
    sidebar.classList.toggle(sidebarCollapsed);

    if (localStorage.getItem('SidebarState') === "chevron_left"){
        iconElement.textContent = "chevron_right";
        localStorage.setItem('SidebarState', iconElement.textContent);

    }
    else{
        iconElement.textContent = "chevron_left";
        localStorage.setItem('SidebarState', iconElement.textContent);
    }
});