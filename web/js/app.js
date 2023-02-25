function send() {

    var marka = document.getElementById("marka").value;
    var gads = document.getElementById("gads").value;
    var ipasnieks = document.getElementById("ipasnieks").value;

    var dataToSend = {
        "marka": marka,
        "gads": parseInt(gads),
        "ipasnieks": ipasnieks
    }
    
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            alert("Sludinājums saglabāts!");
            document.getElementById("marka").value = null;
            document.getElementById("gads").value = null;
            document.getElementById("ipasnieks").value = null;
        }
    }
    xhttp.open("POST", "https://localhost:7076/sludinajumi", true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send(JSON.stringify(dataToSend));
}

// Ar šo mēs pasakām, ka vēlamies izpildīt Javascript
// kodu, kad dokuments ir ielādējies
document.addEventListener("DOMContentLoaded", () => {
    
    // Atrodam elementu, kur liksim sludinajumus
    var sludinajumuKonteineris = document.getElementById("sludinajumi");

    // Ja elements ir, tad aizpildīsim to ar datiem no datubāzes
    if (sludinajumuKonteineris) {

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {

                var sludinajumi = JSON.parse(this.responseText);

                for (let i = 0; i < sludinajumi.length; i++) {

                    // Izveidojam sludinājuma HTML elementu
                    var sludinajumaElements = izveidotSludinajumaElementu(sludinajumi[i]);

                    // Pievienojam to konteinerim
                    sludinajumuKonteineris.appendChild(sludinajumaElements);
                }
            }
        }
        xhttp.open("GET", "https://localhost:7076/sludinajumi", true);
        xhttp.setRequestHeader("Content-type", "application/json");
        xhttp.send();

    }

});

// Šī funkcija uzģenerēs jaunu sludinājuma elementu
// Funkcijā tiek izmantota template strings pieeja
// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Template_literals
function izveidotSludinajumaElementu(sludinajums) {

    // Izveidojam jaunu DIV elementu
    var sludinajumaElements = document.createElement('div');
    // Uzstādam tā klasi
    sludinajumaElements.className = "col-sm-6 col-md-4 col-lg-3 card mx-3 mb-4";

    // Aizpildām tā saturu ar HTML kodu un datiem no sludinajuma
    sludinajumaElements.innerHTML  = `
        <div class="card-body">
            <h5 class="card-title">${sludinajums.marka} ${sludinajums.gads}</h5>
            <h6 class="card-subtitle mb-2 text-muted">${sludinajums.ipasnieks}</h6>
            <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
            <a href="https://localhost:7076/sludinajumi/${sludinajums.id}" class="btn btn-primary">Apskatīt</a>
        </div>
    `;

    return sludinajumaElements;

}