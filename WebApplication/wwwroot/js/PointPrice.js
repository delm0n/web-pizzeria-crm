const price = document.querySelector('.check_number');


    price.oninput = function () {
        if (price.value.indexOf(',') != -1) {
            let arr = price.value.split('')
            arr[price.value.indexOf(',')] = ".";
            price.value = arr.join('')
        }
    }

