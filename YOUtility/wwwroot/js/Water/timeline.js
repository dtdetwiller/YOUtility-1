/**
 * The timeline bar chart
 */

function getData(jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec, year) {

    const ctx = document.getElementById('timeline-chart').getContext('2d');

    const timeline = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'Novembver', 'December'],
            datasets: [{
                label: 'Gallons of Water',
                data: [jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec],
                backgroundColor: [
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68'
                ],
                borderColor: [
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68',
                    '#5DAA68'
                ],
                borderWidth: 1
            }]
        },
        options: {
            title: {
                display: true,
                text: "Water usage for " + year
            },
            responsive: true,
            maintainAspectRatio: true
        },

    });
}

