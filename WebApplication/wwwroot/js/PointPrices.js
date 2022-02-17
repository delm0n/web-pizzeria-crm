const price = document.querySelector('.check_number');
const price2 = document.querySelector('.check_number2');
const price3 = document.querySelector('.check_number3');

price.oninput = function () {
    if (price.value.indexOf(',') != -1) {
        let arr = price.value.split('')
        arr[price.value.indexOf(',')] = ".";
        price.value = arr.join('')
    }
}

    
price2.oninput = function () {
    if (price2.value.indexOf(',') != -1) {
        let arr = price2.value.split('')
        arr[price2.value.indexOf(',')] = ".";
        price2.value = arr.join('')
    }
}


price3.oninput = function () {
    if (price3.value.indexOf(',') != -1) {
        let arr = price3.value.split('')
        arr[price3.value.indexOf(',')] = ".";
        price3.value = arr.join('')
    }
}
