
//function treeview exten
(function ($)
{
    var CustomTreeView = kendo.ui.TreeView.extend({
        selectedNode: function ()
        {
            return this.dataItem(this.select());
        },
        selectedId: function ()
        {
            var id = '';
            var node = this.dataItem(this.select());
            if (node)
            {
                return node.id;
            }
            return id;
        },
        selectedNodes: function ()
        {
            var treeview = this;
            var items = $("#" + treeview.element[0].id + " .k-item input[type=checkbox]:checked").closest(".k-item");
            var nodes = new Array();
            $(items).each(function (index, value) { nodes.push(treeview.dataItem(value)) });
            return nodes;
        }
    });

    kendo.ui.plugin(CustomTreeView);
})(jQuery);

//function grid exten
(function ($)
{
    var CustomGrid = kendo.ui.Grid.extend({
        selectedItem: function ()
        {
            var grid = this;
            return grid.dataItem(grid.select());
        },
        selectedId: function ()
        {
            var id = '';
            var selectItem = this.dataItem(this.select());
            if (selectItem)
            {
                return selectItem.id;
            }
            return id;
        },
        selectedItems: function ()
        {
            var grid = this;
            var items = new Array();
            $(grid.select()).each(function (index, value) { items.push(grid.dataItem(value)) });
            return items;
        },
        getItems: function ()
        {
            var grid = this;
            return grid.dataSource.view();
        },
        selectedRowById: function (id)
        {
            var grid = this;
            grid.clearSelection();
            var items = grid.getItems();
            items.forEach(function (item, idx)
            {
                if (item.id != undefined && item.id == id)
                {
                    grid.select("tr:eq(" + idx + ")");
                    return false;
                }
            });
            return grid.dataItem(grid.select());
        },
        exportExcel: function ()
        {
            var grid = this;
            grid.bind("excelExport");
            grid.saveAsExcel();
        }
    });

    kendo.ui.plugin(CustomGrid);
})(jQuery);

(function ($)
{
    $.fn.histaffDropdowntree = function (opt)
    {
        return histaffDropdowntree(this, opt);
    }
    $.fn.histaffDatePicker = function (opt)
    {
        return histaffDatePicker(this, opt);
    }
    $.fn.histaffDateTimePicker = function (opt)
    {
        return histaffDateTimePicker(this, opt);
    }
    $.fn.histaffTimePicker = function (opt)
    {
        return histaffTimePicker(this, opt);
    }
    $.fn.histaffGrid = function (opt)
    {
        return histaffGrid(this, opt);
    }
    $.fn.histaffTreeList = function (opt)
    {
        return histaffTreeList(this, opt);
    }
    $.fn.histaffWindow = function (opt)
    {
        return histaffWindow(this, opt);
    }
    $.fn.histaffConfirm = function (opt)
    {
        return histaffConfirm(this, opt);
    }
    $.fn.histaffNumeric = function (opt)
    {
        return histaffNumeric(this, opt);
    }
}(jQuery));

function histaffDropdowntree(element, opt)
{
    var item = element.data('kendoDropDownTree');
    if (item)
    {
        if (typeof opt !== 'undefined' && typeof opt.destroy !== 'undefined'  && opt.destroy == true)
        {
            item.destroy();
            element.empty();
        }
        else return item;
    }
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.dataTextField === 'undefined') opt.dataTextField = "text";
    if (typeof opt.dataValueField === 'undefined') opt.dataValueField = "id";
    if (typeof opt.autoClose === 'undefined') opt.autoClose = true;

    //event 
    var change = opt.change;
    opt.change = function ()
    {
        var dropDownTree = this;
        if (typeof change !== 'undefined')
        {
            try
            {
                change(dropDownTree);
            } catch (err)
            {
                console.log(err);
            }
        }
    };
    var select = opt.select;
    opt.select = function (e)
    {
        var dropDownTree = this;
        if (typeof select !== 'undefined')
        {
            try
            {
                select(dropDownTree, e.node);
            } catch (err)
            {
                console.log(err);
            }
        }
    };

    element.kendoDropDownTree(opt);
}

