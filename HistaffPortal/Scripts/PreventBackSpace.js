if (typeof window.event == 'undefined') {
    document.onkeypress = function (e) {
        var test_var = e.target.nodeName.toUpperCase();
        if (e.target.type) var test_type = e.target.type.toUpperCase();
        if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA' || (test_var == 'INPUT' && test_type == 'PASSWORD')) {
            return e.keyCode;
        } else if (e.keyCode == 8) {
            e.preventDefault();
        }
    }
} else {
    document.onkeydown = function () {
        var test_var = event.srcElement.tagName.toUpperCase();
        if (event.srcElement.type) var test_type = event.srcElement.type.toUpperCase();
        if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA' || (test_var == 'INPUT' && test_type == 'PASSWORD')) {
            return event.keyCode;
        } else if (event.keyCode == 8) {
            event.returnValue = false;
        }
    }
}

function FilterMenuShowing(sender, eventArgs) {
    var menu = eventArgs.get_menu();
    var items = menu._itemData;
    var i = 0;
    while (i < items.length) {
        var arrMenuOptions = ',IsNull,NotIsNull,NotEqualTo,';
        if (arrMenuOptions.indexOf(',' + items[i].value + ',') != -1) {
            var item = menu._findItemByValue(items[i].value);
            if (item) {
                item._element.style.display = "none";
            }
        }
        i++;
    }
}