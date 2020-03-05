<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FTPDownloadCheckInformation.aspx.cs" Inherits="FineUIPro.EmptyProjectNet40.FTP.FTPDownloadCheckInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
   
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" EnableFormChangeConfirm="true" />
        

        <f:Form ID="SimpleForm1" IsFluid="true" Title="查看详情-图片" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Margin="20px"
            AutoScroll="true" BodyPadding="10px" runat="server">
            <Items>
                <f:Panel runat="server" ShowBorder="false" ShowHeader="false" Layout="VBox" BodyPadding="10px" AutoScroll="true">
                    <Items>                        
                        <f:Image ID="image_1"  runat="server" Margin="30px"  RegionPosition="Center" ImageHeight="600px" ImageWidth="800px"></f:Image>
                    </Items>
                </f:Panel>
            </Items>          
        </f:Form>
    </form>
</body>
</html>
