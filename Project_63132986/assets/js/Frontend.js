

const navBtn = document.getElementById('nav_bar');
const cancelBtn = document.getElementById('cancel_btn');
const sidenav = document.getElementById('sidenav');
const modal = document.getElementById('modal');

navBtn.addEventListener("click", function () {
    sidenav.classList.add('show');
    modal.classList.add('showModal');
});

cancelBtn.addEventListener('click', function () {
    sidenav.classList.remove('show');
    modal.classList.remove('showModal');
});

window.addEventListener('click', function (event) {
    if (event.target === modal) {
        sidenav.classList.remove('show');
        modal.classList.remove('showModal');
    }
});

