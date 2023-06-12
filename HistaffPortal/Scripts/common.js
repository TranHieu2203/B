function openWindow(url, width, height) {
    /// <summary>Hiện cửa sổ mới lên (không phải là modern popup)</summary>
    /// <param name="url">URL của trang cần mở</param>
    /// <param name="width">Độ rộng của cửa sổ mới</param>
    /// <param name="height">Độ cao của cửa sổ mới</param>
    if (!width) {
        width = screen.width - 20;
        height = screen.height - 60;
    }
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    var ngay = today.getDay();
    var thang = today.getMonth();
    var nam = today.getYear();

    var addDay = '' + nam + thang + ngay + h + m + s;

    url += (url.indexOf('?') > 0) ? ("&x=" + addDay) : ("?x=" + addDay);

    return window.open(url, '_blank', 'width=' + width + ',height=' + height + ',location=0,status=0,scroll=1');
}

function openWindowNoScroll(url, width, height) {
    /// <summary>Hiện cửa sổ mới lên (không phải là modern popup)</summary>
    /// <param name="url">URL của trang cần mở</param>
    /// <param name="width">Độ rộng của cửa sổ mới</param>
    /// <param name="height">Độ cao của cửa sổ mới</param>
    if (!width) {
        width = screen.width - 20;
        height = screen.height - 60;
    }
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    var ngay = today.getDay();
    var thang = today.getMonth();
    var nam = today.getYear();

    var addDay = '' + nam + thang + ngay + h + m + s;

    url += (url.indexOf('?') > 0) ? ("&x=" + addDay) : ("?x=" + addDay);

    return window.open(url, '_blank', 'width=' + width + ',height=' + height + ',location=0,status=0,scroll=0');
}

function openModalWindow(url, width, height) {
    /// <summary>Hiện cửa sổ mới lên (modern popup)</summary>
    /// <param name="url">URL của trang cần mở</param>
    /// <param name="width">Độ rộng của cửa sổ mới</param>
    /// <param name="height">Độ cao của cửa sổ mới</param>
    if (!width) {
        width = screen.width - 20;
        height = screen.height - 60;
    }
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    var ngay = today.getDay();
    var thang = today.getMonth();
    var nam = today.getYear();

    var addDay = '' + nam + thang + ngay + h + m + s;

    url += (url.indexOf('?') > 0) ? ("&x=" + addDay) : ("?x=" + addDay);
    return window.showModalDialog(url, 'Dialog Box Arguments', 'dialogHeight: ' + (height + 5) + 'px; dialogWidth: ' + (width + 5) + 'px;smartNavigation=Yes; center: Yes; resizable: Yes; status: No; Scroll: Yes');
}

function openModalWindowNoScroll(url, width, height) {
    /// <summary>Hiện cửa sổ mới lên (modern popup)</summary>
    /// <param name="url">URL của trang cần mở</param>
    /// <param name="width">Độ rộng của cửa sổ mới</param>
    /// <param name="height">Độ cao của cửa sổ mới</param>
    if (!width) {
        width = screen.width - 20;
        height = screen.height - 60;
    }
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    var ngay = today.getDay();
    var thang = today.getMonth();
    var nam = today.getYear();

    var addDay = '' + nam + thang + ngay + h + m + s;

    url += (url.indexOf('?') > 0) ? ("&x=" + addDay) : ("?x=" + addDay);
    return window.showModalDialog(url, 'Dialog Box Arguments', 'dialogHeight: ' + (height + 5) + 'px; dialogWidth: ' + (width + 5) + 'px;smartNavigation=Yes; center: Yes; resizable: Yes; status: No; Scroll: No');
}

