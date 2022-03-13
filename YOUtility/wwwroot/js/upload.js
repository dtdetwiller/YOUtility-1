
/*
$("#upload").click(Upload);

function Upload() {
    var fileupload = $("#files").get(0);
    var files = fileupload.files;
    //var data = new FormData();
    var data = {
        filepath: files[0]
    }
    //data.append(files[0].name, files[0])
    $.ajax({
        type: "post",
        url: "/Home/upload",
        contentType: false,
        processData: false,
        data: data,
        success: function (message) {
            alert(message);
        },
        error: function () {
            alert("Make sure file is a PDF!");
        }
    });
}
*/

    async function AJAXSubmit(oFormElement) {
                    var resultElement = oFormElement.elements.namedItem("result");
    const formData = new FormData(oFormElement);

    try {
                        const response = await fetch(oFormElement.action, {
        method: 'POST',
    body: formData
                        });

    if (response.ok) {
        window.location.href = '/';
                        }

    resultElement.value = 'Result: ' + response.status + ' ' +
    response.statusText;
                    } catch (error) {
        console.error('Error:', error);
                    }
                }
