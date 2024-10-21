window.initializeChartComprasAñoActual = (meses, volumenCliente, volumenSimsa) => {
    var ctx = document.getElementById('myChartMes').getContext('2d');
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: meses, // Eje X (meses)
            datasets: [
                {
                    label: 'Cliente',
                    data: volumenCliente, // Volumen del cliente
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1
                },
                {
                    label: 'SIMSA',
                    data: volumenSimsa, // Volumen de SIMSA
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                }
            ]
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