function histaffDatePicker(element, opt)
{
    var item = element.data('kendoDatePicker');
    if (item)
    {
        if (typeof opt !== 'undefined' && typeof opt.destroy !== 'undefined'  && opt.destroy == true)
        {
            item.destroy();
            element.empty();
        }
        else return item;
    }
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.format === 'undefined') opt.format = "dd/MM/yyyy";
    var open = opt.open;
    opt.open = function (e)
    {
        var calendar = this.dateView.calendar;
        console.log(this.wrapper.width());
        if (this.wrapper.width() > 285)
        {
            calendar.wrapper.width(this.wrapper.width() - 6);
        }
        if (typeof open !== 'undefined')
        {
            try
            {
                open(dropDownTree);
            } catch (err)
            {
                console.log(err);
            }
        }
    };
    //opt.close= function(e)
    //{
    //    e.preventDefault(); //prevent popup closing
    //}
    element.kendoDatePicker(opt);
}

function histaffDateTimePicker(element, opt)
{
    var item = element.data('kendoDateTimePicker');
    if (item)
    {
        if (typeof opt !== 'undefined' && typeof opt.destroy !== 'undefined'  && opt.destroy == true)
        {
            item.destroy();
            element.empty();
        }
        else return item;
    }
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.format === 'undefined') opt.format = "dd/MM/yyyy hh:mm tt";

    element.kendoDateTimePicker(opt);
}

function histaffTimePicker(element, opt)
{
    var item = element.data('kendoTimePicker');
    if (item)
    {
        if (typeof opt !== 'undefined' && typeof opt.destroy !== 'undefined'  && opt.destroy == true)
        {
            item.destroy();
            element.empty();
        }
        else return item;
    }
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.format === 'undefined') opt.format = "HH:mm";
    if (typeof opt.interval === 'undefined') opt.interval = 15;
    element.kendoTimePicker(opt);
}

function histaffGrid(element, opt)
{
    var item = element.data('kendoGrid');
    if (item)
    {
        if (typeof opt !== 'undefined' && typeof opt.destroy !== 'undefined'  && opt.destroy == true)
        {
            item.destroy();
            element.empty();
        }
        else return item;
    }
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.event === 'undefined') opt.event = {};

    opt.excel = { allPages: true };
    if (typeof opt.multiple === 'undefined') opt.multiple = "multiple,row";
    if (typeof opt.scrollable === 'undefined') opt.scrollable = true;
    if (typeof opt.selectable === 'undefined') opt.selectable = "row";
    if (typeof opt.pageable === 'undefined')
        opt.pageable = {
            responsive: false,
            numeric: false,
            pageSizes: [10, 20, 50, 100],
        };
    if (typeof opt.dataSource === 'undefined')
        opt.dataSource = {
            schema: {
                data: "data",
                total: "total",
            },
            pageSize: 10,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
        };
    if (typeof opt.dataSource.pageSize === 'undefined') opt.dataSource.pageSize = 10;
    if (typeof opt.dataSource.serverFiltering === 'undefined') opt.dataSource.serverFiltering = true;
    if (typeof opt.dataSource.serverSorting === 'undefined') opt.dataSource.serverSorting = true;
    if (typeof opt.dataSource.serverPaging === 'undefined') opt.dataSource.serverPaging = true;
    if (typeof opt.dataSource.schema === 'undefined')
        opt.dataSource.schema = {
            data: "data",
            total: "total",
        };
    if (typeof opt.dataSource.schema.data === 'undefined') opt.dataSource.schema.data = "data";
    if (typeof opt.dataSource.schema.total === 'undefined') opt.dataSource.schema.total = "total";
    if (typeof opt.filterable === 'undefined')
        opt.filterable = {
            mode: 'row',
            extra: false,
            operators: {
                string: {
                    contains: "Contains"
                },
                number: {
                    eq: "Equal"
                },
                date: {
                    eq: "Equal"
                }
            }
        };
    if (typeof opt.filterable.mode === 'undefined') opt.filterable.mode = "row";
    if (typeof opt.filterable.extra === 'undefined') opt.filterable.extra = false;
    if (typeof opt.filterable.operators === 'undefined')
        opt.filterable.operators = {
            string: {
                contains: "Contains"
            },
            number: {
                eq: "Equal"
            },
            date: {
                eq: "Equal"
            }
        }
    // event
    var change = opt.change;
    var dataBound = opt.dataBound;
    opt.change = function (arg)
    {
        var grid = this;
        if (typeof change !== 'undefined')
        {
            try
            {
                change(grid);
            } catch (err)
            {
                console.log(err);
            }
        }
    };
    opt.dataBound = function (arg)
    {
        var grid = this;
        if (typeof opt.doubleclick !== 'undefined')
        {
            //add event dbclick
            grid.element.off('dblclick', 'tbody tr[data-uid]');
            grid.element.on('dblclick', 'tbody tr[data-uid]', function (e)
            {
                var dataItem = grid.dataItem($(e.target).closest('tr'));
                try
                {
                    opt.doubleclick(dataItem);
                } catch (err)
                {
                    console.log(err);
                }
            });
        }

        if (typeof dataBound !== 'undefined')
        {
            try
            {
                dataBound(grid);
            } catch (err)
            {
                console.log(err);
            }
        }
    };
    element.kendoGrid(opt);
}

