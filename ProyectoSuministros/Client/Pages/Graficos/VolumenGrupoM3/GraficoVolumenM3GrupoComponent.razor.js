function initializeChartVolumenGrupo(labels, data) {
    var ctx = document.getElementById('myChartVolumenM3').getContext('2d');
    if (window.mychart) {
        window.mychart.destroy();
    }

    window.mychart = new Chart(ctx, {
        type: 'bar', // Tipo de gráfica (puede ser 'line', 'bar', 'pie', etc.)
        data: {
            labels: labels,
            datasets: [{
                label: 'Volumen M3 Grupo',
                data: data,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}