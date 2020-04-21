// Ana Carolina Alves

function onSubmit() {
    var p = document.getElementById("result");
    var form = document.getElementsByClassName("form")[0];
    var num = parseInt(form.elements[0].value);
    if (Number.isNaN(num)) {
        p.innerHTML = "Valor inválido.";
    }
    else {
        p.innerHTML = "Resultado:<br>" + num.toString() + "! = " + fac(num).toString();
    }    
}

function fac(num) {
    if (num === 0 || num === 1)
      return 1;
    for (i = num - 1; i >= 1; i--) {
      num *= i;
    }
    return num;
  }