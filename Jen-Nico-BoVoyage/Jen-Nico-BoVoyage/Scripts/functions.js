function deleteDestination(id) {
    $("#deleteModal").modal("show");
    $("#confirm").on("click", function () {
        $.ajax({
            url: "http://localhost:54746/api/destinations/" + id,
            method: 'delete',
            success: function () {
                window.location.href = "http://localhost:54746/Html/AfficherDestinations.html";
                alert("Destination supprimée");
            }
        });
    });
};

function editDestination(id) {
    let url = "http://localhost:54746/api/destinations/" + id;
    $("#editModalForm").trigger("reset");
    $("#editModal").modal();
    $.ajax({
        url: url,
        method: 'GET',
        success: function (data) {
            $("#destinationID").val(data.ID);
            $("#editContinent").val(data.Continent);
            $("#editPays").val(data.Pays);
            $("#editRegion").val(data.Region);
            $("#editDescription").val(data.Description);
        }
    });
    $("#editModalForm").submit(function (ev) {
        let obj = {
            ID: $("#destinationID").val(),
            Continent: $("#editContinent").val(),
            Pays: $("#editPays").val(),
            Region: $("#editRegion").val(),
            Description: $("#editDescription").val()
        };

        $.ajax({
            url: url,
            method: "PUT",
            data: obj,
            success: function () {
                alert(`Destination modifiée`);
                window.location.href = "http://localhost:54746/Html/AfficherDestinations.html";
                $("#editModal").modal("hide");
            }
        });
        ev.preventDefault();
    });
};


