function deleteClient(id) {
    $("#deleteModal").modal("show");
    $("#confirm").on("click", function () {
        $.ajax({
            url: "http://localhost:54746/api/clients/" + id,
            method: 'delete',
            success: function () {
                window.location.href = "http://localhost:54746/Html/Clients.html";
                alert("Client supprimé");
            }
        });
    });
};

function editClient(id) {

    let url = "http://localhost:54746/api/clients/" + id;
    $("#editModalForm").trigger("reset");
    $("#editModal").modal();
    $.ajax({
        url: url,
        method: 'GET',
        success: function (data) {
            $("#clientID").val(data.ID);
            $("#editPrenom").val(data.Prenom);
            $("#editCivilite").val(data.Civilite);
            $("#editNom").val(data.Nom);
            $("#editTelephone").val(data.Telephone);
            $("#editAdresse").val(data.Adresse);
            $("#editEmail").val(data.Email);
            $("#editBirthdate").val(data.DateNaissance);
        }
    });
 
    $("#editModalForm").submit(function(ev) {
        let obj = {
            ID: $("#clientID").val(),
            Civilite: $("#editCivilite").val(),
            Prenom: $("#editPrenom").val(),
            Nom: $("#editNom").val(),
            Telephone: $("#editTelephone").val(),
            Adresse: $("#editAdresse").val(),
            Email: $("#editEmail").val(),
            DateNaissance: $("#editBirthdate").val()
        };

        $.ajax({
            url: url,
            method: "PUT",
            data: obj,
            success: function () {
                alert(`Client modifié`);
                window.location.href = "http://localhost:54746/Html/Clients.html";
                $("#editModal").modal("hide");
            }
        });
        ev.preventDefault();
    });
};


