// Global variables to store chart instances
let tempChartInstance, humidityChartInstance, mq7ChartInstance, mq136ChartInstance;

window.addEventListener("DOMContentLoaded", () => {
    console.log("Js loaded properly");
});

function renderTempChart(labels, values) {
    const ctx = document.getElementById('tempChart');
    if (!ctx) return;

    if (tempChartInstance) {
        tempChartInstance.destroy();
    }
    tempChartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Temperature (°C)',
                data: values,
                borderColor: 'rgba(255, 99, 132, 1)',
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderWidth: 2,
                tension: 0.4
            }]
        },
        options: {
            responsive: true,
            scales: { y: { min: 0, max: 100 } }
        }
    });
}

function renderHumidityChart(labels, values) {
    const ctx = document.getElementById('humidityChart');
    if (!ctx) return;

    if (humidityChartInstance) {
        humidityChartInstance.destroy();
    }
    humidityChartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Humidity (%)',
                data: values,
                borderColor: 'rgba(54, 162, 235, 1)',
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderWidth: 2,
                tension: 0.4
            }]
        },
        options: {
            responsive: true,
            scales: { y: { min: 0, max: 100 } }
        }
    });
}

//js chart for mq7
function renderMQ7Chart(labels, values) {
    const ctx = document.getElementById('mq7Chart'); 
    if (!ctx) return;

    if (mq7ChartInstance) {
        mq7ChartInstance.destroy();
    }
    mq7ChartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'CO Level (PPM)',
                data: values,
                borderColor: 'rgba(75, 192, 192, 1)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderWidth: 2,
                tension: 0.4
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    min: 0,
                    max: 200,
                    ticks: { stepSize: 20 }
                }
            }
        }
    });
}

//js chart for Mq136
function renderMQ136Chart(labels, values) {
    const ctx = document.getElementById('mq136Chart');
    if (!ctx) return;

    if (mq136ChartInstance) {
        mq136ChartInstance.destroy();
    }
    mq136ChartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'H2s Level',
                data: values,
                borderColor: 'rgba(75, 192, 192, 1)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderWidth: 2,
                tension: 0.4
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    min: 0,
                    max: 200,
                    ticks: { stepSize: 20 }
                }
            }
        }
    });
}

