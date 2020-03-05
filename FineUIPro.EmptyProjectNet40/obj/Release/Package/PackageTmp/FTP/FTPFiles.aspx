<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FTPFiles.aspx.cs" Inherits="FineUIPro.EmptyProjectNet40.FTP.FTPFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="sourcefiles" content="~/new_window.aspx" />
    <title>ASP.NET的FTP上传和下载</title>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <%--<asp:FileUpload runat="server" ID="FileUpload"></asp:FileUpload>  
            <asp:Button ID="Button1" runat="server" Text="FTP上传" OnClick="Button1_Click" /> --%>
        <%--<asp:Button ID="Button2" runat="server" Text="刷新列表" OnClick="Button2_Click" />--%>
        <br />
        <br />
        <f:PageManager runat="server" AutoSizePanelID="Panel"></f:PageManager>
        <f:Panel ID="Panel" ShowBorder="false" ShowHeader="false" Layout="VBox" BodyPadding="10px" runat="server">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:FileUpload ID="upFile" Width="450px" EmptyText="请选择文件" Label="文件上传" Required="true" ButtonIcon="Add"
                            ShowRedStar="true" runat="server">
                        </f:FileUpload>
                        <f:Button ID="Button2" Text="FTP上传" OnClick="Button2_Click" runat="server"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Grid ID="Grid1" Title="数据表格" PageSize="15" IsFluid="true" CssClass="blockpanel" ShowBorder="true" EnableCollapse="false" BoxFlex="1" AllowPaging="true" IsDatabasePaging="false"
                    ShowHeader="true" runat="server" EnableCheckBoxSelect="false" Height="400px" DataKeyNames="ID,文件名,新文件名,扩展名,上传时间" OnRowCommand="Grid1_RowCommand">
                    <Columns>
                        <f:RowNumberField></f:RowNumberField>
                        <%--<f:ImageField ImageUrl="~/res/icon/page_white_powerpoint.png"></f:ImageField>--%>
                        <f:RenderField ColumnID="ID" DataField="ID" HeaderText="ID" Hidden="True"></f:RenderField>
                        <f:RenderField ColumnID="文件名" DataField="文件名" HeaderText="文件名" Width="400px" ExpandUnusedSpace="true"></f:RenderField>
                        <f:RenderField ColumnID="上传时间" DataField="上传时间" HeaderText="上传时间" Width="180px"></f:RenderField>
                        <f:RenderField ColumnID="文件类型" DataField="文件类型" HeaderText="文件类型" Hidden="true"></f:RenderField>
                        <f:TemplateField Width="100px">
                            <ItemTemplate>
                                <a href="javascript:;" onclick="<%# GetEditUrl(Eval("新文件名")) %>">查看详情</a>
                                <%--<a href="./new_window.aspx" onclick="<%# GetEditUrl(Eval("ID")) %>">查看详情</a>--%>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:LinkButtonField Width="160px" TextAlign="Center" Text="下载" CommandName="download" ConfirmTarget="Top"></f:LinkButtonField>
                    </Columns>
                </f:Grid>
            </Items>

<%--            <Toolbars>
                <f:Toolbar runat="server" Position="Bottom" ToolbarAlign="Right">
                    <Items>
                        <f:Button Text="点击弹出通知对话框" runat="server" ID="btnHello" ValidateForms="SimpleForm1" OnClick="btnHello_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>--%>
        </f:Panel>
        <%--<table border="1">
                <tr>
                    <th>编号</th>
                    <th>文件夹</th>
                    <th>文件名</th>
                    <th>日期</th>
                    <th>http协议下载</th>
                    <th>ftp协议下载</th>
                </tr>
                <asp:Repeater runat="server" ID="Repeater1">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("fileNo") %></td>
                            <td><%#Eval("ftpURI") %></td>
                            <td><%#Eval("fileName") %></td>
                            <td><%#Eval("datetime") %></td>
                            <td><a target="_blank" href='ftp://192.168.1.171:26/20191212/测试.xlsx'></a></td>                                              
                            <td><input type="button" value="下载" οnclick=ftpDownload('<%#Eval("ftpURI") %>','<%#Eval("fileName") %>') /></td>         
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>--%>
    </form>
</body>
</html>

<script type="text/javascript">
    function ftpDownload(uri, name) {
        $.get("ftpDownload.ashx",
            { ftpURI: uri, fileName: name },
            function (e) {
                if (e == "ok") {
                    alert("下载成功！文件在：\r\n C:\\" + name);
                }
                else {
                    alert("下载失败:\r\n" + e);
                }
            });
    }
</script>

