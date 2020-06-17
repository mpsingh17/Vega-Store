$(document).ready(function () {

    $("#confirmDeleteBtn").on("click", function (e) {
        e.preventDefault();

        //console.log(e.target);

        var url = e.target.href;

        var xsrfToken = $("#RequestVerficationToken").val();

        //console.log(xsrfToken);

        if (url && xsrfToken) {
            $.ajax(url, {
                method: "DELETE",
                headers: {
                    "RequestVerificationToken": xsrfToken
                },
                success: function (data, textStatus, jqXHR) {
                    $confirmDeleteModal.modal("hide");
                    window.location.reload();
                },
                statusCode: {
                    404: function () {
                        $confirmDeleteModal.modal("hide");
                        alert("Resource not found. Please try again!");
                    },
                    400: function () {
                        $confirmDeleteModal.modal("hide");
                        alert("Invalid request!");
                    }
                }
            });
        }
    });

    var $confirmDeleteModal = $("#confirmDeleteModal").on("shown.bs.modal", function (e) {

        // Contains href value to send Ajax request.
        var $relatedTarget = $(e.relatedTarget);

        $(this).find("#confirmDeleteBtn").attr("href", $relatedTarget.attr("href"));

    });
});