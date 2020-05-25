$(function () {

    
    $(document).ready(function () {

        $("#a1").click(function () {
            var Username = document.getElementById("username").value;
            var Password = document.getElementById("password").value;
            if (Username == "" || Username == undefined) {
                alert("Please enter or username");
                return;
            }
            if (Password == "" || Password == undefined) {
                alert("Please enter or Password");
                return;
            }
            $.ajax({
                url: "Restaurant/Login",
                data: { 'username': Username, 'password': Password },
                type: "POST",
                success: function (data) {
                    if (data == "Success") {
                        //alert(data);
                        location.href = "../Restaurant/HomeIndex";
                    }
                    else {
                        location.href = "../Restaurant/Login";
                    }
                }
            });

        });

    });

});
function Register() {
    $.ajax({
        url: "Restaurant/Register",
        data: "",
        type: "POST",
        success: function (data) {
            
        }
    });
    
}