const hamBurger = document.querySelector(".toggle-btn");
const sidebar = document.querySelector("#sidebar");

if (sidebar) {
    sidebar.classList.toggle("expand");
}

if (hamBurger && sidebar) {
    hamBurger.addEventListener("click", function () {
        sidebar.classList.toggle("expand");
    });
}

function validarRut(rut) {
    rut = rut.replace(/[^0-9kK]/g, "");
    if (rut.length < 2) return false;

    const cuerpo = rut.slice(0, -1);
    const dv = rut.slice(-1).toUpperCase();

    let suma = 0;
    let multiplo = 2;

    for (let i = cuerpo.length - 1; i >= 0; i--) {
        suma += multiplo * parseInt(cuerpo[i]);
        multiplo = multiplo < 7 ? multiplo + 1 : 2;
    }

    const dvEsperado = 11 - (suma % 11);
    const dvFinal = dvEsperado === 11 ? "0" : dvEsperado === 10 ? "K" : dvEsperado.toString();

    return dv === dvFinal;
}


function formatearRut(rut) {
    rut = rut.replace(/[^0-9kK]/g, "");
    if (rut.length > 1) {
        let cuerpo = rut.slice(0, -1);
        let dv = rut.slice(-1);

        cuerpo = cuerpo.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

        return `${cuerpo}-${dv}`;
    }
    return rut;
}