function histaffTreeList(element, opt)
{
    var item = element.data('kendoTreeList');
    if (item)
    {
        if (typeof opt !== 'undefined' && typeof opt.destroy !== 'undefined'  && opt.destroy == true)
        {
            item.destroy();
            element.empty();
        }
        else return item;
    }
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.event === 'undefined') opt.event = {};

    opt.excel = { allPages: true };
    if (typeof opt.sortable === 'undefined') opt.sortable = false;
    if (typeof opt.filterable === 'undefined') opt.filterable = false;
    if (typeof opt.pageable === 'undefined') opt.pageable = false;
    if (typeof opt.scrollable === 'undefined') opt.scrollable = true;
    if (typeof opt.dataSource === 'undefined')
        opt.dataSource = {
            schema: {
                data: "data",
                total: "total",
            }
        };

    // event
    var change = opt.change;
    var dataBound = opt.dataBound;
    opt.change = function (arg)
    {
        if (typeof change !== 'undefined')
        {
            try
            {
                change();
            } catch (err)
            {
                console.log(err);
            }
        }
    };
    opt.dataBound = function (arg)
    {
        var grid = this;
        if (typeof opt.doubleclick !== 'undefined')
        {
            grid.element.off('dblclick');

            grid.element.on('dblclick', 'tbody tr[data-uid]', function (e)
            {
                var dataItem = grid.dataItem(element);
                try
                {
                    opt.doubleclick(dataItem);
                } catch (err)
                {
                    console.log(err);
                }
            });
        }

        if (typeof dataBound !== 'undefined')
        {
            try
            {
                dataBound(grid);
            } catch (err)
            {
                console.log(err);
            }
        }
    };
    element.kendoTreeList(opt);
}

