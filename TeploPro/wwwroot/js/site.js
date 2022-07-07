$(document).ready(function () {
    /* Прокрутка до исходных данных */
    $("#runToCalculation").click(function () {
        $([document.documentElement, document.body]).animate({
            scrollTop: $("#calculation").offset().top - 60
        }, 500);
    });

    document.getElementById("header").classList.add("scroll");

    // Валидация
    if (document.querySelector(".form__input")) {
        let inputs = document.querySelectorAll(".form__input");
        inputs.forEach(element => replaceToCorrectValue(element));
    }
    if (document.querySelector(".form-reference__input")) {
        let inputs = document.querySelectorAll(".form-reference__input");
        inputs.forEach(element => replaceToCorrectValue(element));
    }

    function replaceToCorrectValue(input) {
        let inputButton = document.getElementById("submitButton");
        input.oninput = function () {
            var x = this.value.length - 1;
            if (this.value[x] == "," || this.value[x] == "-") {
                inputButton.disabled = true;
                input.parentElement.classList.add("same-error");
                inputButton.classList.add("lock");
            } else {
                inputButton.disabled = false;
                input.parentElement.classList.remove("same-error");
                inputButton.classList.remove("lock");
            }
            console.log(input);
            if (this.value == "") {
                inputButton.disabled = true;
                inputButton.classList.add("lock");
                input.parentElement.classList.add("error");
            } else if (this.value[x] != "," && this.value[x] != "-") {
                inputButton.disabled = false;
                inputButton.classList.remove("lock");
                input.parentElement.classList.remove("error");
            }
            // Проверка на корректность остальных ячеек
            if ($("td").hasClass("error") || $("td").hasClass("same-error")) {
                inputButton.disabled = true;
                $(".form__submit").addClass("lock")
            }
        };
    }

    $(".result-main__data-value").click(function () {
        navigator.clipboard.writeText($(this).text());
    });

    $("#showFullResult").change(function () {
        if (this.checked) {
            //$(".result-main__table").removeClass("hide");
            $(".result-main__table").show();
        } else {
            $(".result-main__table").hide();
        }
    });

    //if (document.getElementById("theoreticalTemperature")) {
    //    let theorTemperature = document.getElementById("theoreticalTemperature");
    //    let theorTemperatureValue = parseFloat(document.getElementById("theoreticalTemperature").innerText);
    //    if (theorTemperatureValue >= 1950 && theorTemperatureValue <= 2100) {
    //        theorTemperature.classList.remove("incorrect");
    //        theorTemperature.classList.add("correct");
    //    } else {
    //        theorTemperature.classList.remove("correct");
    //        theorTemperature.classList.add("incorrect");
    //    }
    //}

    $("input, select, textarea").attr("autocomplete", "off");
    $(".form__input").numeric({ decimal: ",", negative: false, scale: 3 });

    $(".form-reference__input").numeric({ decimal: ",", scale: 3 });

    if (document.querySelector(".project")) {
        $(".project__select-list").change(function () {
            let inputId = $(this).children("option:selected").val();
            if (inputId != 0) {
                $.ajax({
                    type: "POST",
                    url: "/Home/GetInputData",
                    data: { inputId: inputId },
                    beforeSend: function () {
                        $("#loader").show();
                    },
                    complete: function () {
                        $("#loader").hide();
                        $(".form-project").show();
                    },
                    success: function (response) {
                        console.log(response);
                        //let test = response.oxygenContentInBlast.toString().replace(/\./g, ',')
                        //console.log(test);

                        $("#inputDataId").val(response.id);
                        $(".base-blast-temperature input").val(response.blastTemperature.toString().replace(/\./g, ','));
                        $(".base-blast-humidity input").val(response.blastHumidity.toString().replace(/\./g, ','));
                        $(".base-oxyden-blast-content input").val(response.oxygenContentInBlast.toString().replace(/\./g, ','));
                        $(".base-natural-gas-consumption input").val(response.naturalGasConsumption.toString().replace(/\./g, ','));
                        $(".base-colosh-gas-pressure input").val(response.coloshGasPressure.toString().replace(/\./g, ','));
                        $(".base-chugun-si input").val(response.chugun_SI.toString().replace(/\./g, ','));
                        $(".base-chugun-mn input").val(response.chugun_MN.toString().replace(/\./g, ','));
                        $(".base-chugun-p input").val(response.chugun_P.toString().replace(/\./g, ','));
                        $(".base-chugun-s input").val(response.chugun_S.toString().replace(/\./g, ','));
                        $(".base-ash-content-coke input").val(response.ashContentInCoke.toString().replace(/\./g, ','));
                        $(".base-sulfur-content-coke input").val(response.sulfurContentInCoke.toString().replace(/\./g, ','));
                    },
                    failure: function (response) {
                        console.log(response.responseText);
                    },
                    error: function (response) {
                        console.log(response.responseText);
                    }
                });
            }
        });
    }

    // POPUP
    //$(".popup__close").click(function () {
    //    $(".popup").removeClass("show");
    //});

    //$(".popup").click(function () {
    //    $(this).removeClass("show");
    //});

    $(".popup").on("click", function (event) {
        if ($(event.target).is(".popup__close") || $(event.target).is(".popup")) {
            event.preventDefault();
            $(this).removeClass("show");
            //$('body').removeClass('lock');
        }
    });

    if (document.querySelector(".inputs-main")) {
        $(".inputs-main__item-delete").click(function () {
            let inputId = $(this).data("id");
            $(".popup").data("id", inputId);
            $(".popup").addClass("show");
            $(".input-id").text(inputId);
        });

        $(".popup__input-delete").click(function () {
            let inputId = $(this).parents().find(".popup").data("id");
            $.ajax({
                type: "POST",
                url: "/Home/DeleteInputVariant",
                data: { inputId: inputId },
                beforeSend: function () {
                    $("#loader").show();
                },
                complete: function () {
                    $("#loader").hide();
                },
                success: function (response) {
                    let jsonResponse = $.parseJSON(response);
                    console.log(jsonResponse);

                    /*$(this).parent().remove();*/
                    $(".inputs-main__item").each(function () {
                        let deleteButton = $(this).find(".inputs-main__item-delete");
                        if (deleteButton.data("id") == inputId) {
                            $(this).remove();
                        }
                    });

                    $(".popup").removeClass("show");
                },
                failure: function (response) {
                    console.log(response.responseText);
                },
                error: function (response) {
                    console.log(response.responseText);
                }
            });
        });
    }

    /* Кнопка экспорта */
    let buttonExport = document.querySelector(".result-table-export");
    if (!document.querySelector(".result-main") && !document.querySelector(".comparison-main .result-table")) {
        buttonExport.parentElement.remove();
    }
    let link = document.querySelector(".index-main__inputs");
    if (document.querySelector(".inputs-main-content")) {
        link.remove();
    }

    function checkTemperature() {
        let inputsTemperature = document.querySelectorAll(".temperature-calculate-data");
        let isTemperature = document.getElementById("checkbox-temperature");
        let inputs = document.querySelectorAll(".calculate-data");

        if (isTemperature.checked) {
            inputs.forEach(element => element.classList.add("hidden"));
        } else {
            inputs.forEach(element => element.classList.remove("hidden"));
        }
    }

    /* Реализация экспорта таблицы с результатами в Excel */
    var tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; })
            }
            , downloadURI = function (uri, name) {
                var link = document.createElement("a");
                link.download = name;
                link.href = uri;
                link.click();
            }

        return function (table, name, fileName) {
            if (!table.nodeType) table = document.getElementById(table)
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
            var resuri = uri + base64(format(template, ctx))
            downloadURI(resuri, fileName);
        }
    })();

    onclick = "tableToExcel('resultTable', 'Расчет индексов', 'Расчет_индексов_верха_и_низа_доменной_плавки.xls')"

    buttonExport.onclick = function () {
        if (document.title == "Result - TeploPro") {
            tableToExcel('resultTable', 'Расчет индексов доменной плавки', 'Расчет_индексов_верха_и_низа_доменной_плавки.xls')
        } else if (document.title == "ResultTemperature - TeploPro") {
            tableToExcel('resultTable', 'Расчет теоретической температуры горения', 'Расчет_теор_температуры_горения_углерода_кокса.xls')
        } else if (document.title == "Comparison - TeploPro") {
            tableToExcel('resultTable', 'Сопоставление индексов', 'Сопоставление_индексов_верха_и_низа_доменной_плавки.xls')
        } else if (document.title == "ComparisonTemperature - TeploPro") {
            tableToExcel('resultTable', 'Сопоставление теор. температуры горения', 'Сопоставление_теор_температуры_горения_углерода_кокса.xls')
        }
    };

    // if document query selector
    let projectBlastTemperatureMaxValue = 0;

    $(".project-blast-temperature input").focus(function () {
        let baseBlastTemperature = $(".base-blast-temperature input").val();
        let projectBlastTemperatureMax = $(".project-blast-temperature-max .value");

        if (baseBlastTemperature >= 800 && baseBlastTemperature <= 900) {
            projectBlastTemperatureMaxValue = 900;
            projectBlastTemperatureMax.text(projectBlastTemperatureMaxValue);
        } else if (baseBlastTemperature >= 901 && baseBlastTemperature <= 1000) {
            projectBlastTemperatureMaxValue = 1000;
            projectBlastTemperatureMax.text(projectBlastTemperatureMaxValue);
        } else if (baseBlastTemperature >= 1001 && baseBlastTemperature <= 1100) {
            projectBlastTemperatureMaxValue = 1100;
            projectBlastTemperatureMax.text(projectBlastTemperatureMaxValue);
        } else if (baseBlastTemperature >= 1101 && baseBlastTemperature <= 1200) {
            projectBlastTemperatureMaxValue = 1200;
            projectBlastTemperatureMax.text(projectBlastTemperatureMaxValue);
        }

        $(this).parent().css("background-color", "#5bd2ff");
        $(".project-blast-temperature-max").show();
    });

    $(".project-blast-temperature input").focusout(function () {
        $(this).parent().css("background-color", "#fff");
        $(".project-blast-temperature-max").hide();
    });

    $(".project-blast-temperature input").keyup(function () {
        let projectBlastTemperature = $(this);
        if (projectBlastTemperature.val() > projectBlastTemperatureMaxValue) {
            projectBlastTemperature.val(projectBlastTemperatureMaxValue);
        }
    });
});
