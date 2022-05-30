


$(function () {

    $('form').submit(async e => {

        // prevent page refresh.
        e.preventDefault();

        // save the search input.
        const q = $('#search').val();

        //$('.accordion').load('/Ratings/Search2?query='+q);

        const response = await fetch('/Ratings/Search?query=' + q);
        const data = await response.json();

        const template = $('#template').html();
        let results = '';
        for (var item in data) {
            let row = template;
            for (var key in data[item]) {
                if (key === "time") {
                    data[item][key] = data[item][key].replaceAll("T", " ");
                }
                //console.log('{' + key + '}', data[item][key]);
                row = row.replaceAll('{' + key + '}', data[item][key]);
                row = row.replaceAll('%7B' + key + '%7D', data[item][key]);          
            }
            results += row;
        }

        $('.accordion').html(results);

        
    });

});