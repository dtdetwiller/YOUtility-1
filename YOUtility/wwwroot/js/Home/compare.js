function getMonthData() {
    new Chart(document.getElementById("line-chart"), {
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