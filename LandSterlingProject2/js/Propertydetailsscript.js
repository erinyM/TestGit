function PropertyPrice(value){
    document.getElementById("property-price-span").innerHTML = "ADE " + value;

    CalculatePaymentAndInvestMent()
}
function LoanDuration(value){
    document.getElementById("Loan-Duration-span").innerHTML = value + " years";
    CalculatePaymentAndInvestMent()
}
function DownPayment(value){
    document.getElementById("Down-Payment-span").innerHTML = "ADE " + value;
    CalculatePaymentAndInvestMent();
}
function Interest(value){
    document.getElementById("Interest-span").innerHTML = value + " %"
    CalculatePaymentAndInvestMent();
}

function CalculatePaymentAndInvestMent() {

    var propertyPrice = parseInt( document.getElementById("property").value)
    console.log(propertyPrice);
    var LoanDuration = parseInt( document.getElementById("Loan-Duration-input").value);
    console.log(LoanDuration)
    var DownPayment = parseInt(document.getElementById("Down-Payment-input").value);
    var Interest = parseInt(document.getElementById("Interest-input").value);
    var TotalInvestValue = (propertyPrice - DownPayment) / (LoanDuration - Interest)
    var MonthlyPayment = (propertyPrice - DownPayment) / (LoanDuration - Interest)
    document.getElementById("total-invest-value").innerHTML = TotalInvestValue.toFixed(2);
    document.getElementById("monthly-payment").innerHTML = MonthlyPayment.toFixed(2);
    document.getElementById("total-invest-value-tosend").value = TotalInvestValue.toFixed(2);
    document.getElementById("monthly-payment-tosend").value = MonthlyPayment.toFixed(2);
    console.log("test")
}
