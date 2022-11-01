


$("#ViewGrid1").click(function () {
  
    $("#ViewGrid2div").hide();
    $("#ViewGrid1div").show();
});
$("#ViewGrid2").click(function () {
  
    $("#ViewGrid1div").hide();
    $("#ViewGrid2div").show();
});
function NextClick() {
    var TargetPageVal = document.getElementById("TargetPage").value;
    var LastPageVal = document.getElementById("LastPage").value;
    if (TargetPageVal !== LastPageVal) {
        document.getElementById("TargetPage").value = parseInt(TargetPageVal) + 1;
        var form = document.getElementById("frmSearch");
        form.submit();
    }
}

function PrevClick() {
    var TargetPageVal = document.getElementById("TargetPage").value;
    if (TargetPageVal !== '0') {
        document.getElementById("TargetPage").value = parseInt(TargetPageVal) - 1;
        var form = document.getElementById("frmSearch");
        form.submit();
    }
}
function PageClick(pageNumber) {
    if (pageNumber !== "..") {
        var target = parseInt(pageNumber) - 1;
        document.getElementById("TargetPage").value = target;
        var form = document.getElementById("frmSearch");
        form.submit();
    }
}

var phone_number = window.intlTelInput(document.querySelector("#MobileNumber"), {
    separateDialCode: true,
    preferredCountries: ["ae"],
    hiddenInput: "full",
    utilsScript:"/js/utils.js"
    /*"//cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.3/js/utils.js"*/
});
$('#MobileNumber').change(function () {

  
    var full_number = phone_number.getNumber(intlTelInputUtils.numberFormat.E164);
    $("#MobileNumber1").val(full_number);
   // alert(full_number);
  
});




//$("#Newestbutton").click(function () {
//    if ($("#Newestbutton").val() == "Oldest Properties")
//        $("#Newestbutton").val( "Newest Properties");
//    else
//        $("#Newestbutton").val( "Oldest Properties");
//});

//$("#SortPricebutton").click(function () {
//    if ($("#SortPricebutton").val() == "Price: High to Low")
//        $(this).prop('value', 'Price: Low to High').trigger('change');
//    else
//        $(this).prop('value', 'Price: High to Low').trigger('change');
//});