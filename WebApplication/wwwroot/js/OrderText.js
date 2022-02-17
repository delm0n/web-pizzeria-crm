const buttons = document.querySelectorAll('.text_more');
const text_orders = document.querySelectorAll('.text_order');

for (let i = 0; i < buttons.length; i++) {

    buttons[i].addEventListener('click', e => {
        text_orders[i].classList.toggle('text_order_more');
        if (buttons[i].textContent == "+") buttons[i].textContent = "-";
        else buttons[i].textContent = "+";
    })
}
