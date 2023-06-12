<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSessionWarning.ascx.vb"
    Inherits="Common.ctrlSessionWarning" %>
<style type="text/css">
    .modalBackground {
        background-color: Black;
        opacity: .15;
        filter: alpha(opacity: 15);
        z-index: 999999999999;
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        display: none;
    }

    .popupDialog {
        display: none;
        z-index: 9999999999999;
        position: fixed;
        left: 0px;
        top: 150px;
        width: 100%;
    }

    .popupDialogCountDown {
        display: block;
        z-index: 9999999999999;
        position: fixed;
        left: 3px;
        top: 5px;
        width: 40px;
        height: 15px;
        text-align: center;
        background-color: White;
        color: Red;
        border: 1px solid #909090;
        font-family: Courier New;
    }
</style>
<div id="dlgPopup_temp" style="display: none">
    <div style="width: 350px; text-align: center; margin: 0 auto; background-color: white; border: 1px solid #808080">
        <div style="padding: 20px;">
            Session của bạn sẽ hết trong vòng <b><span class="CountDown"></span></b>
            <br />
            Nhấn "Tiếp tục" để tiếp tục phiên làm việc của bạn!
        </div>
        <div style="padding-bottom: 20px">
            <input type="button" value="<%# Translate("Tiếp tục") %>" onclick="btnCONTINUE_ClientClicking()" />
        </div>
    </div>
</div>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var sessionTimeoutWarning = '<%= System.Configuration.ConfigurationSettings.AppSettings("SessionWarning").ToString()%>';
        var sessionTimeout = "<%= Session.Timeout %>";
        var sessionTimeoutWarningCount = sessionTimeoutWarning * 60;
        var sessionTimeoutCount = sessionTimeout * 60;

        var _countDownTimer, _countDownTimerTest, _popupTimer;

        function updateCountDown() {
            //if (!_popupShow) return;
            var min = Math.floor(sessionTimeoutWarningCount / 60);
            var sec = sessionTimeoutWarningCount % 60;
            if (sec < 10)
                sec = "0" + sec;

            $('#dlgPopup .CountDown').html(min + ":" + sec);
            //$('.CountDownHolder').html(min + ":" + sec);

            sessionTimeoutWarningCount--;

            if (sessionTimeoutWarningCount > 0) {
                clearTimeout(_countDownTimer);
                _countDownTimer = setTimeout(updateCountDown, 1000);
            } else {
                document.location = 'Account/Login.aspx?ReturnUrl=<%= Request.RawUrl.Replace("/", "%2f").Replace("?", "%3f").Replace("=", "%3d").Replace("&","%26") %>';
            }
        };

        function updateCountDownTest() {
            //if (!_popupShow) return;
            var min = Math.floor(sessionTimeoutCount / 60);
            var sec = sessionTimeoutCount % 60;
            if (sec < 10)
                sec = "0" + sec;

            //$('#dlgPopup .CountDownHolder').html(min + ":" + sec);
            $('#countdownPopup .CountDownHolder').html(min + ":" + sec);

            sessionTimeoutCount--;

            if (sessionTimeoutCount > 0) {
                clearTimeout(_countDownTimerTest);
                _countDownTimerTest = setTimeout("updateCountDownTest()", 1000);
            }
            else {
                clearTimeout(_countDownTimerTest);
            }
        };

        function stopTimers() {
            if (_countDownTimer != null) clearTimeout(_countDownTimer);
            if (_countDownTimerTest != null) clearTimeout(_countDownTimerTest);
            if (_popupTimer != null) clearTimeout(_popupTimer);
            //alert('End Clean');
            sessionTimeoutWarningCount = sessionTimeoutWarning * 60;
            sessionTimeoutCount = sessionTimeout * 60;
        };

        function showCountdownPopup() {

            // clear all dic that is shown
            $('#countdownPopup').remove();

            //$('<div class="modalBackground" id="popupBackground"></div>').appendTo($('body'));
            $('<div class="popupDialogCountDown" id="countdownPopup"><span class="CountDownHolder"></span><span class="debug"></span><span class="clear"></span></div>').appendTo($('body'));

            updateCountDownTest();
        };

        function showWarningPopup() {
            //stopTimers();
            //_popupShow = true;

            // clear all dic that is shown
            $('.modalBackground, #dlgPopup').remove();

            $('<div class="modalBackground" id="popupBackground"></div>').appendTo($('body'));
            $('<div class="popupDialog" id="dlgPopup"></div>').appendTo($('body'));

            $('#dlgPopup').html($('#dlgPopup_temp').html());

            $('#popupBackground, #dlgPopup').show();

            updateCountDown();
        };

        function btnCONTINUE_ClientClicking() {

            $.ajax({
                url: '<%= ResolveClientUrl("~/default.aspx?mid=Profile&fid=ctrlDashboard") %>',
                success: function (data) {
                    $('#popupBackground, #dlgPopup').remove();
                    stopTimers();
                    _popupTimer = window.setTimeout(showWarningPopup, (sessionTimeout - sessionTimeoutWarning) * 60 * 1000);
                    updateCountDownTest();
                }
            });
        }

        $(document).ready(function () {
            showCountdownPopup();
            stopTimers();
            updateCountDownTest();

            _popupTimer = window.setTimeout(showWarningPopup, (sessionTimeout - sessionTimeoutWarning) * 60 * 1000);
        });
    </script>
</tlk:RadScriptBlock>
