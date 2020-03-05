<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="设备文件.aspx.cs" Inherits="FineUIPro.EmptyProjectNet40.设备文件.设备文件" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel" runat="server" />
        <br />
        <br />
        <f:Panel ID="Panel" ShowBorder="false" runat="server" ShowHeader="false" BodyPadding="10px" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Form runat="server" ID="Form6" ShowBorder="false" ShowHeader="false">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:DropDownList ID="上传类型" Label="文件类型" Width="250px" LabelWidth="110" AutoPostBack="true" OnSelectedIndexChanged="上传类型_SelectedIndexChanged" runat="server">
                                    <f:ListItem Text="全部" Value="全部" />
                                    <f:ListItem Text="设备文件类型" Value="设备文件类型" />
                                    <f:ListItem Text="设备台账类型" Value="设备台账类型" />
                                    
                                </f:DropDownList>
                                <f:DropDownList ID="设备台账类型" Width="250px" Label="设备台账类型" Hidden="true" LabelWidth="150" AutoPostBack="true" OnSelectedIndexChanged="设备台账类型_SelectedIndexChanged" AutoSelectFirstItem="false" runat="server">
                                </f:DropDownList>
                                <f:DropDownList ID="设备文件类型" Width="250px" Label="设备文件类型" Hidden="true" LabelWidth="150" AutoPostBack="true" OnSelectedIndexChanged="设备文件类型_SelectedIndexChanged" AutoSelectFirstItem="false" runat="server">
                                </f:DropDownList>
                                <%--<f:DropDownList ID="设备文件类型" Width="250px" Label="设备文件类型" LabelWidth="110"  ShowLabel="false" AutoPostBack="true" OnSelectedIndexChanged="设备文件类型_SelectedIndexChanged" AutoSelectFirstItem="false" runat="server">
                                </f:DropDownList>--%>
                                <f:TwinTriggerBox runat="server" EmptyText="输入要搜索的关键词" ShowLabel="false" ID="tSearch" ShowTrigger1="false" Trigger1Icon="Clear" Trigger2Icon="Search" OnTrigger2Click="tSearch_Trigger2Click">
                                </f:TwinTriggerBox>
                                
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid runat="server" ForceFit="true" ID="Grid1" Title="数据表格" PageSize="15" IsFluid="true" CssClass="blockpanel" ShowBorder="true" BoxFlex="1" AllowPaging="true"
                    IsDatabasePaging="false" ShowHeader="true" DataKeyNames="ID,设备编号,原文件名,文件名,上传路径,上传时间,文件后缀,文件类型" OnRowCommand="Grid1_RowCommand" EnableCheckBoxSelect="false">
                    <Columns>
                        <f:RowNumberField></f:RowNumberField>
                        <f:RenderField ColumnID="设备编号" TextAlign="Center" DataField="设备编号" HeaderText="设备编号"></f:RenderField>
                        <f:RenderField ColumnID="原文件名" TextAlign="Center" DataField="原文件名" HeaderText="文件名"></f:RenderField>
                        <f:RenderField ColumnID="上传时间" TextAlign="Center" DataField="上传时间" HeaderText="上传时间"></f:RenderField>
                        <f:RenderField ColumnID="文件后缀" TextAlign="Center" DataField="文件后缀" HeaderText="扩展名"></f:RenderField>
                        <f:RenderField ColumnID="文件类型" TextAlign="Center" DataField="文件类型" HeaderText="文件类型"></f:RenderField>
                       
                       <%-- <f:LinkButtonField Text="查看详情" Width="120px" TextAlign="Center"></f:LinkButtonField>--%>
                       <%-- <f:TemplateField ID="download" HeaderText="下载" TextAlign="Center" >
                            <ItemTemplate>
                                <a href="../<%#Eval("上传路径") %>" style="text-align:center;">下载</a>
                            </ItemTemplate>
                        </f:TemplateField>--%>
                        <f:LinkButtonField ID="download" Text="下载" Width="120px" CommandName="downloadDocument" TextAlign="Center"></f:LinkButtonField>                       
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
</body>

</html>
<script>

    //var url = 'ftp://192.168.1.130:26/FTPcs/Word/201912m26d3147446a179.doc'; //要预览文件的访问地址
    //window.open('http://127.0.0.1:8012/onlinePreview?url='+encodeURIComponent(url));
</script>
