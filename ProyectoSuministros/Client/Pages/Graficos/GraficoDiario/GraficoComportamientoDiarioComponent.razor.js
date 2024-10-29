function initializeChartComportamientoDiario(labels, dataClientes, dataSimsa) {
    var ctx = document.getElementById('myChartComportamiento').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels, // Fechas
            datasets: [{
                label: 'Clientes',
                data: dataClientes,
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 2,
                fill: false,
            },
            {
                label: 'SIMSA',
                data: dataSimsa,
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 2,
                fill: false
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                tooltip: {
                    enabled: true, // Activa los tooltips
                    mode: 'index', // Muestra todos los tooltips en el eje X
                    intersect: false, // Evita que se oculte si no hay intersección
                    external: function (context) {
                        // Dejar esta opción vacía permite que los tooltips se gestionen normalmente
                    }
                }
            }
        }
    });

    // Activa los tooltips para todos los puntos de manera permanente
    const datasets = myChart.data.datasets;
    const elements = [];

    datasets.forEach((dataset, datasetIndex) => {
        myChart.getDatasetMeta(datasetIndex).data.forEach((element, index) => {
            elements.push({
                datasetIndex: datasetIndex,
                index: index
            });
        });
    });

    // Establece los elementos activos para que los tooltips permanezcan visibles
    myChart.setActiveElements(elements);
    myChart.update();
}

