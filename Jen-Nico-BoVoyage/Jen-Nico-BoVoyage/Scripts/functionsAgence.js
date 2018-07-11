function deleteAgence(id) {
    $("#deleteModal").modal("show");
    $("#confirm").on("click", function () {
        $.ajax({
            url: "http://localhost:54746/api/Agences/" + id,
            method: 'delete',
            success: function () {
                window.location.href = "http://localhost:54746/Html/Agences.html";
                alert("Agence supprimée");
            }
        });
    });

};

function editAgence(id) {
    let url = "http://localhost:54746/api/Agences/" + id;
    $("#editModaForm").trigger("reset");
    $("#editModal").modal();
    $.ajax({
        url: url,
        method: 'GET',
        success: function (data) {
            $("#agenceID").val(data.ID);
            $("#editNom").val(data.Nom);
        }
    });
    $("#editModalForm").submit(function (ev) {
        let obj = {
            ID: $("#agenceID").val(),
            Nom: $("#editNom").val()
        };

        $.ajax({
            url: url,
            method: 'PUT',
            data: obj,
            success: function () {
                alert("Agence modifiée");
                window.location.href = "http://localhost:54746/Html/Agences.html";
                $("#editModal").modal("hide");
            }

        });
        ev.preventDefault();
    });


};