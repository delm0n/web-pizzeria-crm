const showorder = document.querySelector('.show_order');
const order = document.querySelector('.order_hidden');

window.onload = function () {
    setTimeout(ImmitCLick, 700);
}

function ImmitCLick() {
    showorder.click();
}

showorder.addEventListener('click', e => {
      order.classList.toggle('order_active');

    if (showorder.textContent == "Показать заказ") {
        showorder.textContent = "Скрыть заказ";
    }
    else showorder.textContent = "Показать заказ";

});
