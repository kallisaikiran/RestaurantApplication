$(function () {


    $(document).ready(function () {

        $("#a22").click(function () {
           
            var Username = document.getElementById("Username").value;
            var Password = document.getElementById("Password").value;
            var Email = document.getElementById("Mail").value;
            if (Username === "" || Username === undefined) {
                alert("Please enter or username");
                return;
            }
            if (Password === "" || Password === undefined) {
                alert("Please enter or Password");
                return;
            }
            if (Mail === "" || Mail === undefined) {
                alert("Please enter or Email");
                return;
            }
            $.ajax({
                url: "SaveRegister",
                data: { 'username': Username, 'password': Password, 'Email': Email },
                type: "POST",
                success: function (data) {
                    if (data === "1") {
                        //alert(data);
                        location.href = "../Restaurant/Login";
                    }
                    else {
                        alert("Try with different Username........");
                    }
                     

                }
            });
        });

    });

});