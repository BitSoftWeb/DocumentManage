﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginTest.aspx.cs" Inherits="mydddd.Web.LoginTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="LoginTestStyle/css/style.css" rel="stylesheet" />
    <link href="LoginTestStyle/css/reset.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .check {
            margin-left:76px;
        }
    </style>

</head>
<body style="background-image: url('/MenuImage/nebg.jpg')">
    <form id="form1" runat="server">
        <div id="main">
            <div id="title">
            </div>
            <div class="login" style="margin-left: 500px">
                <img id="img1" src="MenuImage/车.png" style="margin-top: 60px; margin-left: 40px" />
                <div style="margin-top: -25px; margin-left: 79px; font-size: 22px">企业资产运营管理平台</div>
                <div class="login-top">
                    <%--登录--%>
                </div>
                <div class="login-center clearfix">
                    <div class="login-center-img">
                        <img src="LoginTestStyle/img/name.png" /></div>
                    <div class="login-center-input">
                        <asp:TextBox ID="Login_name" runat="server"></asp:TextBox>
                        <div class="login-center-input-text">用户名</div>
                    </div>
                </div>
                <div class="login-center clearfix">
                    <div class="login-center-img">
                        <img src="LoginTestStyle/img/password.png" /></div>
                    <div class="login-center-input">
                        <asp:TextBox ID="Pass_word" runat="server" TextMode="Password"></asp:TextBox>
                        <%--<input id="label" runat="server" type="text" hidden="hidden" />--%>
                        <div class="login-center-input-text">密码</div>
                    </div>
                </div>
               
                <asp:CheckBox ID="CheckBox1" runat="server" CssClass="check" Text="记住账号和密码" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <br />
                <asp:Button CssClass="login-button1" ID="Login_Button" Text="登录" OnClick="Login_Button_Click" runat="server" />

            </div>
            <div class="sk-rotating-plane"></div>
        </div>
    </form>
</body>
</html>
