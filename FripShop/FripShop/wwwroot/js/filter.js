document.getElementById("filter_submit").addEventListener("click", filter);
if (document.getElementById("previous_page"))
    document.getElementById("previous_page").addEventListener("click", filter_previous_page); 
if (document.getElementById("next_page"))
    document.getElementById("next_page").addEventListener("click", filter_next_page); 

if (document.getElementById("filter_reset"))
    document.getElementById("filter_reset").addEventListener("click", filter_reset);

function filter() {
    paginated_filter(0);
}

function filter_previous_page() {
    paginated_filter(-1);
}

function filter_next_page() {
    paginated_filter(1);
}

function paginated_filter(relative_page = 0) {
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

    var conditionMin = document.getElementById('conditionMin').value;

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

    var pageRes = "1";
    var page = document.getElementById('page').value;
    if (page != '') {
        pageVal = parseInt(page);
        pageTemp = pageVal + relative_page;
        if (pageTemp <= 0)
            pageRes = "1";
        else
            pageRes = pageTemp.toString();
    }

    var pageSizeRes = "12";
    var pageSize = document.getElementById('pageSize').value;
    if (pageSize != '') {
        pageSizeVal = parseInt(pageSize)
        if (pageSizeVal <= 0)
            pageSizeRes = "1";
        else
            pageSizeRes = pageSizeVal.toString();
    }

    var href = '?gender=' + gender +
        '&category=' + category +
        '&minprice=' + minPrice +
        '&maxPrice=' + maxPrice +
        '&conditionMin=' + conditionMin +
        '&sortBy=' + sortBy +
        '&ascending=' + ascending +
        '&search=' + search +
        '&page=' + pageRes +
        '&pageSize=' + pageSizeRes;

    console.log(href)

    window.location.href = href;
}

function filter_reset() {
    document.getElementById('search').value = "";

    document.getElementById('man').checked = false;
    document.getElementById('woman').checked = false;
    document.getElementById('unisex').checked = false;
    document.getElementById('child').checked = false;

    document.getElementById('top').checked = false;
    document.getElementById('pants').checked = false;
    document.getElementById('shoes').checked = false;
    document.getElementById('accessories').checked = false;

    document.getElementById('minPrice').value = null;
    document.getElementById('maxPrice').value = null;

    document.getElementById('conditionMin').value = null;

    document.getElementById('date').checked = false;
    document.getElementById('price').checked = false;
    document.getElementById('rating').checked = false;
    document.getElementById('condition').checked = false;

    document.getElementById('ascending').checked = false;
    document.getElementById('descending').checked = false;

    document.getElementById('pageSize').value = null;
}