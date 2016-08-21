
angular.module("app").directive("promedMask", PromedMask);


function PromedMask() {
    return {
        link: function (scope, element, attrs) {
            
            $(element).keypress(function () {
                formataCampo();
            });



            //$(element).down(function () {

            //    var reDigits = /^\d+$/;
            //    var valor = element[0].value;
            //    var novoValor = '';
            //    for (var i = 0; i < valor.length; i++) {
            //        if (!reDigits.test(valor[i])) {
            //            valor[i] = '';
            //        } else {
            //            novoValor = novoValor + valor[i];
            //        }
            //    }

            //    $(element).val(novoValor);
            //    formataCampo();
            //});


            function formataCampo() {
                var reDigits = /^\d+$/;
                var valor = element[0].value;
                var novoValor = '';
                for (var i = 0; i < valor.length; i++) {
                    if (!reDigits.test(valor[i])) {
                        valor[i] = '';
                    } else {
                        novoValor = novoValor + valor[i];
                    }
                }

                if (novoValor.length > 2) {
                    var val1 = novoValor.substr(0, novoValor.length - 1);
                    var val2 = novoValor.substr(novoValor.length - 1);
                    $(element).val(val1 + '.' + val2);
                } else {
                    $(element).val(novoValor);
                }
            }
        }
    };
}