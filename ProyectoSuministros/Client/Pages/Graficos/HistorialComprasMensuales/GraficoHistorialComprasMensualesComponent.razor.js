window.initializeChartHistorialCompras = (meses, compras) => {
    const ctx = document.getElementById('myChartHistorial').getContext('2d');

    // Agrupamos los productos y preparamos los datos para Chart.js
    const productos = [...new Set(compras.map(c => c.producto))];
    const datasets = productos.map(producto => {
        return {
            label: producto,
            data: meses.map(mes => {
                const compra = compras.find(c => c.producto === producto && c.mes === mes);
                return compra ? compra.volumen : 0;
            }),
            backgroundColor: getRandomColor(),
            borderColor: getRandomColor(),
            borderWidth: 1
        };
    });

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: meses,
            datasets: datasets
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

// Función para generar colores aleatorios
function getRandomColor() {
    const r = Math.floor(Math.random() * 255);
    const g = Math.floor(Math.random() * 255);
    const b = Math.floor(Math.random() * 255);
    return `rgba(${r}, ${g}, ${b}, 0.5)`;
}