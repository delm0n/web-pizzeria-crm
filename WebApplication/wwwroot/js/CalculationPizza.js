const pizzaPrice = Number(document.querySelector('.totalPrice').value);
const pizzaMass = Number(document.querySelector('.totalMass').value)

function GetTotal() {

    let totalCost = pizzaPrice;
    let totalMass = pizzaMass;


    [...document.querySelectorAll('.row_item')].forEach((row_item) => {

        totalCost += Number(row_item.querySelector('.price').textContent)
            * Number(row_item.querySelector('.input_beetw').value);

        totalMass += Number(row_item.querySelector('.mass').textContent)
            * Number(row_item.querySelector('.input_beetw').value);
    })

    document.querySelector('.totalPrice').value = totalCost.toFixed(2);
    document.querySelector('.totalMass').value = totalMass;

}

document.querySelector('.table_block_order').addEventListener('click', e => {
    if (e.target.classList.contains('button_minus')) {
        CalculateMinus(e.target.closest('.row_item'));
    }

    if (e.target.classList.contains('button_plus')) {
        CalculatePlus(e.target.closest('.row_item'));
    }
})


let arrOrders = [];

if (document.querySelector('.totalOrder').value != '') {
    let what = JSON.parse(document.querySelector('.totalOrder').value); //это объект


    //разбиваем объект на массив
    for (key in what) {

        for (let i = 0; i < what[key]; i++) {
            arrOrders.push(key)
        }

        for (let i = 0; i < document.querySelectorAll('.nameProduct').length; i++) {
            if (document.querySelectorAll('.nameProduct')[i].textContent == key) {
                document.querySelectorAll('.input_beetw')[i].value = what[key];

                ColorVal(document.querySelectorAll('.input_beetw')[i]);
            }
        }
    }

    GetTotal();
}

//при нажатии на кнопку +
function CalculatePlus(item) {

    if (Number(item.querySelector('.input_beetw').value) < 3) {
        item.querySelector('.input_beetw').value = Number(item.querySelector('.input_beetw').value) + 1;
        ColorVal(item.querySelector('.input_beetw'));
        GetTotal();
        arrOrders.push(item.querySelector('.nameProduct').textContent);
        CountOfReply(arrOrders);
    }

}

//при нажатии на кнопку -
function CalculateMinus(item) {

    if (Number(item.querySelector('.input_beetw').value) > 0) {
        item.querySelector('.input_beetw').value = Number(item.querySelector('.input_beetw').value) - 1;
        ColorVal(item.querySelector('.input_beetw'));
        GetTotal();
        RemoveOneElementInMass(arrOrders, item.querySelector('.nameProduct').textContent);
    }
}

//функция удаляет из массива elem
function RemoveOneElementInMass(array, elem) {

    var index = array.indexOf(elem);
    if (index > -1) {
        array.splice(index, 1);
    }
    CountOfReply(array)

    //очистка поля от {} при пустых значениях
    if (document.querySelector('.totalOrder').value == "{}") {
        document.querySelector('.totalOrder').value = "";
    }

}

//счётчик элементов и вывод их в JSON-формате
function CountOfReply(arr) {

    var count = {};
    arr.forEach(function (i) { count[i] = (count[i] || 0) + 1; });
    document.querySelector('.totalOrder').value = JSON.stringify(count);

}

//изменение цвета
function ColorVal(color) {
    if (Number(color.value) > 0) {
        color.style.color = '#e74c36';
    }
    else {
        color.style.color = 'rgb(84, 84, 84)';
    }
}