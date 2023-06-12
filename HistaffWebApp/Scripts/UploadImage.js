function getFile() {
    if (validateFileUpload()) {
        document.getElementById('btnOk').click();
    }
    else {
        displayMessage()
    }

}
function displayMessage() {
    $('#windowCover').stop(false, true).fadeIn(300);
}
function closeMessage() {
    $('#windowCover').stop(false, true).fadeOut(300);
}
function validateFileUpload() {
    var fuObject = document.getElementById('fuImage');
    var fuData = fuObject.value;
    if (fuData == '') {
        document.getElementById('lblStatus').innerHTML = "Bạn chưa chọn file. Click chuột lên màn hình để tắt thông báo này"
        
        return false;
    }
    else {
        var temp = fuData.substring(fuData.lastIndexOf('.') + 1).toLowerCase();
        if (temp == "jpg" || temp == "png") {
            return true;
        }
        else {
            document.getElementById('lblStatus').innerHTML = "File có định dạng không hợp lệ. Chỉ chấp nhận file với định dạng JPEG hoặc PNG, xin vui lòng kiểm tra lại. Click chuột lên màn hình để tắt thông báo này"
            return false;
        }
    }
}

$(document).ready(
	function () {
	    var b = $('#hiddenUpload');
	    $('#logo1').bind('mouseenter mouseleave', function (e) { var check = (e.type === 'mouseenter') ? (b.stop(false, true).fadeIn(300)) : (b.stop(false, true).fadeOut(300)); });
	    $('#hiddenUpload').click(function () { $('#fuImage').click(); })
	    $('#windowCover').click(function () { closeMessage(); })	    
});