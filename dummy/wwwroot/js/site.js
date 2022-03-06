function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}
function number_format(number, decimals, dec_point, thousands_sep) {
    number = (number + '').replace(/[^0-9+\-Ee.]/g, '');
    var n = !isFinite(+number) ? 0 : +number,
        prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
        sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
        dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
        s = '',
        toFixedFix = function (n, prec) {
            var k = Math.pow(10, prec);
            return '' + (Math.round(n * k) / k)
                .toFixed(prec);
        };
    s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
    if (s[0].length > 3) {
        s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
    }
    if ((s[1] || '').length < prec) {
        s[1] = s[1] || '';
        s[1] += new Array(prec - s[1].length + 1).join('0');
    }
    return s.join(dec);
}
function onSuccessUpload(e) {
    var colname = e.sender.name;
    colname = colname.substr(0, colname.length - 5);
    getListFileUpload(colname);
}
function OnselectUpload(e,maxFile) {
    var colname = e.sender.name;
    colname = colname.substr(0, colname.length - 5);
    getListFileUpload(colname);
    var fileVal = $("#" + colname + "").val();
    if (fileVal != "") {
        var listFile = fileVal.split(",");
        if (maxFile > 0 && listFile.length >= maxFile) {
            alert("jumlah file maksimum : " + maxFile);
            e.preventDefault();
        } else {
            jQuery.each(e.files, function (i, val) {
                if (listFile.includes(val.name)) {
                    alert("File Name: " + val.name + " Already Exists");
                    e.preventDefault();
                }
            });
        }
    }
}
function OnselectUpload2(e, maxFiles, allowExt, max_filesize, max_filesize_text) {
    var colname = e.sender.name;
    colname = colname.substr(0, colname.length - 5);
    getListFileUpload(colname);
    var fileVal = $("#" + colname + "").val();
    var oldFileName = fileVal.split(',');
    maxFile = maxFiles != '' ? parseInt(maxFiles) : 1;
    if (maxFile > 0 && oldFileName.length >= maxFile) {
        var msg = "jumlah file maksimum : " + maxFile;
        notification.show({ title: "Pesan Kesalahan", message: kendo.toString(msg) }, "error"); e.preventDefault();
        e.preventDefault();
    } else {
        $.each(e.files, function (index, value) {
            var validExt = 1; if (allowExt != '') {
                var allowExts = allowExt.split(',');
                if (allowExts.length > 0) {
                    validExt = 0;
                    for (i = 0; i < allowExts.length; i++) {
                        if (allowExts[i].trim().toLowerCase() === value.extension.toLowerCase()) {
                            validExt = 1; break;
                        }
                    }
                }
            }
            var max_file_size = max_filesize != '' ? parseInt(max_filesize) : 2097152;
            var isvalid = 1;
            if (!validExt) {
                isvalid = 0;
                var msg = 'File extension yang diperbolehkan: ' + allowExt;
                notification.show({ title: "Pesan Kesalahan", message: kendo.toString(msg) }, "error"); e.preventDefault();
            } if (isvalid === 1 && value.size > max_file_size) {
                isvalid = 0;
                var msg = 'Ukuran file yang diperbolehkan maksimal:' + max_filesize_text;
                notification.show({ title: "Pesan Kesalahan", message: kendo.toString(msg) }, "error");
                e.preventDefault();
            } if (isvalid === 1) {
                for (i = 0; i < oldFileName.length; i++) {
                    if (oldFileName[i] === value.name) {
                        isvalid = 0; var msg = 'Nama file sudah ada';
                        notification.show({ title: "Pesan Kesalahan", message: kendo.toString(msg) }, "error");
                        e.preventDefault();
                        break;
                    }
                }
            }
        });
    }
}
function getListFileUpload(colname) {
    var upload = $("#" + colname + "_file").data("kendoUpload"),
        files = upload.getFiles();
    var item_file = "";
    jQuery.each(files, function (i, val) { item_file += item_file != "" ? "," : ""; item_file += val.name; });
    $("#" + colname + "").val(item_file);
}