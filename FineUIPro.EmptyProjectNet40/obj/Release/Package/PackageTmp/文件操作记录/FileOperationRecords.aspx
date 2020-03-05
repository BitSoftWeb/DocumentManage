<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileOperationRecords.aspx.cs" Inherits="FineUIPro.EmptyProjectNet40.文件操作记录.FileOperationRecords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel" runat="server" />
        <f:Panel ID="Panel" ShowBorder="false" ShowHeader="false" BodyPadding="10px" Layout="VBox" BoxConfigAlign="Stretch" runat="server">
            <Items>             
                <f:Grid ID="Grid1" Title="数据表格" PageSize="15" IsFluid="true" EnableCheckBoxSelect="false" ShowBorder="true" BoxFlex="1" AllowPaging="true" IsDatabasePaging="false" ShowHeader="true" runat="server" DataKeyNames="ID">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd" Label="起始日期" EmptyText="请选择日期"
                                    ID="起始日期" ShowRedStar="true" Width="250px" LabelWidth="90" AutoPostBack="false">
                                </f:DatePicker>
                                <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd" Label="截止日期" EmptyText="请选择日期"
                                    ID="截止日期" ShowRedStar="true" Width="250px" LabelWidth="90" AutoPostBack="true">
                                </f:DatePicker>
                                <f:TwinTriggerBox runat="server" EmptyText="输入要搜索的关键词" ShowLabel="false" ID="ttbSearch"
                                    ShowTrigger1="false" Trigger1Icon="Clear"  Width="300">
                                </f:TwinTriggerBox>
                                <f:Button ID="select" Text="查询" OnClick="select_Click" runat="server"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField />
                        <f:RenderField ColumnID="ID" DataField="ID" HeaderText="ID" Hidden="True"></f:RenderField>
                        <f:RenderField ColumnID="用户名" DataField="用户名" HeaderText="用户名" Width="300"></f:RenderField>
                        <f:RenderField ColumnID="设备编号" TextAlign="Center" DataField="设备编号" HeaderText="设备编号"></f:RenderField>
                        <f:RenderField ColumnID="操作类型" DataField="操作类型" HeaderText="操作类型" Width="300"></f:RenderField>
                        <f:RenderField ColumnID="操作时间" DataField="操作时间" HeaderText="操作时间" Width="300"></f:RenderField>
                        <f:RenderField ColumnID="文件名" DataField="文件名" HeaderText="文件名" ExpandUnusedSpace="true"></f:RenderField>                      
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
