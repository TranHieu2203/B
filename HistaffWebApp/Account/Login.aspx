<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="HistaffWebApp.Login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=EDGE" />
    <title></title>
    <link type="image/x-icon" rel="shortcut icon" href="/Static/images/fav-icon.ico" />
    <link href="../../../Styles/userCustom.css" rel="stylesheet" type="text/css">
    <link href="http://fonts.cdnfonts.com/css/myriad-pro" rel="stylesheet">
    <script type="text/javascript" src="/Scripts/jquery-1.7.1.min.js"></script>
</head>

<body style="overflow: visible; background-color: #e6e6e6;">
    <img style="position: absolute; width: 100%; height: 100%;" src="../static/images/loginbackground.jpg">
    <div class="bgrtrans">
    </div>
    <tlk:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        <StyleSheets>
            <tlk:StyleSheetReference Path="~/Styles/Login.css" />
        </StyleSheets>
    </tlk:RadStyleSheetManager>
    <form id="form1" runat="server">
        <tlk:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="360000">
            <Scripts>
                <tlk:RadScriptReference Path="~/Scripts/noty/jquery.noty.js" />
                <tlk:RadScriptReference Path="~/Scripts/noty/layouts/center.js" />
                <tlk:RadScriptReference Path="~/Scripts/noty/layouts/topCenter.js" />
                <tlk:RadScriptReference Path="~/Scripts/noty/themes/default.js" />
            </Scripts>
        </tlk:RadScriptManager>
        <tlk:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="400"
                    Height="275px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
                    Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin reset mật khẩu%>">
                </tlk:RadWindow>
            </Windows>
        </tlk:RadWindowManager>
        <div>
            <style>
                .Maintain {
                    overflow: hidden;
                    width: 99%;
                    font-family: "segoe ui",arial,sans-serif;
                    position: absolute;
                    font-size: 13px;
                    bottom: 60px;
                    text-align: center;
                    color: red;
                }
                .noty_buttons{
                    border-top: none !important;
                    background: inherit !important;
                }
                .box{
                    background-image : url("../Static/Images/login_border.png");
                    background-size: contain;
                }
                input[type="checkbox" i] {
                    margin: 3px 0 3px 4px;
                    background-color: color: #026856;
                }
                #RadWindowWrapper_rwPopup{
                    border-radius: 10px;
                    overflow: hidden;
                }
                .rwTitlebar, .rwTopLeft, .rwTopRight, .rwTopResize {
                    background-color: #106E5E !important;
                    background: #106E5E !important;
                }
                .rwControlButtons a {
                    background-color: #106E5E !important;
                    border: unset !important;
                }
                .RadWindow_Metro {
                    border: unset !important;
                }
            </style>
            <asp:HiddenField runat="server" ID="hidAccept" />
            <asp:Panel ID="Panel1" runat="server" DefaultButton="LoginUser$LoginButton" Height="100%">
                <asp:Login ID="LoginUser" runat="server" EnableViewState="true" Width="100%" RememberMeSet="true">
                    <LayoutTemplate>
                        <div class="box">
                            <div style="text-align: center; object-fit: cover;">
                                <img height="55" id="logo" src="../Static/Images/logo_largest.png">
                            </div>
                            <div class="sperator">
                            </div>
                            <div style="text-align: center;">
                                <%--<p style="
                                            color: #026856;
                                            font-size: 27px;
                                            height: 44px;
                                            font-weight: bold;
                                            margin: 15px 0 38px 0;
                                            font-family: 'Myriad Pro', sans-serif !important;">
                                    <%# Translate("HỆ THỐNG")%>
                                    <br />
                                    <%# Translate("QUẢN TRỊ NHÂN SỰ")%>
                                </p>--%>
                                <img height="55" style="margin: 18px 0 0 0;" id="title_text" src="../Static/Images/title_text.png">
                            </div>
                            <div class="input">
                                <asp:TextBox ID="UserName" runat="server" CssClass="text" placeholder="Nhập username" ></asp:TextBox>
                            </div>
                            <div class="input">
                                <asp:TextBox ID="Password" runat="server" CssClass="text next" TextMode="Password" placeholder="Nhập password"></asp:TextBox>
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="ĐĂNG NHẬP"
                                    CssClass="button" />
                                <%--  <div style="float: left">
                                    <asp:DropDownList ID="rcbLanguage" runat="server" AutoPostBack="true" CausesValidation="false"
                                        Visible="true" Style="float: left; margin-left: 28px;">
                                    </asp:DropDownList>
                                </div>--%>
                                <div style="width: 262px; text-align: center; margin: 12px auto 0 auto;">
                                    <div style="display: flex;">
                                        <div>
                                            <a href="#" id="forget_Pass" style="
                                                                                color: white;
                                                                                /* text-underline-position: unset; */
                                                                                font-size: 12px;
                                                                                text-decoration: none;
                                                                                padding: 7px;
                                                                                background-color: #026856;">Quên mật khẩu</a>
                                        </div>
                                        <div class="remember" style="
                                                                    width: 60%;
                                                                    color: #026856;
                                                                    margin-left: 8px;">
                                            <label class="container">Ghi nhớ tài khoản
                                            <asp:CheckBox ID="RememberMe" runat="server"
                                                TextAlign="Left" />
                                            <span class="checkmark"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="Maintain" style="font-size:18px;font-weight:600">
                            <asp:Label ID="Noti_Maintain" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="copyright">
                            <img alt="Copyright" width="78" src="../Static/Images/tvc_logo.png">
                            <div>
                                Copyright &copy; 2022 Tinhvan Consulting
                            </div>
                        </div>
                    </LayoutTemplate>
                </asp:Login>
            </asp:Panel>
            <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">
                    $(document).ready(function () {
                        var path = getCookie("useravatar");
                        if (path != "") {
                            $.ajax({
                                url: path,
                                success: function (data) {
                                    $("#avatar").attr("src", path);
                                },
                                error: function (data) {
                                    $("#avatar").attr("src", "../Static/Images/user_login.png");
                                },
                            });
                        }
                    });
                    function getCookie(cname) {
                        var name = cname + "=";
                        var ca = document.cookie.split(';');
                        for (var i = 0; i < ca.length; i++) {
                            var c = ca[i].trim();
                            if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
                        }
                        return "";
                    }

                    function OnClientClicking() {

                        var user = $("#<%= LoginUser.FindControl("UserName").ClientID %>").val();
                        user = $.trim(user);
                        $("#<%= LoginUser.FindControl("UserName").ClientID %>").val(user);
                    }

                    function OnClientShow(sender, eventArgs) {
                        $get("<%= LoginUser.FindControl("UserName").ClientID %>").focus();
                    }

                    //Thiết lập notify
                    $.noty.defaults = {
                        layout: 'topCenter',
                        theme: 'defaultTheme',
                        type: 'alert',
                        text: '',
                        dismissQueue: true, // If you want to use queue feature set this true
                        template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
                        animation: {
                            open: { height: 'toggle' },
                            close: { height: 'toggle' },
                            easing: 'swing',
                            speed: 500 // opening & closing animation speed
                        },
                        timeout: false, // delay for closing event. Set false for sticky notifications
                        force: true, // adds notification to the beginning of queue when set to true
                        modal: false,
                        closeWith: ['click'], // ['click', 'button', 'hover']
                        callback: {
                            onShow: function () { },
                            afterShow: function () { },
                            onClose: function () { },
                            afterClose: function () { }
                        },
                        buttons: false // an array of buttons
                    };

                    $("#forget_Pass").click(function (event) {
                        event.preventDefault();
                        OpenEditWindow();
                    });

                    function OpenEditWindow() {
                        var oWindow = radopen('/Account/ForgetPass.aspx', "rwPopup");
                        oWindow.setSize(400, 300);
                        oWindow.center();
                    }
                    function OnClientClose(sender, args) {
                        var m;
                        var arg = args.get_argument();
                        if (arg == '1') {
                            m = '<%# Translate("Đổi mật khẩu thành công") %>';
                            var n = noty({ text: m, dismissQueue: true, type: 'success' });
                            setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                        }
                    }
                </script>
            </tlk:RadCodeBlock>
        </div>
    </form>
</body>
</html>
