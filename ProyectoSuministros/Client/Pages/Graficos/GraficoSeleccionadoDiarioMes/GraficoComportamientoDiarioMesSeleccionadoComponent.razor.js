function initializeChartComportamientoDiarioSeleccionado(labels, dataClientes, dataSimsa) {
    var ctx = document.getElementById('myChartComportamientoSeleccionado').getContext('2d');

    // Si ya existe el gráfico, destrúyelo para evitar la duplicación
    if (window.myChart) {
        window.myChart.destroy();
    }

    // Crea el gráfico asegurando que las dimensiones no cambien con responsive y aspect ratio
    window.myChart = new Chart(ctx, {
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
            responsive: true,  // El gráfico se ajusta al tamaño del contenedor
            maintainAspectRatio: false,  // Evita que Chart.js mantenga una relación de aspecto fija
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

