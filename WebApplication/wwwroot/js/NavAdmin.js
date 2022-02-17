window.onload = function () {
    if (sessionStorage.getItem('inputs') != null) {
        ChangeClass(sessionStorage.getItem('inputs'));
    }

}

const inputs = document.getElementsByClassName('input')
const tabs = document.querySelector('.nav_panel')

document.querySelector('.button_exit').addEventListener('click', e => {
    sessionStorage.setItem('inputs', null);
})

const ChangeClass = el => {

    for (let i = 0; i < tabs.children.length; i++) {
        tabs.children[i].classList.remove('input_wrapper_active');
    }
    tabs.children[el].classList.add('input_wrapper_active');

}


for (let i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener('click', e => {
        storage_work(i);
    })
}

function storage_work(el) {
    sessionStorage.setItem('inputs', el);
}