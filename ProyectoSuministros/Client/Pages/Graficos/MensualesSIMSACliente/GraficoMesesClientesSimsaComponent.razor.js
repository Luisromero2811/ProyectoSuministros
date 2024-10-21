function initializeChartMesesClientesSimsa(labels, data) {
    var ctx = document.getElementById('myChartSIMSACliente').getContext('2d');
    console.log(labels);
    console.log(data);
    var mychart = new Chart(ctx, {
        type: 'bar', // Tipo de gráfica (puede ser 'line', 'bar', 'pie', etc.)
        data: {
            labels: labels,
            datasets: [{
                label: 'Compra Mensual Cliente y SIMSA',
                data: data,
                backgroundColor: [
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                    'rgba(182, 17, 249, 0.2)',
                ],
                borderColor: [
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                    'rgba(84, 0, 118, 1)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
