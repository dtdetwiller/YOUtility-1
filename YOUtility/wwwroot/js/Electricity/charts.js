

function getMonthData(thisMonth, lastMonth) {

    //Chart function for monthly electricity usage
    const bar = document.getElementById("bar-chart").getContext("2d");

    // Bar chart
    const barChart = new Chart(bar, {
        type: 'bar',
        data: {
            labels: [
                "Last Month",
                "This Month"
            ],
            datasets: [
                {
                    backgroundColor: [
                        "#5DAA68",
                        "#5DAA68"
                    ],
                    data: [
                        lastMonth / 30,
                        thisMonth / 30
                    ]
                }
            ]
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: 'Daily Average Usage (Watts)'
            },
            responsive: true,
            maintainAspectRatio: false
        }
    });
}


function getAllUtilityMonthData(water, gas, elec) {
    const pie = document.getElementById("pie-chart").getContext("2d");

    const pieChart = new Chart(pie, {
        type: 'pie',
        data: {
            labels: [
                'Electricity',
                'Other Utilities'
            ],
            datasets: [{
                data: [
                    elec,
                    gas + water
                ],
                backgroundColor: [
                    'rgb(54, 162, 235)',
                    'rgb(255, 99, 132)'
                ],
                hoverOffset: 4
            }]
        },
        options: {
            legend: { display: true },
            title: {
                display: true,
                text: 'Amount spent on electricity / other utilities (USD)'
            },
            responsive: true,
            maintainAspectRatio: false
        }
    });
}

function getYearData() {
    new Chart(document.getElementById("line-chart-1"), {
        type: 'line',
        data: {
            labels: ['January', 'February', 'March', 'April', 'May', 'June'],
            datasets: [{
                data: [86, 114, 106, 106, 107, 111],
                label: "YOU",
                borderColor: "#3e95cd",
                fill: false
            }, {
                data: [282, 350, 411, 502, 635, 809],
                label: "user",
                borderColor: "#8e5ea2",
                fill: false
            }, {
                data: [168, 170, 178, 190, 203, 276],
                label: "user",
                borderColor: "#3cba9f",
                fill: false
            }, {
                data: [40, 20, 10, 16, 24, 38],
                label: "user",
                borderColor: "#e8c3b9",
                fill: false
            }, {
                data: [6, 3, 2, 2, 7, 26],
                label: "user",
                borderColor: "#c45850",
                fill: false
            }
            ]
        },
        options: {
            title: {
                display: true,
                text: 'Utility Cost of Random Users Over 6 Months'
            },
            responsive: true,
            maintainAspectRatio: false
        }
    });
}

function getThisMonthData(thisMonth) {

    //Chart function for monthly electricity usage
    const bar = document.getElementById("bar-chart-2").getContext("2d");

    // Bar chart
    const barChart = new Chart(bar, {
        type: 'bar',
        data: {
            labels: [
                "YOU",
                "Household of 1",
                "Household of 2",
                "Household of 4"
            ],
            datasets: [
                {
                    backgroundColor: [
                        "#5DAA68",
                        "#5DAA68",
                        "#5DAA68",
                        "#5DAA68"
                    ],
                    data: [
                        thisMonth,
                        255,
                        510,
                        1020  
                    ]
                }
            ]
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: 'Monthly Average Usage (Watts)'
            },
            responsive: true,
            maintainAspectRatio: false
        }
    });
}