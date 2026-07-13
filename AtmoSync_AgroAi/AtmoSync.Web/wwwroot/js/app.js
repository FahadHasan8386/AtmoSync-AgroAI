let tempChartInstance;
let humidityChartInstance;
let mq7ChartInstance;
let mq136ChartInstance;



function getCommonOptions() {
    return {

        responsive: true,

        maintainAspectRatio: false,

        plugins: {

            legend: {
                position: 'bottom'
            }

        },


        scales: {

            y: {

                beginAtZero: true

            }

        }

    };
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

                borderWidth: 2,

                tension: 0.4,

                fill: true

            }]

        },


        options: getCommonOptions()

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

                borderWidth: 2,

                tension: 0.4,

                fill: true

            }]

        },


        options: getCommonOptions()


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

                borderWidth: 2,

                tension: 0.4,

                fill: true


            }]


        },


        options: getCommonOptions()



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

                borderWidth: 2,

                tension: 0.4,

                fill: true


            }]


        },


        options: getCommonOptions()


    });



}