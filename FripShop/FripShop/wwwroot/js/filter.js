﻿document.getElementById("filter_submit").addEventListener("click", filter); 

function filter() {
    //Grab your values
    var search = document.getElementById('search').value;

    var gender = '';
    if (document.getElementById('man').checked)
        gender = 'man'
    if (document.getElementById('woman').checked)
        gender = 'woman'
    if (document.getElementById('unisex').checked)
        gender = 'unisex'
    if (document.getElementById('child').checked)
        gender = 'child'


    var category = '';
    if (document.getElementById('top').checked)
        category = 'top'
    if (document.getElementById('pants').checked)
        category = 'pants'
    if (document.getElementById('shoes').checked)
        category = 'shoes'
    if (document.getElementById('accessories').checked)
        category = 'accessories'

    var minPrice = document.getElementById('minPrice').value;
    var maxPrice = document.getElementById('maxPrice').value;

    var conditionMin = '';
    if (document.getElementById('cond_0').checked)
        conditionMin = '0'
    if (document.getElementById('cond_1').checked)
        conditionMin = '1'
    if (document.getElementById('cond_2').checked)
        conditionMin = '2'
    if (document.getElementById('cond_3').checked)
        conditionMin = '3'
    if (document.getElementById('cond_4').checked)
        conditionMin = '4'
    if (document.getElementById('cond_5').checked)
        conditionMin = '5'

    var sortBy = '';
    if (document.getElementById('date').checked)
        sortBy = 'date'
    if (document.getElementById('price').checked)
        sortBy = 'price'
    if (document.getElementById('rating').checked)
        sortBy = 'rating'
    if (document.getElementById('condition').checked)
        sortBy = 'condition'

    var ascending = '';
    if (document.getElementById('ascending').checked)
        ascending = 'true';
    if (document.getElementById('descending').checked)
        ascending = 'false';

    var href = '?gender=' + gender +
        '&category=' + category +
        '&minprice=' + minPrice +
        '&maxPrice=' + maxPrice +
        '&conditionMin=' + conditionMin +
        '&sortBy=' + sortBy +
        '&ascending=' + ascending +
        '&search=' + search;

    console.log(href)

    window.location.href = href;
}