function returnToParent(value) {

    window.returnValue = value;

    window.close();
}
function validateHTMLText(val) {
    const regex = /<(\"[^\"]*\"|'[^']*'|[^'\">])*>/g;
    var result = false;
    if (regex.test(val)) {
        result = true;
    }   
    return result;
}

function validateSQLText(val) {
    const regex = /(SELECT\s[\w\*\)\(\,\s]+\sFROM\s[\w]+)|(UPDATE\s[\w]+\sSET\s[\w\,\'\=]+)|(INSERT\sINTO\s[\d\w]+[\s\w\d\)\(\,]*\sVALUES\s\([\d\w\'\,\)]+)|(DELETE\sFROM\s[\d\w\'\=]+)/g;
    var result = false;
    if (regex.test(val.toUpperCase())) {
        result = true;
    }   
    return result;
}

function daysInMonth(iMonth, iYear) {
    var m = new Array("31", "28", "31", "30", "31", "30", "31", "31", "30", "31", "30", "31");
    if (iMonth != 2) return m[iMonth - 1];
    //Neu la thang 2, kiem tra neu khong phai la nam Nhuan
    if ((iYear % 4 != 0) || (!((iYear % 4 == 0) && ((iYear % 100 != 0) || iYear % 400 == 0)))) return m[1];
    //Trai lai la Nam Nhuan
    return "29";
}
function isDateInput(strTest) {
    if (strTest.trim().length <= 0) return true;

    var sTest = strTest.trim().split('/');

    if (sTest.length > 3) return false;

    var iMonth;
    var iYear;

    if (sTest.length == 3) {

        iMonth = sTest[1];

        if (isNaN(iMonth)) return false;

        if ((iMonth < 1) || (iMonth > 12)) return false;

        iYear = sTest[2];
        if (isNaN(iYear)) return false;

        if ((iYear < 1900) || (iYear > 9999)) return false;

        var iDay
        iDay = sTest[0].trim();
        if (iDay == '') return false;
        if (isNaN(iDay)) return false;
        var iMaxDay = daysInMonth(iMonth, iYear);

        if ((parseInt(doChuanHoaInt(iDay)) < 1) || (parseInt(doChuanHoaInt(iDay)) > parseInt(iMaxDay))) {
            return false;
        }
    }

    else if (sTest.length == 2)// thang/ nam
    {
        iMonth = sTest[0].trim();
        if (iMonth == '') return false;
        if (isNaN(iMonth)) return false;
        if ((parseInt(doChuanHoaInt(iMonth)) < 1) || (parseInt(doChuanHoaInt(iMonth)) > 12)) return false;

        iYear = sTest[1];
        if (iYear == '') return false;
        if (isNaN(iYear)) return false;
        if ((parseInt(iYear) < 1900) || (parseInt(iYear) > 9999)) return false;

    }
    else//Len=1
    {
        iYear = strTest;
        if (isNaN(iYear)) return false;
        if ((parseInt(iYear) < 1900) || (parseInt(iYear) > 9999)) return false;
    }
    return true;
}

function isDateFullInput(strTest) {
    if (strTest.trim().length == 0) return true;

    var sTest = strTest.trim().split('/');

    if (sTest.length != 3) return false;
    var iMonth;
    var iYear;

    if (sTest.length == 3) {

        iMonth = sTest[1];

        if (isNaN(iMonth)) return false;

        if ((iMonth < 1) || (iMonth > 12)) return false;

        iYear = sTest[2];
        if (isNaN(iYear)) return false;

        if ((iYear < 1000) || (iYear > 9999)) return false;

        var iDay
        iDay = sTest[0].trim();
        if (isNaN(iDay)) return false;
        var iMaxDay = daysInMonth(iMonth, iYear);

        if ((parseInt(doChuanHoaInt(iDay)) < 1) || (parseInt(doChuanHoaInt(iDay)) > parseInt(iMaxDay))) {
            return false;
        }
    }
    return true;
}
function checkDate(sender, eventArgs) {

    if (!isDateInput(eventArgs.Value))
        eventArgs.IsValid = false;
    else
        eventArgs.IsValid = true;
}
function checkDateFull(sender, eventArgs) {

    if (!isDateFullInput(eventArgs.Value))
        eventArgs.IsValid = false;
    else
        eventArgs.IsValid = true;
}
function isNumber(val) {
    if (isNaN(val)) {
        return false;
    }
    else {
        return true;
    }
}
function doChuanHoaInt(arg) {
    temp = arg;
    while (temp.substring(0, 1) == '0') {
        temp = temp.substring(1);
    }

    return temp;
}

var BrowserDetect = {
    init: function () {
        this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
        this.version = this.searchVersion(navigator.userAgent)
			|| this.searchVersion(navigator.appVersion)
			|| "an unknown version";
        this.OS = this.searchString(this.dataOS) || "an unknown OS";
    },
    searchString: function (data) {
        for (var i = 0; i < data.length; i++) {
            var dataString = data[i].string;
            var dataProp = data[i].prop;
            this.versionSearchString = data[i].versionSearch || data[i].identity;
            if (dataString) {
                if (dataString.indexOf(data[i].subString) != -1)
                    return data[i].identity;
            }
            else if (dataProp)
                return data[i].identity;
        }
    },
    searchVersion: function (dataString) {
        var index = dataString.indexOf(this.versionSearchString);
        if (index == -1) return;
        return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
    },
    dataBrowser: [
		{
		    string: navigator.userAgent,
		    subString: "Chrome",
		    identity: "Chrome"
		},
		{ string: navigator.userAgent,
		    subString: "OmniWeb",
		    versionSearch: "OmniWeb/",
		    identity: "OmniWeb"
		},
		{
		    string: navigator.vendor,
		    subString: "Apple",
		    identity: "Safari",
		    versionSearch: "Version"
		},
		{
		    prop: window.opera,
		    identity: "Opera",
		    versionSearch: "Version"
		},
		{
		    string: navigator.vendor,
		    subString: "iCab",
		    identity: "iCab"
		},
		{
		    string: navigator.vendor,
		    subString: "KDE",
		    identity: "Konqueror"
		},
		{
		    string: navigator.userAgent,
		    subString: "Firefox",
		    identity: "Firefox"
		},
		{
		    string: navigator.vendor,
		    subString: "Camino",
		    identity: "Camino"
		},
		{		// for newer Netscapes (6+)
		    string: navigator.userAgent,
		    subString: "Netscape",
		    identity: "Netscape"
		},
		{
		    string: navigator.userAgent,
		    subString: "MSIE",
		    identity: "Explorer",
		    versionSearch: "MSIE"
		},
		{
		    string: navigator.userAgent,
		    subString: "Gecko",
		    identity: "Mozilla",
		    versionSearch: "rv"
		},
		{ 		// for older Netscapes (4-)
		    string: navigator.userAgent,
		    subString: "Mozilla",
		    identity: "Netscape",
		    versionSearch: "Mozilla"
		}
	],
    dataOS: [
		{
		    string: navigator.platform,
		    subString: "Win",
		    identity: "Windows"
		},
		{
		    string: navigator.platform,
		    subString: "Mac",
		    identity: "Mac"
		},
		{
		    string: navigator.userAgent,
		    subString: "iPhone",
		    identity: "iPhone/iPod"
		},
		{
		    string: navigator.platform,
		    subString: "Linux",
		    identity: "Linux"
		}
	]

};
BrowserDetect.init();