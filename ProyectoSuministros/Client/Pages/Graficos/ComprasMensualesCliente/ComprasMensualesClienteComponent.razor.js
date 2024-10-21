function initializeChartComprasMensualesCliente(labels, data) {
    var ctx = document.getElementById('myChartComprasMensuales').getContext('2d');
    console.log(labels);
    console.log(data);
    var mychart = new Chart(ctx, {
        type: 'bar', // Tipo de gráfica (puede ser 'line', 'bar', 'pie', etc.)
        data: {
            labels: labels,
            datasets: [{
                label: 'Compras Mensuales Clientes 2023 - 2024',
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

