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