

/*Changes the units inside the units used section based on the selected option of utility*/
document.getElementById('select-utility').addEventListener('change', function () {
    var utility = document.getElementById('select-utility');
    var value = utility.options[utility.selectedIndex].value;

    var unit;

    if (value == "water")
        unit = "gallons";
    else if (value == "gas")
        unit = "therms"
    else
        unit = "kWh"

    document.getElementById('unit').innerHTML = unit + " Used (This Month)";
});

/*
 * Upload bills page upload logic 
 */

function convert() {
    var fr = new FileReader();
    var pdff = new Pdf2TextClass();

    fr.onload = function () {
        pdff.pdfToText(fr.result, null, (text) => {

            console.log(text);

            // Bill data
            var amount, dates, dateStart, dateEnd, used, type;

            if (text.includes("SALT LAKE CITY PUBLIC UTILITIES")) {

                //Get type
                type = "water";

                //Get price
                amount = text.substring(text.lastIndexOf("Due Date") + 9, text.lastIndexOf("Amount Due"))
                parseFloat(amount);

                //Get dates
                dates = text.substring(text.lastIndexOf("FROM TO") + 7, text.lastIndexOf("Dates Read"));
                dateStart = dates.substring(0, dates.indexOf(" "));
                dateEnd = dates.substring(dates.lastIndexOf(dateStart) + dateStart.length + 1, dates.lastIndexOf(" "));

                dateStart = new Date(dateStart);
                dateEnd = new Date(dateEnd);

                //Get Units used
                used = text.substring(text.lastIndexOf("gallons)"), text.lastIndexOf("(100 CUBIC FEET"));
                used = (used.substring(used.indexOf(")") + 1, used.lastIndexOf(" ")));
                parseFloat(used);
            }
            else if (text.includes("ROCKY MTN POWER")) {
                //Get type
                type = "electricity";

                //Get price
                amount = text.substring(text.indexOf("AMOUNT DUE:") + 11, text.indexOf("Write"))
                parseFloat(amount);

                //Get dates
                dateStart = text.substring(text.indexOf("BILLING DATE:") + 14, text.indexOf("ACCOUNT NUMBER"));
                dateEnd = text.substring(text.indexOf("DUE DATE:") + 9, text.indexOf("AMOUNT DUE"));

                dateStart = new Date(dateStart);
                dateEnd = new Date(dateEnd);

                //Get Units used
                used = text.substring(text.indexOf("METERMULTIPLIERAMOUNT"), text.indexOf("kwh") - 1);
                used = used.substring(used.lastIndexOf(" ") + 1);
                parseFloat(used);
            }
            else if (text.includes("Dominion Energy")) {
                //Get type
                type = "gas";

                //Get price
                amount = text.substring(text.indexOf("Current Charges") + 30, text.indexOf("***"))
                parseFloat(amount);


                //Get dates
                dateStart = text.substring(text.indexOf("Service from") + 13, text.indexOf("Rate"));
                dateStart = dateStart.substring(0, dateStart.indexOf(" "));

                dateEnd = text.substring(text.indexOf(dateStart), text.indexOf("Rate"));
                dateEnd = dateEnd.substring(dateEnd.indexOf("-") + 2, dateEnd.length);
                dateStart = new Date(dateStart);
                dateEnd = new Date(dateEnd);

                //Get Units used
                used = text.substring(text.indexOf("DTH"), text.indexOf("Tax Reform"));
                var multiplier = used.substring(used.indexOf("(") + 1, used.indexOf("))"));

                used = used.substring(used.indexOf("))") + 3, used.length);

                used = used / multiplier;

                parseFloat(used);

                used = used.toFixed(2);
            }
            else {
                alert("Utility Company not supported, please use manual upload");
                return;
            }

            var unit;

            if (type == "water")
                unit = "gallons";
            else if (type == "gas")
                unit = "therms"
            else
                unit = "kWh"

            document.getElementById('used').innerHTML = "Used: " + used + " " + unit;
            document.getElementById('date').innerHTML = "Dates: " + dateStart.toLocaleDateString('pt-PT') + " - " + dateEnd.toLocaleDateString('pt-PT');
            document.getElementById('utility-type').innerHTML = "Type: " + type;
            document.getElementById('payment').innerHTML = "Amount: $" + amount;
            document.getElementById('results').style.display = "block";

            //alert("Bill has been created!\nDates: " + dateStart.toLocaleDateString('pt-PT') + " - " + dateEnd.toLocaleDateString('pt-PT') + "\nUsed: " + used + " " + unit + "\nAmount: $" + amount);

            // Data to send to the controller
            var data = {
                dateStart: dateStart.toISOString(),
                dateEnd: dateEnd.toISOString(),
                amount: amount,
                used: used,
                type: type
            }

            // Ajax request to sending the data to the bills controller.
            $.post("/Bills/GetScannedPDFData", data)
                .done(function (result) {

                })

        });
    }

    fr.readAsDataURL(document.getElementById('pdffile').files[0])


}

function Pdf2TextClass() {
    var self = this;
    this.complete = 0;

    this.pdfToText = function (data, callbackPageDone, callbackAllDone) {
        console.assert(data instanceof ArrayBuffer || typeof data == 'string');
        var loadingTask = pdfjsLib.getDocument(data);
        loadingTask.promise.then(function (pdf) {


            var total = pdf._pdfInfo.numPages;
            //callbackPageDone( 0, total );
            var layers = {};
            for (i = 1; i <= total; i++) {
                pdf.getPage(i).then(function (page) {
                    var n = page.pageNumber;
                    page.getTextContent().then(function (textContent) {

                        //console.log(textContent.items[0]);0
                        if (null != textContent.items) {
                            var page_text = "";
                            var last_block = null;
                            for (var k = 0; k < textContent.items.length; k++) {
                                var block = textContent.items[k];
                                if (last_block != null && last_block.str[last_block.str.length - 1] != ' ') {
                                    if (block.x < last_block.x)
                                        page_text += "\r\n";
                                    else if (last_block.y != block.y && (last_block.str.match(/^(\s?[a-zA-Z])$|^(.+\s[a-zA-Z])$/) == null))
                                        page_text += ' ';
                                }
                                page_text += block.str;
                                last_block = block;
                            }

                            textContent != null && console.log("page " + n + " finished."); //" content: \n" + page_text);
                            layers[n] = page_text + "\n\n";
                        }
                        ++self.complete;
                        //callbackPageDone( self.complete, total );
                        if (self.complete == total) {
                            window.setTimeout(function () {
                                var full_text = "";
                                var num_pages = Object.keys(layers).length;
                                for (var j = 1; j <= num_pages; j++)
                                    full_text += layers[j];
                                callbackAllDone(full_text);
                            }, 1000);
                        }
                    }); // end  of page.getTextContent().then
                }); // end of page.then
            } // of for
        });
    }; // end of pdfToText()
}; // end of class