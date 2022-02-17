//window.onload = function () {
//    if (sessionStorage.getItem('order') == 1) 
//        order.classList.add('order_active');
    

//}

const showorder = document.querySelector('.show_order');
const order = document.querySelector('.order_hidden');

//document.querySelector('.button_exit').addEventListener('click', e => {
//    sessionStorage.setItem('order', 2);
//})

showorder.addEventListener('click', e => {
//    sessionStorage.setItem('order', 1);
      order.classList.toggle('order_active');

});
