// Ana Carolina Alves

function onSubmit() {
    var form = document.getElementsByClassName("form")[0];
    var total = 0;
    for (i = 0; i < form.length - 1; i++) {
        var num = parseInt(form.elements[i].value);
        if (!Number.isNaN(num)) {
            total += num;
        }
    }
    var result = "A soma (" + total.toString() + ") é ";
    if (total % 2 == 0) {
        result += "par.";
    }
    else {
        result += "ímpar."
    }
    alert(result);
}