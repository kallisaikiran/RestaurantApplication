$(function () {


    $(document).ready(function () {

        $("#k22").click(function () {

            var Username = document.getElementById("Username").value;
            var Password = document.getElementById("Password").value;
            
            if (Username === "" || Username === undefined) {
                alert("Please enter username");
                return;
            }
            if (Password === "" || Password === undefined) {
                alert("Please enter Code");
                return;
            }
            
            $.ajax({
                url: "SaveRegister",
                data: { 'username': Username, 'password': Password },
                type: "POST",
                success: function (data) {
                    if (data === "1") {
                        //alert(data);
                        location.href = "../Restaurant/Login";
                    }

                }
            });
        });

    });

});