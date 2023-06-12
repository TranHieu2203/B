<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="About.aspx.vb" Inherits="HistaffWebApp.About" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>About HiStaff</title>
    <meta http-equiv="content-type" content="html/charset=utf-8" />
    <link href="Styles/about.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tip.js" type="text/javascript"></script>
</head>
<body class="wraper">
    <center>
        <img src="Static/Images/logo_large.png" width="175" height="56" alt="logo_large" />
    </center>
    <br />
    <br />
    <p class="inden15">
        <%# Translate("ABOUT_HISTAFF_1")%>
    </p>
    <br />
    <p class="inden15">
        <%# Translate("ABOUT_HISTAFF_2")%>
    </p>
    <br />
    <center>
        Copyright &copy; 2013 Tinhvan Consulting
        <img src="Static/Images/tvc_logo.png" width="80" alt="tvc_logo" style="position: absolute;
            right: 20px; bottom: 20px" />
    </center>
</body>
</html>