function histaffTreeListProcessClass(items, levels = 0)
{
    levels++;
    for (var i = 0; i < items.length; i++)
    {
        var item = items[i];
        var div = $("li[data-uid='" + item.uid + "'] div:first");
        if (typeof div != undefined)
        {
            div.removeClass("node-first");
            div.removeClass("node-last");
            div.removeClass("node-mid");
            div.removeClass("node-top");
            if (item.isfirst) div.addClass("node-first");
            if (item.islast) div.addClass("node-last");
            if (item.ismid) div.addClass("node-mid");
            if (levels == 1) div.addClass("node-top");
        }
        if (item.items) treeviewProcessClass(item.items, levels);
    }

}
function histaffWindow(element, opt)
{
    var scroll = document.documentElement.scrollTop;
    var item = $("#window").data("kendoWindow");
    if (item)
    {
        if (typeof opt !== 'undefined' && typeof opt.destroy !== 'undefined'  && opt.destroy == true)
        {
            item.destroy();
            element.empty();
        }
        else return item;
    }
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.width === 'undefined') opt.width = $(window).width() - 70;
    if (typeof opt.height === 'undefined') opt.height = $(window).height() - 70;
    if (typeof opt.title === 'undefined') opt.title = "Window";
    if (typeof opt.actions === 'undefined') opt.actions = ["Close"];
    if (typeof opt.draggable === 'undefined') opt.draggable = false;
    if (typeof opt.pinned === 'undefined') opt.pinned = false;
    if (typeof opt.resizable === 'undefined') opt.resizable = false;
    if (typeof opt.modal === 'undefined') opt.modal = true;
    if (typeof opt.noRecords === 'undefined') opt.noRecords = false;
    if (typeof opt.iframe === 'undefined') opt.iframe = true;
    if (typeof opt.visible === 'undefined') opt.visible = true;

    //event 
    var deactivate = opt.deactivate;
    var close = opt.close;
    opt.deactivate = function (e)
    {
        this.destroy();
        if (typeof deactivate !== 'undefined') try { deactivate(e); } catch (err) { console.log(err); }
    };
    opt.close = function (e)
    {
        $('body').css('overflow', 'auto');
        window.scrollTo(0, scroll);
        if (typeof close !== 'undefined') try { close(e); } catch (err) { console.log(err); }
    };

    element.append("<div id='window'></div>");
    $("#window").kendoWindow(opt);
    var dialog = $("#window").data("kendoWindow");

    dialog.center().open();
    $('body').css('overflow', 'hidden');
}
var histaffConfirmTitleInterval;
function histaffConfirm(element, opt)
{
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.title === 'undefined') opt.title = translate("WARNING");

    element.histaffWindow({
        width: 450,
        height: 200,
        content: "/profile/confirm",
        title: opt.title,
        open: function (e)
        {
            if (typeof opt.open !== 'undefined') try { opt.open(e); } catch (err) { console.log(err); }
        },
        activate: function (e)
        {
            histaffConfirmTitleInterval = setInterval(function () { histaffConfirmTitleEvery100ms(opt.content); }, 100);
        },
        close: function (e)
        {
            var args = { status: false };
            var status = $('#window .k-content-frame').contents().find("#status");
            if (status) args.status = status.val() == "1" ? true : false;
            if (typeof opt.close !== 'undefined') try { opt.close(e, args); } catch (err) { console.log(err); }
        }
    });
}

function histaffConfirmTitleEvery100ms(popupMessage)
{
    var content = $('#window .k-content-frame').contents().find("#content");
    if (content) { if (content.html()) clearInterval(histaffConfirmTitleInterval); content.html(popupMessage); }
}

function histaffNumeric(element, opt)
{
    var item = element.data("kendoNumericTextBox");
    if (item)
    {
        if (typeof opt !== 'undefined' && typeof opt.destroy !== 'undefined'  && opt.destroy == true)
        {
            item.destroy();
            element.empty();
        }
        else return item;
    }
    if (typeof opt === 'undefined') opt = {};
    if (typeof opt.decimals === 'undefined') opt.decimals = 0;
    if (typeof opt.format === 'undefined') opt.format = "n0";

    //event 
    var change = opt.change;
    var spin = opt.spin;
    opt.change = function (e)
    {
        if (typeof change !== 'undefined') try { change(); } catch (err) { console.log(err); }
    };

    opt.close = function (e)
    {
        if (typeof spin !== 'undefined') try { spin(); } catch (err) { console.log(err); }
    };
    opt.min = 0; 
    element.kendoNumericTextBox(opt);
}