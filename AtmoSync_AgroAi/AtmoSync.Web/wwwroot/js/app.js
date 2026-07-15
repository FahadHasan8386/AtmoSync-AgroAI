let tempChartInstance;
let humidityChartInstance;
let mq7ChartInstance;
let mq136ChartInstance;

if (typeof Chart !== 'undefined' && typeof ChartDataLabels !== 'undefined') {
    Chart.register(ChartDataLabels);
}

function createGradient(ctx, colorStart, colorEnd) {
    const gradient = ctx.createLinearGradient(0, 0, 0, ctx.canvas.height);
    gradient.addColorStop(0, colorStart);
    gradient.addColorStop(1, colorEnd);
    return gradient;
}

function getCommonOptions(values) {
    const minValue = Math.min(...values);
    const maxValue = Math.max(...values);
    const padding = Math.max((maxValue - minValue) * 0.08, 1);

    return {
        responsive: true,
        maintainAspectRatio: false,
        layout: {
            padding: {
                top: 12,
                right: 16,
                left: 8,
                bottom: 12
            }
        },
        plugins: {
            legend: {
                position: 'bottom',
                labels: {
                    boxWidth: 10,
                    padding: 12
                }
            },
            datalabels: {
                display: true,
                anchor: 'end',
                align: 'top',
                color: '#0f172a',
                font: {
                    size: 10,
                    weight: '700'
                },
                formatter: function(value) {
                    return value;
                },
                backgroundColor: 'rgba(255,255,255,0.9)',
                borderRadius: 4,
                padding: 4
            }
        },
        scales: {
            x: {
                grid: {
                    color: 'rgba(148, 163, 184, 0.15)'
                },
                ticks: {
                    maxRotation: 0,
                    autoSkip: true,
                    maxTicksLimit: 8
                }
            },
            y: {
                beginAtZero: false,
                suggestedMin: Math.max(minValue - padding, minValue * 0.95),
                suggestedMax: Math.max(maxValue + padding, maxValue * 1.05),
                grid: {
                    color: 'rgba(148, 163, 184, 0.12)'
                }
            }
        },
        elements: {
            point: {
                radius: 3,
                hoverRadius: 5,
                borderWidth: 1
            }
        }
    };
}
function createPointPalette(length) {
    const palette = [
        '#ef4444',
        '#f97316',
        '#eab308',
        '#10b981',
        '#0ea5e9',
        '#8b5cf6',
        '#ec4899',
        '#6366f1'
    ];
    return Array.from({ length }, (_, index) => palette[index % palette.length]);
}
function renderTempChart(labels, values) {

    const ctx = document.getElementById('tempChart');

    if (!ctx) return;

    if (tempChartInstance)
        tempChartInstance.destroy();

    tempChartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Temperature °C',
                data: values,
                borderWidth: 3,
                tension: 0.35,
                fill: true,
                backgroundColor: createGradient(ctx, 'rgba(59, 130, 246, 0.28)', 'rgba(59, 130, 246, 0.04)'),
                borderColor: 'rgba(37, 99, 235, 0.95)',
                pointRadius: 6,
                pointHoverRadius: 8,
                pointBackgroundColor: createPointPalette(values.length),
                pointBorderColor: '#ffffff',
                pointBorderWidth: 2,
                pointHoverBackgroundColor: '#ffffff',
                pointHoverBorderColor: 'rgba(37, 99, 235, 1)'
            }]
        },
        options: getCommonOptions(values)
    });

}

function renderHumidityChart(labels, values) {

    const ctx = document.getElementById('humidityChart');

    if (!ctx) return;

    if (humidityChartInstance)
        humidityChartInstance.destroy();

    humidityChartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Humidity %',
                data: values,
                borderWidth: 3,
                tension: 0.35,
                fill: true,
                backgroundColor: createGradient(ctx, 'rgba(14, 165, 233, 0.28)', 'rgba(14, 165, 233, 0.04)'),
                borderColor: 'rgba(14, 165, 233, 0.95)',
                pointRadius: 6,
                pointHoverRadius: 8,
                pointBackgroundColor: createPointPalette(values.length),
                pointBorderColor: '#ffffff',
                pointBorderWidth: 2,
                pointHoverBackgroundColor: '#ffffff',
                pointHoverBorderColor: 'rgba(14, 165, 233, 1)'
            }]
        },
        options: getCommonOptions(values)
    });
}

function renderMQ7Chart(labels, values) {

    const ctx = document.getElementById('mq7Chart');

    if (!ctx) return;

    if (mq7ChartInstance)
        mq7ChartInstance.destroy();

    mq7ChartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'CO Level PPM',
                data: values,
                borderWidth: 3,
                tension: 0.35,
                fill: true,
                backgroundColor: createGradient(ctx, 'rgba(251, 191, 36, 0.28)', 'rgba(251, 191, 36, 0.04)'),
                borderColor: 'rgba(245, 158, 11, 0.95)',
                pointRadius: 6,
                pointHoverRadius: 8,
                pointBackgroundColor: createPointPalette(values.length),
                pointBorderColor: '#ffffff',
                pointBorderWidth: 2,
                pointHoverBackgroundColor: '#ffffff',
                pointHoverBorderColor: 'rgba(245, 158, 11, 1)'
            }]
        },
        options: getCommonOptions(values)
    });
}

function renderMQ136Chart(labels, values) {

    const ctx = document.getElementById('mq136Chart');

    if (!ctx) return;

    if (mq136ChartInstance)
        mq136ChartInstance.destroy();

    mq136ChartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'H2S Level',
                data: values,
                borderWidth: 3,
                tension: 0.35,
                fill: true,
                backgroundColor: createGradient(ctx, 'rgba(34, 197, 94, 0.28)', 'rgba(34, 197, 94, 0.04)'),
                borderColor: 'rgba(16, 185, 129, 0.95)',
                pointRadius: 6,
                pointHoverRadius: 8,
                pointBackgroundColor: createPointPalette(values.length),
                pointBorderColor: '#ffffff',
                pointBorderWidth: 2,
                pointHoverBackgroundColor: '#ffffff',
                pointHoverBorderColor: 'rgba(16, 185, 129, 1)'
            }]
        },
        options: getCommonOptions(values)
    });
}