// Store all chart instances
const charts = {};

// Common function
function createChart(canvasId, labels, values, label, borderColor, backgroundColor) {

    const canvas = document.getElementById(canvasId);

    if (!canvas)
        return;

    if (charts[canvasId]) {
        charts[canvasId].destroy();
    }

    charts[canvasId] = new Chart(canvas, {

        type: "line",

        data: {

            labels: labels,

            datasets: [{

                label: label,

                data: values,

                borderColor: borderColor,

                backgroundColor: backgroundColor,

                fill: true,

                borderWidth: 3,

                tension: .4,

                pointRadius: 4,

                pointHoverRadius: 6,

                pointBackgroundColor: borderColor,

                pointBorderColor: "#ffffff",

                pointBorderWidth: 2

            }]
        },

        options: {

            responsive: true,

            maintainAspectRatio: false,

            interaction: {
                intersect: false,
                mode: "index"
            },

            animation: {
                duration: 800
            },

            plugins: {

                legend: {

                    position: "bottom",

                    labels: {

                        usePointStyle: true,

                        boxWidth: 10,

                        padding: 20

                    }
                },

                tooltip: {

                    backgroundColor: "#1f2937",

                    titleColor: "#fff",

                    bodyColor: "#fff",

                    cornerRadius: 8

                }

            },

            scales: {

                x: {

                    grid: {

                        color: "rgba(0,0,0,.05)"

                    }

                },

                y: {

                    beginAtZero: false,

                    grace: "10%",

                    grid: {

                        color: "rgba(0,0,0,.05)"

                    }

                }

            }

        }

    });

}

// Temperature

window.renderTempChart = function (labels, values) {

    createChart(

        "tempChart",

        labels,

        values,

        "Temperature (°C)",

        "#ef4444",

        "rgba(239,68,68,.15)"

    );

};

// Humidity

window.renderHumidityChart = function (labels, values) {

    createChart(

        "humidityChart",

        labels,

        values,

        "Humidity (%)",

        "#3b82f6",

        "rgba(59,130,246,.15)"

    );

};

// MQ7

window.renderMQ7Chart = function (labels, values) {

    createChart(

        "mq7Chart",

        labels,

        values,

        "CO Level (PPM)",

        "#06b6d4",

        "rgba(6,182,212,.15)"

    );

};

// MQ136

window.renderMQ136Chart = function (labels, values) {

    createChart(

        "mq136Chart",

        labels,

        values,

        "H₂S Level",

        "#f59e0b",

        "rgba(245,158,11,.15)"

    );

};