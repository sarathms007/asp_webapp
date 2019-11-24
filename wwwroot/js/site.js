// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function getSentiment(userInput) {
    return fetch(`Index?handler=AnalyzeSentiment&text=${userInput}`)
        .then((response) => {
            return response.text();
        })
}

function updateMarker(markerPosition, sentiment) {
    $("#markerPosition").attr("style", `left:${markerPosition}%`);
    $("#markerValue").text(sentiment);
}

function updateSentiment() {

    var userInput = $("#Message").val();

    getSentiment(userInput)
        .then((sentiment) => {
            var pos = document.getElementById("Pos");
            var neg = document.getElementById("Neg");
            var nut = document.getElementById("Nut");
           
            switch (sentiment) {
                case "Positive":
                    updateMarker(100.0, sentiment);
                    pos.style.display = "block"
                    neg.style.display = "none"
                    nut.style.display = "none"
                    break;
                case "Negative":
                    updateMarker(0.0, sentiment);
                    pos.style.display = "none"
                    neg.style.display = "block"
                    nut.style.display = "none"
                    break;
                default:
                    updateMarker(45.0, "Neutral");
                    pos.style.display = "none"
                    neg.style.display = "none"
                    nut.style.display = "block"
            }
        });
}

$("#Message").on('change input paste', updateSentiment)
